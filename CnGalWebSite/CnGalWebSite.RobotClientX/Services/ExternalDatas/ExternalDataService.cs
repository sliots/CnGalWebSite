using CnGalWebSite.Core.Services;
using CnGalWebSite.RobotClientX.Models.Messages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace CnGalWebSite.RobotClientX.Services.ExternalDatas
{
    public class ExternalDataService:IExternalDataService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<ExternalDataService> _logger;
        private readonly IHttpService _httpService;

        public ExternalDataService(IHttpService httpService, IConfiguration configuration, ILogger<ExternalDataService> logger)
        {
            _httpService = httpService;
            _configuration = configuration;
            _logger = logger;

        }

        public async Task<string> GetArgValue(string name, string infor, long qq, Dictionary<string, string> adds)
        {

            //若本地没有 则请求服务器
            var result = await _httpService.PostAsync<GetArgValueModel, Result>(_configuration["WebApiPath"] + "api/robot/GetArgValue", new GetArgValueModel
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
