using System;
using System.Collections.Generic;
using System.Text;

namespace GameChangeScraper.Model
{
    public class BoxScore
    {
        public BoxScore()
        {
            this.BoxLines = new List<BoxLine>();
        }
        public string HomeTeam { get; set; }
        public string AwayTeam { get; set; }

        public List<BoxLine> BoxLines { get; set; }

    }

    public class BoxLine
    {
        public string Name { get; set; }
        public int AB { get; set; }
        public int R { get; set; }
        public int H { get; set; }
        public int RBI { get; set; }
        public int BB { get; set; }
        public int SO { get; set; }
    }
}
