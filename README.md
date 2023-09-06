## AuthMe .NET Core Class Library
This is a .NET Standard class library that provides functionality for implementing HTTP Basic Authentication in your .NET applications. 
HTTP Basic Authentication is a simple method for securing HTTP resources by requiring a username and password from the client.

#Features
Easily integrate HTTP Basic Authentication into your .NET applications.
Validate credentials against of a user.Compatibility with .NET Standard, making it suitable for a wide range of .NET projects.

Usage

```cs
 services.Configure<BasicAuthSettings>(Configuration.GetSection("BasicAuth"))
 services.AddBasicAuthentication();
 ```
            

