using System;
using System.Threading.Tasks;
using PCLStorage;

namespace bfb.net
{
	/* BeyondFileStorage
	 * Static helper methods for file storage access/writing
	 * Functions will run threaded
	*/
	public class BeyondFileStorage
	{
		/* WriteLocalJSON
		 * Using the PCLStorage class, write the file 'data.json'
		 * to the devices local filesystem. Contents of file are
		 * the string JSON, and any file that is found will be
		 * overwrote.
		 * Asynchronus method
		*/
		public static async Task CreateLocalJSON (string JSON) {
			//Get the local filesystem app folder location
			IFolder localFolder = FileSystem.Current.LocalStorage;
			//Create a file called "data.json" overwritting any existing file
			IFile localFile = await localFolder.CreateFileAsync ("data.json",
				                  CreationCollisionOption.ReplaceExisting);
			//Write the string JSON to the file
			await localFile.WriteAllTextAsync (JSON);
		}

		/* ReadLocalJSON
		 * Using the PCLStorage class, read the file 'data.json'
		 * from the devices local filesystem.
		 * Asynchronus method

		public static async Task<string> ReadLocalJSON () {
			//Get the local filesystem app folder location
			IFolder localFolder = FileSystem.Current.LocalStorage;
			//Check if the local file exists
			if(FileSystem.Current.LocalStorage.CheckExistsAsync("data.json")) {
				//Obtain the file "data.json"
				IFile localFile = await localFolder.GetFileAsync ("data.json");
				//Return the contents of the file as a string
				return await localFile.ReadAllTextAsync ();
			} else {
				return "none";
			}
		}
*/
	}
}

