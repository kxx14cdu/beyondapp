using bfbnet.Droid;
using bfbnet;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(TransparentWebView), typeof(TransparentWebViewRenderer))]

namespace bfbnet.Droid
{
	public class TransparentWebViewRenderer : WebViewRenderer
	{
		protected override void OnElementChanged(ElementChangedEventArgs<WebView> e)
		{
			base.OnElementChanged(e);
			this.Control.SetBackgroundColor(Android.Graphics.Color.Transparent);
		}
	}
}