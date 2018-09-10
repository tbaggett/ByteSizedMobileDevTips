namespace App.Services.CheckForUpdates
{
    public class IsUpdateAvailableResponse
    {
        public IsUpdateAvailableResponse(bool isUpdateAvailable = false, 
                                         bool isUpdateRequired = false)
        {
            IsUpdateAvailable = isUpdateAvailable;
            IsUpdateRequired = isUpdateRequired;
        }

        public bool IsUpdateAvailable { get; }
        public bool IsUpdateRequired { get; }
    }
}
