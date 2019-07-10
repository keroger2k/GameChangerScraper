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

            var g = gameList.ElementAt(1);
            var boxScore = parser.ParseGameHTML(g, gc.GameStats(g.GameLink));
        }
    }   
}
