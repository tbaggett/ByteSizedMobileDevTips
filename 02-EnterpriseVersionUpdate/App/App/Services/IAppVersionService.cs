namespace App.Services
{
    public interface IAppVersionService
    {
        string ShortVersion { get; }
        string Version { get; }
    }
}
