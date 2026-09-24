using MySqlConnector;

namespace CnGalWebSite.DrawingBed.Configuration;

public static class DrawingBedOptionsRegistration
{
    public static IServiceCollection AddDrawingBedConfiguration(this IServiceCollection services)
    {
        services.AddOptions<DatabaseOptions>()
            .BindConfiguration(DatabaseOptions.SectionName)
            .Validate(IsValidDatabase, "ConnectionStrings:Default is missing or invalid.")
            .ValidateOnStart();
        services.AddOptions<JwtAuthorityOptions>()
            .BindConfiguration(JwtAuthorityOptions.SectionName)
            .Validate(options => Uri.TryCreate(options.Authority, UriKind.Absolute, out var uri) &&
                (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps),
                "JwtBearer:Authority must be an absolute HTTP or HTTPS address.")
            .ValidateOnStart();
        services.AddOptions<AliyunOssOptions>()
            .BindConfiguration(AliyunOssOptions.SectionName)
            .Validate(options => !string.IsNullOrWhiteSpace(options.Endpoint),
                "AliyunOss:Endpoint is required.")
            .Validate(options => !string.IsNullOrWhiteSpace(options.BucketName),
                "AliyunOss:BucketName is required.")
            .ValidateOnStart();
        services.AddOptions<TencentCosOptions>()
            .BindConfiguration(TencentCosOptions.SectionName)
            .Validate(options => !string.IsNullOrWhiteSpace(options.BucketName),
                "TencentCos:BucketName is required.")
            .ValidateOnStart();
        services.AddOptions<TucangCcOptions>().BindConfiguration(TucangCcOptions.SectionName);
        services.AddOptions<FFmpegOptions>().BindConfiguration(FFmpegOptions.SectionName);
        return services;
    }

    private static bool IsValidDatabase(DatabaseOptions options)
    {
        if (string.IsNullOrWhiteSpace(options.Default))
            return false;
        try
        {
            var connection = new MySqlConnectionStringBuilder(options.Default);
            return !string.IsNullOrWhiteSpace(connection.Server) &&
                !string.IsNullOrWhiteSpace(connection.Database) &&
                (connection.ConnectionProtocol != MySqlConnectionProtocol.Sockets ||
                 connection.Port is >= 1 and <= 65535);
        }
        catch (Exception ex) when (ex is ArgumentException or FormatException or
            OverflowException or InvalidCastException)
        {
            return false;
        }
    }
}
