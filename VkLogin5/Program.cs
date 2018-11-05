using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using HtmlAgilityPack;
using System.Text.RegularExpressions;
using AngleSharp;
using AngleSharp.Dom.Html;
using AngleSharp.Parser.Html;
using System.IO;
using AngleSharp.Network.Default;
using System.Collections.Specialized;
using System.Web;

namespace VkLogin5
{
    class Program
    {
        private const string _patternIph = @"input type=""hidden"" name=""ip_h"" value=""\w{18}""";
        private const string _patternLgh = @"input type=""hidden"" name=""lg_h"" value=""\w{18}""";
        public static string _htmlString = "";

        static void Main(string[] args)
        {
            Console.Write("Login:");
            var _login = Console.ReadLine();
            Console.Write("Password:");
            var _pass = Console.ReadLine();
            var _ip_h = "";
            var _lg_h = "";


            _htmlString = Code.GetCode();


            var _regexIph = new Regex(_patternIph);
            Match _matchesIph = _regexIph.Match(_htmlString);

            var _regexLgh = new Regex(_patternLgh);
            Match _matchesLgh = _regexLgh.Match(_htmlString);

            string _stringIph = _matchesIph.ToString();
            string _stringLgh = _matchesLgh.ToString();

            string[] _wordsIph = _stringIph.Split('"');
            string[] _wordsLgh = _stringLgh.Split('"');

            if (_wordsIph.Length > 0 & _wordsLgh.Length > 0)
            {
                _ip_h = _wordsIph[5];
                _lg_h = _wordsLgh[5];
            }

            else Console.WriteLine("ERROR ip_h or lg_h");


            Console.WriteLine(_htmlString);
            Console.WriteLine("*******************************");
            if (_matchesIph != null & _matchesLgh != null)
            {
                Console.WriteLine(_matchesIph);
                Console.WriteLine(_matchesLgh);
            }
            else Console.WriteLine("FFFFUUUUUCCCCCCKKKKK!!!!");

            Console.WriteLine("{0}::{1}", _ip_h, _lg_h);

            var webRequest = (HttpWebRequest)WebRequest.Create("https://login.vk.com/?act=login");
            //var webRequest = (HttpWebRequest)WebRequest.Create("https://login.vk.com/?act=login&soft=1");
            webRequest.Method = "POST";
            webRequest.ContentType = "application/x-www-form-urlencoded";
            webRequest.Referer = "https://vk.com";
            webRequest.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/64.0.3282.140 Safari/537.36 Edge/17.17134;";
            webRequest.Accept = "text/html, application/xhtml+xml, application/xml; q=0.9, */*; q=0.8";
           // webRequest.Accept = "image/gif, image/x-xbitmap, image/jpeg,image/pjpeg, application/x-shockwave-flash,application/vnd.ms-excel,application/vnd.ms-powerpoint,application/msword";
            webRequest.Headers.Add("Accept-Language", "en-US");
           // webRequest.KeepAlive = false;


            NameValueCollection nameValueCollection = HttpUtility.ParseQueryString(String.Empty);

            nameValueCollection.Add("act","login");
            nameValueCollection.Add("ip_h", _ip_h);
            nameValueCollection.Add("lg_h", _lg_h);
            nameValueCollection.Add("role", "al_frame");
            nameValueCollection.Add("email", _login);
            nameValueCollection.Add("pass", _pass);
            nameValueCollection.Add("expire","");
            nameValueCollection.Add("captcha_sid","");
            nameValueCollection.Add("captcha_key","");
            nameValueCollection.Add("_origin", "https://vk.com");
            nameValueCollection.Add("q", "1");

            byte[] byteArray = new UTF8Encoding().GetBytes(nameValueCollection.ToString());
            webRequest.ContentLength = byteArray.Length;
            Stream dataStream = webRequest.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();
            WebResponse response = webRequest.GetResponse();

            dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();

            reader.Close();
            dataStream.Close();
            response.Close();

            Console.WriteLine(responseFromServer);

            Console.ReadKey();
        }
    }
}
