using System;
using System.Collections.Generic;
using System.Text;

namespace GameChangeScraper.Helpers
{
    public static class GCLinks
    {
        public static string BASE_URL { get { return "https://gc.com"; } }
        public static string BASE_LOGIN { get { return BASE_URL + "/login"; } }
        public static string BASE_LOGIN_POST { get { return BASE_URL + "/do-login"; } }
    }
}
