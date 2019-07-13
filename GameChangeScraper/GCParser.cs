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
            htmlDocument.LoadHtml(html);

            var awayTeam = htmlDocument.DocumentNode.Descendants("h5").ElementAt(0).InnerText;
            var homeTeam = htmlDocument.DocumentNode.Descendants("h5").ElementAt(1).InnerText;

            var bs = new BoxScore();

            bs.AwayTeam = awayTeam.Substring(0, awayTeam.Length - 10);
            bs.HomeTeam = homeTeam.Substring(0, homeTeam.Length - 10);

            var awayHittingTable = htmlDocument.DocumentNode.Descendants("table").ElementAt(0);
            var homeHittingTable = htmlDocument.DocumentNode.SelectNodes("//table").ElementAt(1);
            var awayPitchingTable = htmlDocument.DocumentNode.SelectNodes("//table").ElementAt(2);
            var homePitchingTable = htmlDocument.DocumentNode.SelectNodes("//table").ElementAt(3);

            try
            {
                var playerRows = awayHittingTable.Descendants("tr")
                        .Where(node => node.GetAttributeValue("class", "")
                        .Contains("playerRow"))
                        .ToList();
                foreach (var row in playerRows)
                {
                    var tds = row.Descendants("td");
                    bs.AwayHittingBoxLines.Add(new HittingBoxLine
                    {
                        Name = tds.ElementAt(0).InnerText.Trim(),
                        AB = Int32.Parse(tds.ElementAt(1).InnerText.Trim()),
                        R = Int32.Parse(tds.ElementAt(2).InnerText.Trim()),
                        H = Int32.Parse(tds.ElementAt(3).InnerText.Trim()),
                        RBI = Int32.Parse(tds.ElementAt(4).InnerText.Trim()),
                        BB = Int32.Parse(tds.ElementAt(5).InnerText.Trim()),
                        SO = Int32.Parse(tds.ElementAt(6).InnerText.Trim()),
                    });
                }

                playerRows = homeHittingTable.Descendants("tr")
                    .Where(node => node.GetAttributeValue("class", "")
                    .Contains("playerRow"))
                    .ToList();
                foreach (var row in playerRows)
                {
                    var tds = row.Descendants("td");
                    bs.HomeHittingBoxLines.Add(new HittingBoxLine
                    {
                        Name = tds.ElementAt(0).InnerText.Trim(),
                        AB = Int32.Parse(tds.ElementAt(1).InnerText.Trim()),
                        R = Int32.Parse(tds.ElementAt(2).InnerText.Trim()),
                        H = Int32.Parse(tds.ElementAt(3).InnerText.Trim()),
                        RBI = Int32.Parse(tds.ElementAt(4).InnerText.Trim()),
                        BB = Int32.Parse(tds.ElementAt(5).InnerText.Trim()),
                        SO = Int32.Parse(tds.ElementAt(6).InnerText.Trim()),
                    });
                }

                playerRows = awayPitchingTable.Descendants("tr")
                    .Where(node => node.GetAttributeValue("class", "")
                    .Contains("playerRow"))
                    .ToList();
                foreach (var row in playerRows)
                {
                    var tds = row.Descendants("td");
                    bs.AwayPitchingBoxLines.Add(new PitchingBoxLine
                    {
                        Name = tds.ElementAt(0).InnerText.Trim(),
                        IP = float.Parse(tds.ElementAt(1).InnerText.Trim()),
                        NumberPitches = int.Parse(tds.ElementAt(2).InnerText.Trim()),
                        StikePercent = float.Parse(tds.ElementAt(3).InnerText.Trim()),
                        H = Int32.Parse(tds.ElementAt(4).InnerText.Trim()),
                        R = Int32.Parse(tds.ElementAt(5).InnerText.Trim()),
                        ER = Int32.Parse(tds.ElementAt(6).InnerText.Trim()),
                        SO = Int32.Parse(tds.ElementAt(7).InnerText.Trim()),
                        BB = Int32.Parse(tds.ElementAt(8).InnerText.Trim()),
                        HR = Int32.Parse(tds.ElementAt(9).InnerText.Trim()),
                    });
                }

                playerRows = homePitchingTable.Descendants("tr")
                    .Where(node => node.GetAttributeValue("class", "")
                    .Contains("playerRow"))
                    .ToList();
                foreach (var row in playerRows)
                {
                    var tds = row.Descendants("td");
                    bs.HomePitchingBoxLines.Add(new PitchingBoxLine
                    {
                        Name = tds.ElementAt(0).InnerText.Trim(),
                        IP = float.Parse(tds.ElementAt(1).InnerText.Trim()),
                        NumberPitches = int.Parse(tds.ElementAt(2).InnerText.Trim()),
                        StikePercent = float.Parse(tds.ElementAt(3).InnerText.Trim()),
                        H = Int32.Parse(tds.ElementAt(4).InnerText.Trim()),
                        R = Int32.Parse(tds.ElementAt(5).InnerText.Trim()),
                        ER = Int32.Parse(tds.ElementAt(6).InnerText.Trim()),
                        SO = Int32.Parse(tds.ElementAt(7).InnerText.Trim()),
                        BB = Int32.Parse(tds.ElementAt(8).InnerText.Trim()),
                        HR = Int32.Parse(tds.ElementAt(9).InnerText.Trim()),
                    });
                }
            }
            catch (Exception)
            {
                //crap excecption catching
                Console.WriteLine("No Game Data!");
            }

            return bs;
        }
    }
}
