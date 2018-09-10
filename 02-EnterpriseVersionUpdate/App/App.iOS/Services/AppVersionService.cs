using App.iOS.Services;
using App.Services;
using Foundation;
using Xamarin.Forms;

[assembly: Dependency(typeof(AppVersionService))]
namespace App.iOS.Services
{
    public class AppVersionService : IAppVersionService
    {
        public string ShortVersion
            => NSBundle.MainBundle.ObjectForInfoDictionary("CFBundleShortVersionString").ToString();

        public string Version
            => NSBundle.MainBundle.ObjectForInfoDictionary("CFBundleVersion").ToString();
    }
}