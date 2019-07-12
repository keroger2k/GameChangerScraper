using System;
using System.Collections.Generic;
using System.Text;

namespace GameChangeScraper.Model
{
    public class BoxScore
    {
        public BoxScore()
        {
            this.AwayHittingBoxLines = new List<HittingBoxLine>();
            this.AwayPitchingBoxLines = new List<PitchingBoxLine>();
            this.HomePitchingBoxLines = new List<PitchingBoxLine>();
            this.HomeHittingBoxLines = new List<HittingBoxLine>();
        }
        public string HomeTeam { get; set; }
        public string AwayTeam { get; set; }

        public List<HittingBoxLine> AwayHittingBoxLines { get; set; }
        public List<HittingBoxLine> HomeHittingBoxLines { get; set; }
        public List<PitchingBoxLine> AwayPitchingBoxLines { get; set; }
        public List<PitchingBoxLine> HomePitchingBoxLines { get; set; }

    }



    public class HittingBoxLine
    {
        public string Name { get; set; }
        public int AB { get; set; }
        public int R { get; set; }
        public int H { get; set; }
        public int RBI { get; set; }
        public int BB { get; set; }
        public int SO { get; set; }
    }

    public class PitchingBoxLine
    {
        public string Name { get; set; }
        public float IP { get; set; }
        public int NumberPitches { get; set; }
        public float StikePercent { get; set; }
        public int H { get; set; }
        public int R { get; set; }
        public int ER { get; set; }
        public int SO { get; set; }
        public int BB { get; set; }
        public int HR { get; set; }
    }
}
