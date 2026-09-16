namespace CnGalWebSite.RobotClientX.Services.ExternalDatas
{
    public interface IExternalDataService
    {
        Task<string> GetArgValue(string name, string infor, long qq, Dictionary<string, string> adds);
    }
}
