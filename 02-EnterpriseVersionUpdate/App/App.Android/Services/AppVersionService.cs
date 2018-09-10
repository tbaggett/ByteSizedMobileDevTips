using App.Droid.Services;
using App.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(AppVersionService))]
namespace App.Droid.Services
{
    public class AppVersionService : IAppVersionService
    {
        public string ShortVersion
        {
            get
            {
                var context = Android.App.Application.Context;
                return context.PackageManager.GetPackageInfo(context.PackageName, 0).VersionName;
            }
        }

        public string Version
        {
            get
            {
                var context = Android.App.Application.Context;
                var buildNumber = context.PackageManager.GetPackageInfo(context.PackageName, 0).VersionCode;
                return $"{ShortVersion}.{buildNumber}";
            }
        }
    }
}