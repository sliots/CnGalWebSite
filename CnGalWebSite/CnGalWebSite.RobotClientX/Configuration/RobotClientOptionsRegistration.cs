namespace CnGalWebSite.RobotClientX.Configuration;

public static class RobotClientOptionsRegistration
{
    public static IServiceCollection AddRobotClientConfiguration(this IServiceCollection services)
    {
        services.AddOptions<RobotOptions>()
            .BindConfiguration(RobotOptions.SectionName);

        services.AddOptions<OneBotOptions>()
            .BindConfiguration(OneBotOptions.SectionName);

        services.AddOptions<CnGalApiOptions>()
            .BindConfiguration(CnGalApiOptions.SectionName);

        services.AddOptions<WebSiteOptions>()
            .BindConfiguration(WebSiteOptions.SectionName);

        services.AddOptions<ReplyLimitsOptions>()
            .BindConfiguration(ReplyLimitsOptions.SectionName);

        services.AddOptions<GroupHistoryOptions>()
            .BindConfiguration(GroupHistoryOptions.SectionName);

        return services;
    }
}
