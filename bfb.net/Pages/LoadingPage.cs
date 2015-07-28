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
			//Create a new activity indicator
			ActivityIndicator progressIndicator = new ActivityIndicator () {
				VerticalOptions = LayoutOptions.Center,
				HorizontalOptions = LayoutOptions.Center,
				IsEnabled = true,
				IsRunning = true
			};
			//Create a new label to store the current progress tatus
			Label progressStatusLabel = new Label () {
				VerticalOptions = LayoutOptions.Center,
				HorizontalOptions = LayoutOptions.Center
			};
			//Create a new stack layout
			StackLayout pageLayout = new StackLayout () {
				VerticalOptions = LayoutOptions.Center,
				HorizontalOptions = LayoutOptions.Center,
			};
			//Pop the activity indicator and the label into the stack layout
			pageLayout.Children.Add (progressIndicator);
			pageLayout.Children.Add (progressStatusLabel);
			//Set the stacklayout to be the content of this page
			this.Content = pageLayout;
			//Check for an Internet connection
			//Invoke the method asynchronusly on the main thread
			Device.BeginInvokeOnMainThread (async () => {
				if (CrossConnectivity.Current.IsConnected) {
					progressStatusLabel.Text = "Internet connection detected";
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
							foreach(var item in beyondModel) {
								System.Diagnostics.Debug.WriteLine(item.pageName);
							}
						} else {
							//File does not exist therefore download a new copy of the data
							//and launch the application
							progressStatusLabel.Text = "Changes detected. Retrieving latest changes";
							await BeyondFileStorage.CreateLocalJSON (await BeyondNetwork.DownloadJSONData ());
							if(await BeyondFileStorage.CheckJSONExists ()) {
								progressStatusLabel.Text = "Starting application";
								//The file was downloaded successfully, read the file and launch the application
								BeyondRootModel[] beyondModel = BeyondUtility.ConvertJSONToObjectModel(LocalJSON);
								foreach(var item in beyondModel) {
									System.Diagnostics.Debug.WriteLine(item.pageName);
								}
							} else {
								//The file was not downloaded, successfully therefore display an error message
								await this.DisplayAlert("Error","The file could not be created. Please check that you have enough disk space and try again","Ok");
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
							foreach(var item in beyondModel) {
								System.Diagnostics.Debug.WriteLine(item.pageName);
							}
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
						foreach(var item in beyondModel) {
							System.Diagnostics.Debug.WriteLine(item.pageName);
						}
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

