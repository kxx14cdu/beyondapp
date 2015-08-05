using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.NetworkInformation;

namespace bfbnet
{
	/* BeyondNetwork
	 * Static helper methods for network access. All functions are threaded
	*/
	public class BeyondNetwork
	{
		/* DownloadSHAHash
		 * Using HttpClient connect to the webserver
		 * and download the current SHA256 hash of the content
		 * and return it as a string.
		 * Asynchronus method
		*/
		public static async Task<String> DownloadSHAHash () {
			//New HttpClient instance
			using (HttpClient client = new HttpClient ()) {
				//Set the base address and the headers
				client.BaseAddress = new Uri ("http://beyondfleshandbloodgame.com/");
				client.DefaultRequestHeaders.Accept.Clear ();
				client.DefaultRequestHeaders.Accept.Add (new MediaTypeWithQualityHeaderValue ("text/plain"));
				//Obtain the SHA256 hash
				HttpResponseMessage response = await client.GetAsync ("app-bson?opt=md5");
				//If successful
				if (response.IsSuccessStatusCode) {
					//Return the contents of the response
					return await response.Content.ReadAsStringAsync ();
				} else {
					return "";
				}
			}
		}


		/* DownloadJSONData
		 * Using HttpClient connect to the webserver
		 * and download the current JSON data and return it as a string.
		 * Asynchronus method
		*/
		public static async Task<String> DownloadJSONData () {
			//New HttpClient instance
			using (HttpClient client = new HttpClient ()) {
				//Set the base address and the headers
				client.BaseAddress = new Uri ("http://beyondfleshandbloodgame.com/");
				client.DefaultRequestHeaders.Accept.Clear ();
				client.DefaultRequestHeaders.Accept.Add (new MediaTypeWithQualityHeaderValue ("application/json"));
				//Obtain the application data
				HttpResponseMessage response = await client.GetAsync ("app-bson");
				//If successful
				if (response.IsSuccessStatusCode) {
					//Return the contents of the response
					return await response.Content.ReadAsStringAsync ();
				} else {
					return "";
				}
			}
		}
	}
}

