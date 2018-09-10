namespace App.Services.CheckForUpdates
{
    public class LatestVersionResponse
    {
        public string LatestVersion { get; set; }
        public bool IsUpdateRequired { get; set; }
    }
}
