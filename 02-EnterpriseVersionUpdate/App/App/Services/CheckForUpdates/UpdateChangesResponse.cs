using System;
using System.Collections.Generic;

namespace App.Services.CheckForUpdates
{
    public class UpdateChangesResponse
    {
        public string CurrentVersion { get; set; }
        public string LatestVersion { get; set; }
        public DateTime LatestVersionReleaseDate { get; set; }
        public string UpdateLinkUri { get; set; }
        public List<VersionDetails> VersionChanges { get; set; }
    }

    public class VersionDetails
    {
        public string Version { get; set; }
        public DateTime ReleaseDate { get; set; }
        public List<string> Changes { get; set; }
    }
}
