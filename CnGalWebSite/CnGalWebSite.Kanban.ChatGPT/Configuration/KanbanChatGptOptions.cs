namespace CnGalWebSite.Kanban.ChatGPT.Configuration;

public sealed class ChatGptOptions
{
    public const string SectionName = "ChatGpt";

    public string ApiKey { get; set; } = "";
    public string BaseAddress { get; set; } = "";
    public string Model { get; set; } = "deepseek-flash";
    public int GlobalRequestsPerMinute { get; set; } = 30;
    public int GlobalRequestsPerDay { get; set; } = 1000;
    public int MaxMessageLength { get; set; } = 30;
    public string SystemMessageTemplate { get; set; } = "";
    public string SampleUser { get; set; } = "";
    public string SampleKanban { get; set; } = "";
    public int MaxRecursionDepth { get; set; } = 10;
    public bool EnableFunctionCalling { get; set; } = true;
    public bool DisablePersonalizedSystem { get; set; } = true;
}

public sealed class CnGalApiOptions
{
    public const string SectionName = "CnGalApi";

    public string BaseAddress { get; set; } = "https://api.cngal.org/";
}

public sealed class PersistentStorageOptions
{
    public const string SectionName = "PersistentStorage";

    public string Directory { get; set; } = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "storage");
}
