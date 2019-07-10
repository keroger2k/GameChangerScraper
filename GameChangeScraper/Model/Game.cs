using System;
using System.Collections.Generic;
using System.Text;

namespace GameChangeScraper.Model
{
    public class Game
    {
        public string Opponent { get; set; }
        public string Result { get; set; }
        public string HomeScore { get; set; }
        public string AwayScore { get; set; }
        public string GameLink { get; set; }
        public string GameStatus { get; set; }

        public string Location
        {
            get
            {
                return GameStatus == "vs" ? "Home" : "Away";
            }
        }

    }
}
