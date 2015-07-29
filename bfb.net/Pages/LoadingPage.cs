using System;
using Xamarin.Forms;
using Connectivity.Plugin;

namespace bfbnet
{
	public class LoadingPage : ContentPage
	{
		/* LoadingPage
		 * Produces a page with a loading indicator,
		 * which checks for an internet connection,
		 * downloads the latest version of the app data
		 * (if the download version is outdated) and
		 * opens the 'application' with the data. If no
		 * internet connection is detected but local data exists
		 * the application is opened with the outdated data. If 
		 * there is no internet connection and no data present on
		 * the device (unlikely) an error message is displayed.
		 */
		public LoadingPage ()
		{
			NavigationPage.SetBackButtonTitle (this, "Back");
		//Create a new activity indicator
			ActivityIndicator progressIndicator = new ActivityIndicator () {
				VerticalOptions = LayoutOptions.CenterAndExpand,
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				IsEnabled = true,
				IsRunning = true,
				Color = Color.White,
				Scale = 2.5
			};
			Image backgroundImage = new Image () {
				Source = "1.jpg",
				Aspect = Aspect.AspectFill
			};
			//Create a new stack layout
			StackLayout pageLayout = new StackLayout () {
				VerticalOptions = LayoutOptions.Center,
				HorizontalOptions = LayoutOptions.Center,
				Spacing = 15
			};
			//Create a new relative layout for the background and stuff
			RelativeLayout layout = new RelativeLayout ();

			layout.Children.Add (backgroundImage, Constraint.Constant (0), Constraint.Constant (0), 
				Constraint.RelativeToParent ((parent) => {
					return parent.Width;
				}), 
				Constraint.RelativeToParent ((parent) => {
					return parent.Height;
				}));

			layout.Children.Add (pageLayout, Constraint.Constant (0), Constraint.Constant (0), 
				Constraint.RelativeToParent ((parent) => {
					return parent.Width;
				}), 
				Constraint.RelativeToParent ((parent) => {
					return parent.Height;
				}));
			//Create a new label to store the current progress tatus
			Label progressStatusLabel = new Label () {
				VerticalOptions = LayoutOptions.CenterAndExpand,
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				FontFamily = Device.OnPlatform ("Orbitron",null,null),
				TextColor = Color.White
			};
			//Pop the activity indicator and the label into the stack layout
			pageLayout.Children.Add (progressIndicator);
			pageLayout.Children.Add (progressStatusLabel);
			//Set the stacklayout to be the content of this page
			this.Content = layout;
			this.Title = "Loading...";
			//Check for an Internet connection
			//Invoke the method asynchronusly on the main thread
			Device.BeginInvokeOnMainThread (async () => {
				if (CrossConnectivity.Current.IsConnected) {
					progressStatusLabel.Text = "Internet connection detected";
					//Check that the site is accessible
					bool result = await CrossConnectivity.Current.IsRemoteReachable("http://beyondfleshandbloodgame.com");
					if(result){
						//An internet connection has been detected, therefore
						//check the local copy of the data against the server.
						if(await BeyondFileStorage.CheckJSONExists ()) {
							progressStatusLabel.Text = "Local copy of the data exists";
							//File exists therefore check the file against the server data
							progressStatusLabel.Text = "Retrieving remote hash";
							String remoteHash = await BeyondNetwork.DownloadSHAHash ();
							//Obtain the local file and store it as string LocalJSON
							String LocalJSON = await BeyondFileStorage.ReadLocalJSON ();
							if(BeyondUtility.CompareMD5(remoteHash, LocalJSON)) {
								progressStatusLabel.Text = "No changes detected. Starting application";
								//File matches the server, therefore do not download
								//a new file and start the application using the local copy
								BeyondRootModel[] beyondModel = BeyondUtility.ConvertJSONToObjectModel(LocalJSON);
								await Navigation.PushAsync(new MenuPage (beyondModel));
								Navigation.RemovePage(this);
							} else {
								//File does not exist therefore download a new copy of the data
								//and launch the application
								progressStatusLabel.Text = "Changes detected. Retrieving latest changes";
								await BeyondFileStorage.CreateLocalJSON (await BeyondNetwork.DownloadJSONData ());
								if(await BeyondFileStorage.CheckJSONExists ()) {
									progressStatusLabel.Text = "Starting application";
									//The file was downloaded successfully, read the file and launch the application
									BeyondRootModel[] beyondModel = BeyondUtility.ConvertJSONToObjectModel(LocalJSON);
									await Navigation.PushAsync(new MenuPage (beyondModel));
									Navigation.RemovePage(this);
								} else {
									//The file was not downloaded, successfully therefore display an error message
									await this.DisplayAlert("Error","The file could not be created. Please check that you have enough disk space and try again","Ok");
								}
							}
						} else {
							await this.DisplayAlert("Error","Our website could not be reached. Trying local copy","Ok");
							progressStatusLabel.Text = "Site unreachable";
							if(await BeyondFileStorage.CheckJSONExists ()) {
								progressStatusLabel.Text = "Starting application";
								//The file was downloaded successfully, read the file and launch the application
								String LocalJSON = await BeyondFileStorage.ReadLocalJSON();
								BeyondRootModel[] beyondModel = BeyondUtility.ConvertJSONToObjectModel(LocalJSON);
								await Navigation.PushAsync(new MenuPage (beyondModel));
								Navigation.RemovePage(this);
							} else {
								progressStatusLabel.Text = "No local copy";
								//The file does not exist therefore no data is present.
								await this.DisplayAlert("Error","Our site is unreachable and you do not have a local copy of the data. Please try again in 10 minutes","Ok");
							}
						}
					} else {
						//File does not exist therefore download a new copy of the data
						//and launch the application
						progressStatusLabel.Text = "Downloading data";
						await BeyondFileStorage.CreateLocalJSON (await BeyondNetwork.DownloadJSONData ());
						if(await BeyondFileStorage.CheckJSONExists ()) {
							progressStatusLabel.Text = "Starting Application";
							//The file was downloaded successfully, read the file and launch the application
							String LocalJSON = await BeyondFileStorage.ReadLocalJSON();
							BeyondRootModel[] beyondModel = BeyondUtility.ConvertJSONToObjectModel(LocalJSON);
							await Navigation.PushAsync(new MenuPage (beyondModel));
							Navigation.RemovePage(this);
						} else {
							//The file was not downloaded, successfully therefore display an error message
							progressStatusLabel.Text = "Failed";
							await this.DisplayAlert("Error","The file could not be created. Please check that you have enough disk space and try again","Ok");
						}
					}
				} else {
					//An internet connection has not been detected, therefore
					//check if there is a local copy of the data.
					progressStatusLabel.Text = "Internet connection not detected";
					if(await BeyondFileStorage.CheckJSONExists ()) {
						progressStatusLabel.Text = "Starting application";
						//The file was downloaded successfully, read the file and launch the application
						String LocalJSON = await BeyondFileStorage.ReadLocalJSON();
						BeyondRootModel[] beyondModel = BeyondUtility.ConvertJSONToObjectModel(LocalJSON);
						await Navigation.PushAsync(new MenuPage (beyondModel));
						Navigation.RemovePage(this);
					} else {
						progressStatusLabel.Text = "Internet connection not detected";
						//The file does not exist therefore no data is present.
						await this.DisplayAlert("Error","An internet connection is required to install this application","Ok");
					}
				}
			});
		}
	}
}

