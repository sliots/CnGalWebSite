namespace CnGalWebSite.Kanban.ChatGPT.Configuration;

public static class KanbanChatGptOptionsRegistration
{
    public static IServiceCollection AddKanbanChatGptConfiguration(this IServiceCollection services)
    {
        services.AddOptions<ChatGptOptions>()
            .BindConfiguration(ChatGptOptions.SectionName);

        services.AddOptions<CnGalApiOptions>()
            .BindConfiguration(CnGalApiOptions.SectionName);

        services.AddOptions<PersistentStorageOptions>()
            .BindConfiguration(PersistentStorageOptions.SectionName);

        return services;
    }
}
