using System;
using Xamarin.Forms;
using System.Collections.Generic;
using Acr.DeviceInfo;

namespace bfbnet
{
	public class ConceptartPage : ContentPage
	{
		public ConceptartPage (BeyondRootModel Page) 
		{
			NavigationPage.SetBackButtonTitle (this, "");
			this.Title = Page.pageName;

			this.BackgroundColor = Color.FromRgb (70, 124, 154);

			StackLayout pageView = new StackLayout { VerticalOptions = LayoutOptions.FillAndExpand };

			ScrollView scrollContent = new ScrollView { Content = pageView, VerticalOptions = LayoutOptions.FillAndExpand };

			WrapLayout layout = new WrapLayout () {
				Spacing = 0,
				Orientation = StackOrientation.Horizontal,
				Padding = new Thickness(0,0,0,0)
			};

			List<Frame> imageList = new List<Frame> ();
			int i = 0;
			int imgwidth = 0;
			if ((DeviceInfo.Instance.ScreenWidth % 3) == 0) {
				imgwidth = (DeviceInfo.Instance.ScreenWidth / 3);
			}
			if ((DeviceInfo.Instance.ScreenWidth % 4) == 0) {
				imgwidth = (DeviceInfo.Instance.ScreenWidth / 3);
			}
			if ((DeviceInfo.Instance.ScreenWidth % 6) == 0) {
				imgwidth = (DeviceInfo.Instance.ScreenWidth / 3);
			}
			if ((DeviceInfo.Instance.ScreenWidth % 8) == 0) {
				imgwidth = (DeviceInfo.Instance.ScreenWidth / 3);
			}
			if ((DeviceInfo.Instance.ScreenWidth % 10) == 0) {
				imgwidth = (DeviceInfo.Instance.ScreenWidth / 3);
			}
			foreach (BeyondImage image in Page.conceptart) {
				imageList.Add(new Frame () {
					WidthRequest = imgwidth,
					HeightRequest = imgwidth,
					OutlineColor = Color.FromHex("007DA1"),

					HasShadow = false,
					Padding = 0,
					Content = new Image () { 
						Source = ImageSource.FromStream (() => new System.IO.MemoryStream (image.image)), 
						Aspect = Aspect.AspectFill
					}
				});
				imageList [i].GestureRecognizers.Add (new TapGestureRecognizer {
					Command = new Command (() => {
						Navigation.PushAsync(new ContentPage () {
							Title = "View Image",
							BackgroundColor = Color.White,
							ToolbarItems = {
								new ToolbarItem ("Download", null, async () => {
									Device.OpenUri (new Uri(image.url));
								})
							},
							Content = new Image () { 
								HorizontalOptions = LayoutOptions.Fill,
								VerticalOptions = LayoutOptions.Fill, 
								Source = ImageSource.FromStream (() => new System.IO.MemoryStream (image.image)), 
								Aspect = Aspect.AspectFit
							}
						});
					}),
					NumberOfTapsRequired = 1
				});
				layout.Children.Add(imageList[i]);
				i++;
			}

			pageView.Children.Add (layout);

			this.Content = scrollContent;

		}
	}
}

