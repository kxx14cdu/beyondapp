using System;
using Xamarin.Forms;
using System.Text;
using PCLCrypto;
using PCLStorage;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace bfb.net
{
	/* BeyondFUtility
	 * Static utility helper methods
	*/
	public class BeyondUtility
	{
		/* SHA256Gen
		 * Using the PCLCrypto library a new SHA256 algorithm provider
		 * is intialised. The string input is converted to a binary array
		 * and then hashed using the SHA256 algorithm. This is then returned
		 * as a hexadecimal string representation
		*/
		public static string SHA256Gen (string input) {
			//Create a SHA256 Algorithm provider
			var provider = WinRTCrypto.HashAlgorithmProvider.OpenAlgorithm (HashAlgorithm.Sha256);
			//Convert the input string to binary using UTF8 encoding
			var inputBinary = WinRTCrypto.CryptographicBuffer.ConvertStringToBinary (input, Encoding.UTF8);
			//Generate a hash of the data
			var hash = provider.HashData (inputBinary);
			//Return the hash as a hexadecimal string
			return WinRTCrypto.CryptographicBuffer.EncodeToHexString (hash);
		}
	
		/* CompareSHA
		 * Simple function to compare the inputted remotehash string 
		 * with the SHA256 hash of the local JSON file. Returns
		 * true/false depending on if they match or not.

		public static bool CompareSHA (string remotehash) {
			//Generate a SHA256 hash of the local JSON file
			//string localhash = SHA256Gen (BeyondFileStorage.ReadLocalJSON());
			//"none" is returned if their is no local JSON file, in this case
			//say that the local file does not match the server in order to
			//force an initial or new download of the data.
			if (localhash == "none") {
				if (remotehash == localhash)
					return true;
				else 
					return false;
			} else {
				return false;
			}
		}*/

		/* ConvertJSONToObjectModel
		 * Returns an object of type BeyondRootObject deserialized
		 * from the input JSON String using Newtonsoft JSON.NET
		*/
		public static BeyondRootModel ConvertJSONToObjectModel (string JSON) {
			return JsonConvert.DeserializeObject<BeyondRootModel> (JSON);
		}

	}
}

