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
		public static string SHA256Gen(string input) {
			var provider = WinRTCrypto.HashAlgorithmProvider.OpenAlgorithm (HashAlgorithm.Sha256);
			var inputBinary = WinRTCrypto.CryptographicBuffer.ConvertStringToBinary (input, Encoding.UTF8);
			var hash = provider.HashData (inputBinary);
			return WinRTCrypto.CryptographicBuffer.EncodeToHexString (hash);
		}
	
		/* CompareSHA
		 * Simple function to compare the inputted remotehash string 
		 * with the SHA256 hash of the local JSON file. Returns
		 * true/false depending on if they match or not.
		*/
		public static bool CompareSHA(string remotehash, IFile localfile) {
			string localhash = SHA256Gen(localfile.ReadAllTextAsync ());
			if (remotehash == localhash) {
				return true;
			} else {
				return false;
			}
		}

	}
}

