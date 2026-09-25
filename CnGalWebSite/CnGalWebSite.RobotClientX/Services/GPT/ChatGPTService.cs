using CnGalWebSite.EventBus.Models;
using CnGalWebSite.EventBus.Services;
using CnGalWebSite.RobotClientX.Configuration;
using CnGalWebSite.RobotClientX.Models.GPT;
using CnGalWebSite.RobotClientX.Models.Messages;
using CnGalWebSite.RobotClientX.Services.Messages;
using Microsoft.Extensions.Options;

namespace CnGalWebSite.RobotClientX.Services.GPT
{
    public class ChatGPTService : IChatGPTService
    {
        private readonly RobotOptions _robotOptions;
        private readonly GroupHistoryOptions _groupHistoryOptions;
        private readonly ILogger<ChatGPTService> _logger;
        private readonly IGroupMessageCacheService _groupMessageCacheService;
        private readonly IEventBusService _eventBusService;

        public ChatGPTService(IOptions<RobotOptions> robotOptions,
            IOptions<GroupHistoryOptions> groupHistoryOptions, ILogger<ChatGPTService> logger,
            IGroupMessageCacheService groupMessageCacheService, IEventBusService eventBusService)
        {
            _robotOptions = robotOptions.Value;
            _groupHistoryOptions = groupHistoryOptions.Value;
            _logger = logger;
            _groupMessageCacheService = groupMessageCacheService;
            _eventBusService = eventBusService;
        }

        public async Task<string> GetReply(long sendTo)
        {
            var qq = _robotOptions.QQ;
            var messages = _groupMessageCacheService.GetGroupMessages(sendTo);

            if (messages.Count == 0)
            {
                _logger.LogError("无法获取群聊历史记录：{id}", sendTo);
                return null;
            }

            // 判断是否需要清理历史消息
            if (_groupMessageCacheService.GetGroupMessages(sendTo).Count > _groupHistoryOptions.MaximumMessages)
            {
                _groupMessageCacheService.KeepLatestMessages(sendTo, _groupHistoryOptions.RetainedMessages);
            }

            // 拼接历史消息
            var model = new KanbanGroupGptModel
            {
                Messages = messages.Select(s => new KanbanGroupMessageModel
                {
                    IsAssistant = s.SenderId == qq,
                    Name = s.SenderName,
                    Text = s.Content,
                    Id = s.SenderId
                }).ToList()
            };

            var latestMessage = model.Messages[^1].Text ?? string.Empty;
            if (latestMessage.Length > 200)
            {
                latestMessage = $"{latestMessage[..197]}...";
            }
            _logger.LogInformation("向看板娘发送请求：群 {GroupId}，历史 {MessageCount} 条，最新消息：{LatestMessage}", sendTo, model.Messages.Count, latestMessage);

            var result = await _eventBusService.CallKanbanGroupChatGPT(model);
            if (result == null || result.Success == false)
            {
                _logger.LogError("获取GPT回复失败：{id}", sendTo);
                return null;
            }


            return result.Message.Replace($"【看板娘】\n", "").Replace("【看板娘】\\n", "").Replace("【看板娘】", "").TrimStart();
        }
    }
}
