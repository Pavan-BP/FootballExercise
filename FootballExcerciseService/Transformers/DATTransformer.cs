using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using FootballExcerciseService.Models;
using FootballExerciseUtilities;

namespace FootballExcerciseService.Transformers
{
    public class DATTransformer : BaseTransformer
    {
        public override List<EnglishPremierLeagueTeam> Transform(StreamReader fileStream)
        {
            string line;
            var lineIndex = 1;
            var englishPremierLeagueTeams = new List<EnglishPremierLeagueTeam>();
            StringBuilder formattedLine;

            while ((line = fileStream.ReadLine()) != null)
            {
                formattedLine = new StringBuilder();
                if (lineIndex == HEADER_LINE_INDEX || lineIndex == SEPARATOR_LINE_INDEX)
                {
                    lineIndex++;
                    continue;
                }
                if (string.IsNullOrWhiteSpace(line))
                    continue;
                line = line.Replace(" ", ",");
                var columns = line.Split(separator);
                
                foreach (var column in columns)
                {
                    if (!string.IsNullOrWhiteSpace(column))
                    {
                        formattedLine.Append(column);
                        formattedLine.Append(separator);
                    }
                }
                columns = formattedLine.ToString().Split(separator);
                var firstColumn = columns[0].Split('.');
                var team = new EnglishPremierLeagueTeam
                {
                    Rank = firstColumn[0].ToNumber(),
                    Name = columns[1].Trim(),
                    MatchesPlayed = columns[2].ToNumber(),
                    MatchesWon = columns[3].ToNumber(),
                    MatchesLost = columns[4].ToNumber(),
                    MatchesDrawn = columns[5].ToNumber(),
                    GoalsFor = columns[6].ToNumber(),
                    GoalsAgainst = columns[8].ToNumber(),
                    Points = columns[9].ToNumber(),
                };
                englishPremierLeagueTeams.Add(team);
                lineIndex++;
            }
            return englishPremierLeagueTeams;
        }
    }
}