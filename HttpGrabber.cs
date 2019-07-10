using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace HttpGrabberFunctions
{
    public static class HttpHelper
    {
        private static HttpClient client = new HttpClient();

        public static async Task<string> MakeGetRequest(string url)
        {
            try
            {
                //asynchronously makes a get request to the link we want to
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                //stringfy the response
                string responseBody = await response.Content.ReadAsStringAsync();
                return responseBody;
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
                throw;
            }

        }
        public static async Task<string> MakeAuthenticatedGetRequest(string url, string Token)
        {
            try
            {
                //Sets securely our canvas token to our http header
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Token);
                //asynchronously makes a get request to the link we want to
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                //stringfy the response
                string responseBody = await response.Content.ReadAsStringAsync();
                return responseBody;
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
                throw;
            }

        }
    }
}
