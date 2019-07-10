using System;
using System.Net;

namespace GameChangeScraper
{
    class Program
    {
        static void Main(string[] args)
        {
            CookieContainer myCookies = new CookieContainer();
            string mySrc = HttpMethods.Get("https://gc.com/login", "https://gc.com", ref myCookies);
            string username = "kyle.rogers@gmail.com";
            string password = "password";
            string csrf = myCookies.GetCookies(new Uri("http://gc.com"))[0].Value;
            string postData = "email=" + username + "&password=" + password + "&csrfmiddlewaretoken=" + csrf + "&form=submit";

            bool result = HttpMethods.Post("https://gc.com/login", postData, "https://gc.com", myCookies);
            if (result)
                Console.WriteLine("Valid!");
            else
                Console.WriteLine("Invalid!");
        }
    }
}
