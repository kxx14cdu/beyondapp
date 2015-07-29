using System;
using Xamarin.Forms;

namespace bfbnet
{
	public class MenuPage : ContentPage
	{
		public MenuPage (BeyondRootModel[] Pages)
		{
			StackLayout buttonList = new StackLayout {
				Spacing = 15,
				VerticalOptions = LayoutOptions.Center
			};

			Image backgroundImage = new Image () {
				Source = "1.jpg",
				Aspect = Aspect.AspectFill
			}; 

			Image logo = new Image () {
				Source = "logo.png",
				Aspect = Aspect.AspectFit
			};

			RelativeLayout layout = new RelativeLayout ();

			layout.Children.Add (backgroundImage, Constraint.Constant (0), Constraint.Constant (0), 
				Constraint.RelativeToParent ((parent) => {
					return parent.Width;
				}), 
				Constraint.RelativeToParent ((parent) => {
					return parent.Height;
				}));

			layout.Children.Add (buttonList, Constraint.Constant (0), Constraint.Constant (0), 
				Constraint.RelativeToParent ((parent) => {
					return parent.Width;
				}), 
				Constraint.RelativeToParent ((parent) => {
					return parent.Height;
				}));

			this.Content = layout;

			this.Title = "Main Menu";

			buttonList.Children.Add (logo);

			foreach (var Page in Pages) {
				switch(Page.pageType) {
				case "text":
					buttonList.Children.Add (new BeyondButton {
						Text = Page.pageName,
						Command = new Command (async c => {
							await Navigation.PushAsync (new StoryPage (Page));
						})
					});
					break;
				case "character_list":
					buttonList.Children.Add (new BeyondButton {
						Text = Page.pageName,
						Command = new Command (async c => {
							await Navigation.PushAsync (new CharacterPage (Page));
						})
					});
					break;					
				default:
					buttonList.Children.Add (new BeyondButton {
						Text = Page.pageName
					});
					break;
				}
			}
		}
	}
}

