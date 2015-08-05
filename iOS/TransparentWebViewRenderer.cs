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
using CoreGraphics;

[assembly: ExportRenderer(typeof(TransparentWebView), typeof(TransparentWebViewRenderer))]
namespace bfbnet.iOS
{
	public class TransparentWebViewRenderer : WebViewRenderer
	{

		protected override void OnElementChanged(VisualElementChangedEventArgs e)
		{
			base.OnElementChanged(e);
			this.Opaque = false;
			this.ScalesPageToFit = true;
			this.BackgroundColor = UIColor.Clear;
			this.AutoresizingMask = UIViewAutoresizing.FlexibleDimensions;
			this.ClipsToBounds = true;
			this.ScrollView.Frame = this.Bounds;
			if (e.OldElement != null) {
				Frame = new CGRect (0, 0, UIScreen.MainScreen.Bounds.Width, UIScreen.MainScreen.Bounds.Height);
			}
		}

	}
}