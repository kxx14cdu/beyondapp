using System;
using System.Threading.Tasks;
using PCLStorage;

namespace bfb.net
{
	/* BeyondFileStorage
	 * Static helper methods for file storage access/writing
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
		public static async Task CreateLocalJSON(string JSON) {
			IFolder localFolder = FileSystem.Current.LocalStorage;
			IFile localFile = await localFolder.CreateFileAsync ("data.json",
				                  CreationCollisionOption.ReplaceExisting);
			await localFile.WriteAllTextAsync (JSON);
		}

		/* ReadLocalJSON
		 * Using the PCLStorage class, read the file 'data.json'
		 * from the devices local filesystem. d ddd
		 * Asynchronus method
		*/
		public static async Task<string> ReadLocalJSON() {
			IFolder localFolder = FileSystem.Current.LocalStorage;
			IFile localFile = await localFolder.GetFileAsync ("data.json");
			return await localFile.ReadAllTextAsync ();
		}

	}
}

