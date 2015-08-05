using System;
using Xamarin.Forms;

namespace bfbnet
{
	public class CharacterPage : ContentPage
	{
		public CharacterPage (BeyondRootModel Page)
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
			this.Title = Page.pageName;

			foreach (var character in Page.characters) {
					buttonList.Children.Add (new BeyondButton {
						Text = character.pageName,
						Command = new Command (async c => {
							await Navigation.PushAsync (new CharacterViewPage (character));
						})
				});
			}
		}
	}
}

