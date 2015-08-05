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

[assembly: ExportRenderer(typeof(HtmlView), typeof(HtmlViewRenderer))]
namespace bfbnet.iOS
{
	public class HtmlViewRenderer : LabelRenderer
	{
		protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
		{
			base.OnElementChanged (e);

			if (e.NewElement != null) 
			{
				var element = (HtmlView)e.NewElement;

				if (element.Text != null) 
				{
					NSError error = null;
					var attributedString = new NSAttributedString (NSData.FromString (e.NewElement.Text), new NSAttributedStringDocumentAttributes { DocumentType = NSDocumentType.HTML }, ref error);
					this.Control.AttributedText = attributedString;
					this.Control.TextColor = UIColor.White;
					this.Control.Font = UIFont.FromName ("Orbitron", 16);
					this.Control.AdjustsFontSizeToFitWidth = true;
				}
			}
		}
	}
}

