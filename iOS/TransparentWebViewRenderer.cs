using bfbnet.iOS;
using bfbnet;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using Foundation;

[assembly: ExportRenderer(typeof(TransparentWebView), typeof(TransparentWebViewRenderer))]
namespace bfbnet.iOS
{
	public class TransparentWebViewRenderer : WebViewRenderer
	{
		public override void LoadHtmlString (string s, NSUrl baseUrl)  {
			baseUrl = new NSUrl (NSBundle.MainBundle.BundlePath, true);
			base.LoadHtmlString (s, baseUrl);
		}
		protected override void OnElementChanged(VisualElementChangedEventArgs e)
		{
			base.OnElementChanged(e);
			this.BackgroundColor = UIColor.Clear;
		}
	}
}