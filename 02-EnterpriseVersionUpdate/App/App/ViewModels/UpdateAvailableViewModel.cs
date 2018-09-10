using App.Services.CheckForUpdates;
using App.Views;
using PropertyChanged;
using System;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace App.ViewModels
{
    public class UpdateAvailableViewModel : INotifyPropertyChanged
    {
        // Don't forget to add the Fody PropertyChanged NuGet package to handle update notifications for our properties

        public bool IsUpdateRequired { get; set; } = true;
        public WebViewSource HtmlSource { get; set; }
        public ICommand PageAppearingCommand { get; }
        public ICommand InstallUpdateCommand { get; }
        public ICommand IgnoreUpdateCommand { get; }

        [DependsOn(nameof(IsUpdateRequired))]
        public string Instructions
        {
            get
            {
                using (var writer = new StringWriter())
                {
                    writer.WriteLine("A new version of our app is available!" +
                                 Environment.NewLine);

                    if (IsUpdateRequired)
                    {
                        writer.WriteLine("This update is required. Please press install to proceed.");
                    }
                    else
                    {
                        writer.WriteLine("Select \"Install\" to update or \"Later\" to continue with the current version.");
                    }

                    writer.WriteLine(Environment.NewLine + "You may relaunch the app after the update is completed.");

                    return writer.ToString();
                }
            }
        }

        [DependsOn(nameof(IsUpdateRequired))]
        public bool IsUpdateOptional => !IsUpdateRequired;

        // INotifyPropertyChanged interface implementation - Unused warning can be ignored due to Fody PropertyChanged handling it
        public event PropertyChangedEventHandler PropertyChanged;

        public UpdateAvailableViewModel()
        {
            // This should be initialized by an IoC container and passed in as a constructor parameter
            CheckForUpdatesService = new CheckForUpdatesService();

            PageAppearingCommand = new Command<bool>(async (bool isUpdateRequired) =>
            {
                IsUpdateRequired = isUpdateRequired;
                await CreateUpdateInfoHtmlContentAsync();
            });

            InstallUpdateCommand = new Command(InstallUpdate);
            IgnoreUpdateCommand = new Command(IgnoreUpdate);
        }

        private CheckForUpdatesService CheckForUpdatesService { get; }

        private void InstallUpdate(object obj)
        {
            if (!string.IsNullOrWhiteSpace(_updateUri))
            {
                HtmlSource = new UrlWebViewSource
                {
                    Url = _updateUri
                };
            }
        }

        private void IgnoreUpdate(object obj)
        {
            Application.Current.MainPage = new MainPage();
        }

        private string _updateUri;

        private async Task CreateUpdateInfoHtmlContentAsync()
        {
            // Retrieve update change details
            var response = CheckForUpdatesService.GetVersionChanges();
            _updateUri = response.UpdateLinkUri;

            // HtmlTextWriter isn't available in NetStandard 2.0, so we use a regular StringWriter
            using (var writer = new StringWriter())
            {
                // Add styling
                await writer.WriteAsync(@"
                    <style>
                        html { font-family: -apple-system, Arial, Verdana; color: #434343; font-size: 13px; }
                        body { -webkit-user-select: none; background-color: #ffffff; }
                        h1 { color: #0000ff; }
                        h2 { color: #0000ff; }
                        h3 { color: #0000ff; }
                        h4 { color: #0000ff; }
                    </style>");

                // Add content
                await writer.WriteAsync($" <p><b>Current version: {response.CurrentVersion}</b>");
                await writer.WriteAsync($" ({new DateTime(2018, 9, 1).ToShortDateString()})</p>");

                if (response.VersionChanges?.Count > 0)
                {
                    // Version entries are expected to be in newest to oldest order. 
                    // Show the latest version number and release date first.

                    var newest = response.VersionChanges[0];
                    await writer.WriteAsync($"<p><b>New version: {newest.Version}</b>");
                    await writer.WriteAsync($" ({((DateTime)newest.ReleaseDate).ToShortDateString()})</p>");

                    // Describe the other versions between our currently installed version and the latest version
                    await writer.WriteAsync("<h3>What's New:</h3>");
                    foreach (var version in response.VersionChanges)
                    {
                        await writer.WriteAsync($"<p><b>{version.Version}</b> " +
                                                $"({version.ReleaseDate.ToShortDateString()})</p>");

                        if (version.Changes?.Count > 0)
                        {
                            await writer.WriteAsync("<ul>");
                            foreach (var change in version.Changes)
                            {
                                await writer.WriteAsync($"<li>{change}</li>");
                            }
                            await writer.WriteAsync("</ul>");
                        }
                    }
                }

                var content = writer.ToString();

                Device.BeginInvokeOnMainThread(() =>
                {
                    HtmlSource = new HtmlWebViewSource
                    {
                        Html = content
                    };
                });
            }
        }
    }
}
