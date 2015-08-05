using System;
using Xamarin.Forms;

namespace bfbnet
{
	public class HtmlView : Label
	{
		public HtmlView ()
		{
			this.FontFamily = Device.OnPlatform ("Orbitron", null, null);
			this.TextColor = Color.White;
		}
	}
}

