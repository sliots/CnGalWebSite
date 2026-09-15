using CnGalWebSite.Core.Services;
using CnGalWebSite.RobotClientX.Configuration;
using CnGalWebSite.RobotClientX.Models.Messages;
using Microsoft.Extensions.Options;

namespace CnGalWebSite.RobotClientX.Services.ExternalDatas
{
    public class ExternalDataService : IExternalDataService
    {
        private readonly CnGalApiOptions _cnGalApiOptions;
        private readonly IHttpService _httpService;

        public ExternalDataService(IHttpService httpService, IOptions<CnGalApiOptions> cnGalApiOptions)
        {
            _httpService = httpService;
            _cnGalApiOptions = cnGalApiOptions.Value;
        }

        public async Task<string> GetArgValue(string name, string infor, long qq, Dictionary<string, string> adds)
        {
            //若本地没有 则请求服务器
            var result = await _httpService.PostAsync<GetArgValueModel, Result>(
                _cnGalApiOptions.BaseAddress + "api/robot/GetArgValue", new GetArgValueModel
                {
                    Infor = infor,
                    Name = name,
                    AdditionalInformations = adds,
                    SenderId = qq,
                });

            //判断结果
            if (result.Successful == false)
            {
                throw new ArgError(result.Error);
            }
            else
            {
                return result.Error;
            }
        }
    }
}
