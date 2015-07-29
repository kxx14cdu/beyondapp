using System;
using Xamarin.Forms;
using System.Text;
using PCLCrypto;
using PCLStorage;
using Newtonsoft.Json;
using Xamarin.Forms.Labs;

namespace bfbnet
{
	public interface IBaseUrl { string Get(); }

	/* BeyondFUtility
	 * Static utility helper methods
	*/
	public class BeyondUtility
	{
		/* StringGen
		 * Using the PCLCrypto library a newMD5 algorithm provider
		 * is intialised. The string input is converted to a binary array
		 * and then hashed using theMD5 algorithm. This is then returned
		 * as a hexadecimal string representation
		*/
		public static string MD5Gen (String input) {
			//Create MD5 Algorithm provider
			var provider = WinRTCrypto.HashAlgorithmProvider.OpenAlgorithm (HashAlgorithm.Md5);
			//Convert the input string to binary using UTF8 encoding
			var inputBinary = WinRTCrypto.CryptographicBuffer.ConvertStringToBinary (input, Encoding.UTF8);
			//Generate a hash of the data
			var hash = provider.HashData (inputBinary);
			//Return the hash as a hexadecimal string
			return WinRTCrypto.CryptographicBuffer.EncodeToHexString (hash);
		}
	
		/* CompareSHA
		 * Simple function to compare the inputted remotehash string 
		 * with theMD5 hash of the local JSON file. Returns
		 * true/false depending on if they match or not.
		*/
		public static bool CompareMD5 (string remotehash, String localjson) {
			//Generate aMD5 hash of the local JSON file
			String localhash = MD5Gen (localjson);
			//"none" is returned if there is no local JSON file, in this case
			//say that the local file does not match the server in order to
			//force an initial or new download of the data.
			if (localhash != "none") {
				if (localhash == remotehash)
					return true;
				else 
					return false;
			} else {
				return false;
			}
		}

		/* ConvertJSONToObjectModel
		 * Returns an object of type BeyondRootObject deserialized
		 * from the input JSON String using Newtonsoft JSON.NET
		*/
		public static BeyondRootModel[] ConvertJSONToObjectModel (String JSON) {
			return JsonConvert.DeserializeObject<BeyondRootModel[]> (JSON);
		}

	}
}

