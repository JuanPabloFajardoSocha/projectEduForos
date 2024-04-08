namespace eduForos.Infrastructure.Settings.Cloudinary;


public class CloudinarySettings
{
    //Cloudinary settings
    public const string SectionName = "CloudinarySettings";
    public string CloudName { get; set; } = null!;
    public string ApiKey { get; set; } = null!;
    public string ApiSecret { get; set; } = null!;

}