using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FootballExcerciseService.Models;
using FootballExerciseUtilities;

namespace FootballExcerciseService.Transformers
{
    public class CSVTransformer : BaseTransformer
    {
        //private new char separator = ',';

        public override List<EnglishPremierLeagueTeam> Transform(StreamReader fileStream)
        {
            string line;
            var lineIndex = 1;
            var englishPremierLeagueTeams = new List<EnglishPremierLeagueTeam>();
            while ((line = fileStream.ReadLine()) != null)
            {
                if (lineIndex == HEADER_LINE_INDEX || lineIndex == SEPARATOR_LINE_INDEX)
                {
                    lineIndex++;
                    continue;
                }
                var columns = line.Split(separator);
                var firstColumn = columns[0].Split('.');
                var englishPremierLeagueTeam = new EnglishPremierLeagueTeam
                {
                    Rank = firstColumn[0].ToNumber(),
                    Name = firstColumn[1].Trim(),
                    MatchesPlayed = columns[1].ToNumber(),
                    MatchesWon = columns[2].ToNumber(),
                    MatchesLost = columns[3].ToNumber(),
                    MatchesDrawn = columns[4].ToNumber(),
                    GoalsFor = columns[5].ToNumber(),
                    GoalsAgainst = columns[7].ToNumber(),
                    Points = columns[8].ToNumber(),
                };
                englishPremierLeagueTeams.Add(englishPremierLeagueTeam);
                lineIndex++;
            }
            return englishPremierLeagueTeams;
        }
    }
}