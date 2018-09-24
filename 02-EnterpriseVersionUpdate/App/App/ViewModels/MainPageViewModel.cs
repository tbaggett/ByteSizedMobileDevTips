using App.Services;
using App.Services.CheckForUpdates;
using App.Views;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace App.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        public MainPageViewModel()
        {
            AppVersionService = DependencyService.Get<IAppVersionService>();
            CheckForUpdatesService = new CheckForUpdatesService();
            CheckForUpdatesCommand = new Command(CheckForUpdates);
        }

        public string AppShortVersionInfo => $"App Short Version: {AppVersionService.ShortVersion}";

        public string AppVersionInfo => $"App Version: {AppVersionService.Version}";

        public ICommand CheckForUpdatesCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private IAppVersionService AppVersionService { get; } 
        private CheckForUpdatesService CheckForUpdatesService { get; }

        private async void CheckForUpdates(object obj)
        {
            var response = CheckForUpdatesService.IsUpdateAvailable();
            if (response.IsUpdateAvailable)
            {
                Application.Current.MainPage = new UpdateAvailable
                {
                    IsUpdateRequired = response.IsUpdateRequired
                };
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("No Updates Available", 
                                                                "Congrats, you have the latest version!",
                                                                "Okay");
            }
        }
    }
}
