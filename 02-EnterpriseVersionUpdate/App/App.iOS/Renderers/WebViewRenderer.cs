using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

// Adapted from https://stackoverflow.com/questions/43936015/xamarin-forms-webview-showing-slight-black-line-at-bottom

[assembly: ExportRenderer(typeof(WebView), typeof(App.iOS.Renderers.WebViewRenderer))]
namespace App.iOS.Renderers
{
    public class WebViewRenderer : Xamarin.Forms.Platform.iOS.WebViewRenderer
    {
        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            if (NativeView != null)
            {
                var webView = (UIWebView)NativeView;
                webView.Opaque = false;
                webView.BackgroundColor = UIColor.Clear;
            }
        }
    }
}