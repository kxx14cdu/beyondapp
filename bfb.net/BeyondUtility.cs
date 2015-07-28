using System;
using Xamarin.Forms;
using System.Text;
using PCLCrypto;
using PCLStorage;

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
		*/
		public static bool CompareSHA (string remotehash) {
			string localhash = SHA256Gen (BeyondFileStorage.ReadLocalJSON());
			if (remotehash == localhash) {
				return true;
			} else {
				return false;
			}
		}

		/* ConvertJSONToObjectModel
		 * Returns an object of type BeyondRootObject deserialized
		 * from the 
		*/
		public static dynamic ConvertJSONToObjectModel (string JSON) {
			
		}

	}
}

