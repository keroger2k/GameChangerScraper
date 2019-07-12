using GameChangeScraper.Helpers;
using System;
using System.Linq;

namespace GameChangeScraper
{
    class Program
    {
        

        static void Main(string[] args)
        {
            var parser = new GCParser();
            var gc = new GCSession();
            var gameList = parser.ParseScheduleHTML(
                gc.GameList("/t/spring-2019/pony-express-blue-9u-5c68fdc259f62da9c0000001"));

            foreach(var g in gameList)
            {
                var boxScore = parser.ParseGameHTML(g, gc.GameStats(g.GameLink));

                Console.WriteLine(string.Format("-------------------------------------------------------------------------------"));
                Console.WriteLine(string.Format("Pony  {0}  {1} ",g.Location, g.Opponent));
                Console.WriteLine(string.Format("-------------------------------------------------------------------------------"));
                Console.WriteLine("");
                Console.WriteLine(string.Format("Name\t\tAB\tR\tH\tRBI\tBB\tSO"));
                foreach (var p in boxScore.AwayHittingBoxLines)
                {
                    Console.WriteLine(string.Format("{0}\t\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}", p.Name, p.AB, p.R, p.H, p.RBI, p.BB, p.SO));
                }

            }
        }
    }
}
