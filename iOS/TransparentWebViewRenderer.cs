using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text;
using bfbnet;
using bfbnet.iOS;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using Rectangle = Xamarin.Forms.Rectangle;

[assembly: ExportRenderer(typeof(TransparentWebView), typeof(TransparentWebViewRenderer))]
namespace bfbnet.iOS
{
	public class TransparentWebViewRenderer : WebViewRenderer
	{
		protected override void OnElementChanged(VisualElementChangedEventArgs e)
		{
			base.OnElementChanged(e);
			this.Opaque = false;
			this.LoadFinished += ResizeWebView;
			this.BackgroundColor = UIColor.Clear;
			this.ScrollView.ScrollEnabled = false;
		}

		private async void ResizeWebView(object sender, EventArgs eventArgs)
		{
			var isActuallyReallyFinishedLoadingForRealz = false;
			while (!isActuallyReallyFinishedLoadingForRealz)
			{
				var result = this.EvaluateJavascript("(function (){return true})();");
				bool.TryParse(result, out isActuallyReallyFinishedLoadingForRealz);
			}

			var frame = this.Frame;
			frame.Height = 10;

			this.Frame = frame;

			var fittingSize = this.SizeThatFits(SizeF.Empty);
			frame.Size = fittingSize;

			this.Frame = frame;
			this.ScrollView.Frame = frame;

			var bounds = new Rectangle(Element.Bounds.X, Element.Bounds.Y, Element.Bounds.Width, frame.Height);
			await Element.LayoutTo(bounds);
			Element.HeightRequest = bounds.Height;
		}
	}
}