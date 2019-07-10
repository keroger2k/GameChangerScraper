using GameChangeScraper.Helpers;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace GameChangeScraper
{
    public class GCSession
    {
        private static CookieContainer _cookie = new CookieContainer();
        
        public GCSession()
        {
            this.LoginCSRF();
            this.Login();
        }

        private void LoginCSRF()
        {
            Get(GCLinks.BASE_LOGIN, GCLinks.BASE_URL, ref _cookie);

        }

        private void Login()
        {
            string username = "kyle.rogers@gmail.com";
            string password = "";
            string csrf = _cookie.GetCookies(new Uri(GCLinks.BASE_URL))[0].Value;
            string postData = "email=" + username + "&password=" + password + "&redirect=&csrfmiddlewaretoken=" + csrf + "&form=submit";
            Post(GCLinks.BASE_LOGIN_POST, postData, GCLinks.BASE_URL, _cookie);
        }

        public string GameList(string teamId)
        {
            return Get(string.Format("{0}{1}/schedule/games", GCLinks.BASE_URL, teamId), GCLinks.BASE_URL, ref _cookie);
        }

        public string GameStats(string gameLink)
        {
            return Get(string.Format("{0}/{1}/stats", GCLinks.BASE_URL, gameLink), GCLinks.BASE_URL, ref _cookie);
        }

        private string Get(string url, string referer, ref CookieContainer cookies)
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = "GET";
            req.CookieContainer = cookies;
            req.UserAgent = "";
            req.Referer = referer;

            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            cookies.Add(resp.Cookies);

            string pageSrc;
            using (StreamReader sr = new StreamReader(resp.GetResponseStream()))
            {
                pageSrc = sr.ReadToEnd();
            }
            return pageSrc;
        }

        private bool Post(string url, string postData, string referer, CookieContainer cookies)
        {
            string key = "Kyle Rogers";

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = "POST";
            req.CookieContainer = cookies;
            req.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/67.0.3396.99 Safari/537.36";
            req.Referer = referer;
            req.ContentType = "application/x-www-form-urlencoded";
            req.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8";

            Stream postStream = req.GetRequestStream();
            byte[] postBytes = Encoding.ASCII.GetBytes(postData);
            postStream.Write(postBytes, 0, postBytes.Length);
            postStream.Dispose();

            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            cookies.Add(resp.Cookies);

            StreamReader sr = new StreamReader(resp.GetResponseStream());
            string pageSrc = sr.ReadToEnd();
            sr.Dispose();
            return (pageSrc.Contains(key));
        }

    }

}
