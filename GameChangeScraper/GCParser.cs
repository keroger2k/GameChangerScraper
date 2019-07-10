using GameChangeScraper.Model;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

namespace GameChangeScraper
{
    public class GCParser
    {
        public List<Game> ParseScheduleHTML(string html)
        {
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);
            var games = htmlDocument.DocumentNode
                .SelectSingleNode("//table[contains(@class, 'gcTable')]")
                .SelectNodes("//tr[contains(@class, 'game')]");
            var gameList = new List<Game>();
            string scorePattern = @"(?<result>\w+), (?<awayScore>\d+)-(?<homeScore>\d+)";
            string oppPattern = @"(?<gameStatus>vs|@)\s+(?<opponent>.+)";
            Regex scoreRegex = new Regex(scorePattern, RegexOptions.IgnoreCase);
            Regex oppRegex = new Regex(oppPattern, RegexOptions.IgnoreCase);

            foreach (var game in games)
            {
                var g = new Game();
                var list = game.Descendants("a");

                Match m = scoreRegex.Match(list.Last().InnerText);
                Match m1 = oppRegex.Match(list.First().InnerText);

                g.Opponent = m1.Groups["opponent"].Value;
                g.GameStatus = m1.Groups["gameStatus"].Value;
                g.Result = m.Groups["result"].Value;
                g.AwayScore = m.Groups["awayScore"].Value;
                g.HomeScore = m.Groups["homeScore"].Value;


                var score = list.Last();
                g.GameLink = score.Attributes["href"].Value.ToString();
                gameList.Add(g);
            }
            return gameList;
        }

        public BoxScore ParseGameHTML(Game gameObj, string html)
        {
            var htmlDocument = new HtmlDocument();
            var h1 = new HtmlDocument();
            var h2 = new HtmlDocument();
            htmlDocument.LoadHtml(html);

            var awayTeam = htmlDocument.DocumentNode.SelectNodes("//h5").ElementAt(0).InnerText;
            var homeTeam = htmlDocument.DocumentNode.SelectNodes("//h5").ElementAt(1).InnerText;

            var bs = new BoxScore();

            bs.AwayTeam = awayTeam.Substring(0, awayTeam.Length - 10);
            bs.HomeTeam = homeTeam.Substring(0, homeTeam.Length - 10);

            var awayHittingTable = htmlDocument.DocumentNode.SelectNodes("//table").ElementAt(0).CreateNavigator();
            var homeHittingTable = htmlDocument.DocumentNode.SelectNodes("//table").ElementAt(1);
            var awayPitchingTable = htmlDocument.DocumentNode.SelectNodes("//table").ElementAt(2);
            var homePitchingTable = htmlDocument.DocumentNode.SelectNodes("//table").ElementAt(3);

            foreach(var i in awayHittingTable.Select("//tr[contains(@class, 'playerRow')]").Current.Select("//td"))
            {
                
                Console.WriteLine(i.ToString());
            }


            //var battingHTML = stats.First();
            //var pitchingHTML = stats.Last();

            return bs;
        }
    }
}
