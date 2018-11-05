using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace VkLogin5
{
    internal class Code
    {
        internal static string GetCode()
        {
            string urlAddress = "https://vk.com/login";
            string data = "";
            HttpWebRequest _request = (HttpWebRequest)WebRequest.Create(urlAddress);
            HttpWebResponse _response = (HttpWebResponse)_request.GetResponse();
            if (_response.StatusCode == HttpStatusCode.OK)
            {
                Stream receiveStream = _response.GetResponseStream();
                StreamReader readStream = null;
                if (_response.CharacterSet == null)
                {
                    readStream = new StreamReader(receiveStream);
                }
                else
                {
                    readStream = new StreamReader(receiveStream, Encoding.GetEncoding(_response.CharacterSet));
                }
                data = readStream.ReadToEnd();
                _response.Close();
                readStream.Close();
            }
            return data;
        }

        //public static string TakeValues()
        //{
        //    Regex re = new Regex(@"(<input type="hidden" name="ip_h" value=>)");
        //    MatchCollection _matchCollection = re.Matches()
        //}

    }

}
