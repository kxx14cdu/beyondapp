using System;
using Xamarin.Forms;
using System.IO;
using System.Net;
using System.Collections.Generic;
using Acr.DeviceInfo;

namespace bfbnet
{
	public class CharacterViewPage : ContentPage
	{
		public CharacterViewPage (BeyondCharacterModel Page)
		{
			NavigationPage.SetBackButtonTitle (this, " ");
			this.Title = Page.pageName;

			RelativeLayout pageLayout = new RelativeLayout ();

			StackLayout pageView = new StackLayout () { VerticalOptions = LayoutOptions.FillAndExpand, HorizontalOptions = LayoutOptions.FillAndExpand };

			ScrollView scrollContent = new ScrollView { Content = pageView, VerticalOptions = LayoutOptions.FillAndExpand, HorizontalOptions = LayoutOptions.FillAndExpand };

			HtmlView test = new HtmlView ();

			ContentView slideContentHolder = new ContentView () { BackgroundColor = new Color (0, 0, 0, 0.5), Padding = new Thickness(10,10,10,0) };

			ContentView imageHolder = new ContentView () { BackgroundColor = new Color (0, 0, 0, 0.5), Padding = new Thickness(0,15,0,15) };

			Image storyImage = new Image () { HeightRequest = 450, Aspect = Aspect.AspectFit };

			Image backgroundImage = new Image () { Source = "bg2.jpg", Aspect = Aspect.AspectFill };

			Byte[] ImageBase64 = System.Convert.FromBase64String(Page.characterRightHandSideImage);

			storyImage.Source = ImageSource.FromStream (() => new System.IO.MemoryStream (ImageBase64));

			slideContentHolder.Content = test;
			imageHolder.Content = storyImage;

			test.Text = Page.characterDescription;

			pageLayout.Children.Add (backgroundImage, Constraint.Constant (0), Constraint.Constant (0), 
				Constraint.RelativeToParent ((parent) => {
					return parent.Width;
				}), 
				Constraint.RelativeToParent ((parent) => {
					return parent.Height;
				})
			);

			pageLayout.Children.Add (scrollContent, Constraint.Constant (0), Constraint.Constant (0), 
				Constraint.RelativeToParent ((parent) => {
					return parent.Width;
				}), 
				Constraint.RelativeToParent ((parent) => {
					return parent.Height;
				})
			);

			pageView.Children.Add (imageHolder);
			pageView.Children.Add (slideContentHolder); 

			ContentView statsHolder = new ContentView () { BackgroundColor = new Color (0, 0, 0, 0.5), Padding = new Thickness(5,5,5,5)};

			HtmlView stats = new HtmlView () {
				Text = BeyondUtility.stripUnRequiredTags(Page.characterStats),
				XAlign = TextAlignment.Center,
				YAlign = TextAlignment.Center
			};

			statsHolder.Content = stats;

			pageView.Children.Add (statsHolder);


			if (Page.characterScreenshotsConceptArt != null) {
				ContentView labelView = new ContentView {
					BackgroundColor = new Color (0, 0, 0, 0.5), Padding = 10 
				};
				Label text = new Label () {
					FontFamily = Device.OnPlatform("Orbitron",null,null),
					TextColor = Color.White,
					Text = "Tap an image to view a larger version",
					XAlign = TextAlignment.Center
				};

				labelView.Content = text;
				ContentView imagesHolder = new ContentView () { BackgroundColor = new Color (0, 0, 0, 0.5), Padding = new Thickness(0,0,0,0) };

				WrapLayout layout = new WrapLayout () {
					Spacing = 0,
					Orientation = StackOrientation.Horizontal
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
				foreach (BeyondImage image in Page.characterScreenshotsConceptArt) {
					imageList.Add(new Frame () {
						HeightRequest = imgwidth,
						WidthRequest = imgwidth,
						HasShadow = false,
						Padding = 0,
						OutlineColor = Color.FromHex("007DA1"),
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
				imagesHolder.Content = layout;
				pageView.Children.Add (labelView);
				pageView.Children.Add (imagesHolder);
			}

			this.Content = pageLayout;
		}
	}
}

