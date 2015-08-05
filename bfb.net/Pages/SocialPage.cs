using System;
using Xamarin.Forms;

namespace bfbnet
{
	public class SocialPage : ContentPage
	{
		public SocialPage ()
		{
			StackLayout buttonList = new StackLayout {
				Spacing = 15,
				Padding = new Thickness(15,15,15,0),
				VerticalOptions = LayoutOptions.CenterAndExpand
			};

			ScrollView buttonScroll = new ScrollView {
				VerticalOptions = LayoutOptions.CenterAndExpand
			};

			NavigationPage.SetBackButtonTitle (this, "Back");

			Image backgroundImage = new Image () {
				Source = "bg3.jpg",
				Aspect = Aspect.AspectFill
			};

			RelativeLayout layout = new RelativeLayout ();

			layout.Children.Add (backgroundImage, Constraint.Constant (0), Constraint.Constant (0), 
				Constraint.RelativeToParent ((parent) => {
					return parent.Width;
				}), 
				Constraint.RelativeToParent ((parent) => {
					return parent.Height;
				}));

			layout.Children.Add (buttonScroll, Constraint.Constant (0), Constraint.Constant (0), 
				Constraint.RelativeToParent ((parent) => {
					return parent.Width;
				}), 
				Constraint.RelativeToParent ((parent) => {
					return parent.Height;
				}));

			buttonScroll.Content = buttonList;
			this.Content = layout;
			this.Title = "Social Media";

			buttonList.Children.Add (new BeyondButton {
				Text = "Facebook",
				Command = new Command (async c => {
					Device.OpenUri(new Uri("https://www.facebook.com/beyondfleshandblood1"));
				})
			});
			buttonList.Children.Add (new BeyondButton {
				Text = "Google+",
				Command = new Command (async c => {
					Device.OpenUri(new Uri("https://plus.google.com/+Beyondfleshandbloodgame/posts"));
				})
			});
			buttonList.Children.Add (new BeyondButton {
				Text = "Twitter",
				Command = new Command (async c => {
					Device.OpenUri(new Uri("https://twitter.com/beyondfbgame"));
				})
			});
			buttonList.Children.Add (new BeyondButton {
				Text = "Pintrest",
				Command = new Command (async c => {
					Device.OpenUri(new Uri("https://uk.pinterest.com/pixelbomb/"));
				})
			});
			buttonList.Children.Add (new BeyondButton {
				Text = "IndieDB",
				Command = new Command (async c => {
					Device.OpenUri(new Uri("http://www.indiedb.com/games/beyond-flesh-and-blood"));
				})
			});
		}
	}
}

