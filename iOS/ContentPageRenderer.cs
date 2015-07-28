using System;
using Xamarin.Forms;
using bfbnet.iOS;
using bfbnet;
using Xamarin.Forms.Platform.iOS;
using UIKit;

[assembly: ExportRenderer(typeof(ContentPage), typeof(ContentPageRenderer_iOS))]
namespace bfbnet.iOS
{
	public class ContentPageRenderer_iOS : PageRenderer
	{
		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(false);

			UIGraphics.BeginImageContext(this.View.Frame.Size);
			UIImage i = UIImage.FromFile("Background.png");
			i = i.Scale(this.View.Frame.Size);

			this.View.BackgroundColor = UIColor.FromPatternImage(i);

		}
	}
}