## AuthMe .NET Core Class Library
This is a .NET Standard class library that provides functionality for implementing HTTP Basic Authentication in your .NET applications. 
HTTP Basic Authentication is a simple method for securing HTTP resources by requiring a username and password from the client.

#Features
Easily integrate HTTP Basic Authentication into your .NET applications.
Validate credentials against of a user.Compatibility with .NET Standard, making it suitable for a wide range of .NET projects.

Usage

```cs
 builder.Services.Configure<BasicAuthSettings>(Configuration.GetSection("BasicAuth"))
 builder.Services.AddBasicAuthentication();

app.UseAuthentication()

 ```
            

 Client Code
 ```cs

 using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

class Program
{
    static async Task Main()
    {
        // Base URL of the API you want to access
        string baseUrl = "https://api.example.com/";

        // Credentials for basic authentication
        string username = "your_username";
        string password = "your_password";

        // Create an HttpClient with a BaseAddress
        using (var client = new HttpClient())
        {
            // Set the BaseAddress for the HttpClient
            client.BaseAddress = new Uri(baseUrl);

            // Convert credentials to Base64
            string base64Credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{username}:{password}"));

            // Set the Authorization header with the Basic authentication token
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64Credentials);

            try
            {
                // Send an HTTP GET request (you can change this to a different HTTP method as needed)
                HttpResponseMessage response = await client.GetAsync("api/resource");

                // Check if the request was successful
                if (response.IsSuccessStatusCode)
                {
                    // Read the response content
                    string content = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("Response Content: " + content);
                }
                else
                {
                    Console.WriteLine("Request failed with status code: " + response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
}


 ```

