namespace CnGalWebSite.DrawingBed.Configuration;

public sealed class DatabaseOptions
{
    public const string SectionName = "ConnectionStrings";
    public string Default { get; set; }
}

public sealed class JwtAuthorityOptions
{
    public const string SectionName = "JwtBearer";
    public string Authority { get; set; }
}

public sealed class AliyunOssOptions
{
    public const string SectionName = "AliyunOss";
    public string Endpoint { get; set; }
    public string BucketName { get; set; }
    public string AccessKeyId { get; set; }
    public string AccessKeySecret { get; set; }
    public string PublicBaseAddress { get; set; }
}

public sealed class TencentCosOptions
{
    public const string SectionName = "TencentCos";
    public string Region { get; set; }
    public string SecretId { get; set; }
    public string SecretKey { get; set; }
    public string BucketName { get; set; }
    public string PublicBaseAddress { get; set; }
}

public sealed class TucangCcOptions
{
    public const string SectionName = "TucangCc";
    public string ApiToken { get; set; }
    public string UploadUrl { get; set; }
    public string PublicBaseAddress { get; set; }
}

public sealed class FFmpegOptions
{
    public const string SectionName = "FFmpeg";
    public string Path { get; set; }
}
