namespace CnGalWebSite.RobotClientX.Configuration;

public sealed class RobotOptions
{
    public const string SectionName = "Robot";

    public long QQ { get; set; }
    public string Name { get; set; } = "看板娘";
    public long WarningGroup { get; set; }
    public string SensitiveReply { get; set; } = "";
}

public sealed class OneBotOptions
{
    public const string SectionName = "OneBot";

    public string Host { get; set; } = "";
    public int WebSocketPort { get; set; }
    public int HttpPort { get; set; }
    public string Token { get; set; } = "";
}

public sealed class CnGalApiOptions
{
    public const string SectionName = "CnGalApi";

    public string BaseAddress { get; set; } = "https://api.cngal.org/";
}

public sealed class WebSiteOptions
{
    public const string SectionName = "WebSite";

    public string Name { get; set; } = "看板娘在这里哦";
    public string BackgroundImageUrl { get; set; } =
        "https://res.cngal.org/_content/CnGalWebSite.Shared/images/game_head.png";
}

public sealed class ReplyLimitsOptions
{
    public const string SectionName = "ReplyLimits";

    public int SinglePerMinute { get; set; } = 5;
    public int TotalPerMinute { get; set; } = 10;
}

public sealed class GroupHistoryOptions
{
    public const string SectionName = "GroupHistory";

    public int MaximumMessages { get; set; } = 30;
    public int RetainedMessages { get; set; } = 10;
}
