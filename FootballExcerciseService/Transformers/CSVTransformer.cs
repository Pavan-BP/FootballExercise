using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using FootballExcerciseService.Models;
using FootballExerciseUtilities;
using FootballExerciseUtilities.Exceptions;

namespace FootballExcerciseService.Transformers
{
    public class CSVTransformer : BaseTransformer
    {

        public override List<EnglishPremierLeagueTeam> Transform(StreamReader fileStream)
        {
            string line;
            var lineIndex = 1;
            var englishPremierLeagueTeams = new List<EnglishPremierLeagueTeam>();

            EmptyFileValidation(fileStream);
            
            while ((line = fileStream.ReadLine()) != null )
            {
                if (string.IsNullOrWhiteSpace(line))
                    throw new InvalidFileFormatException();

                var columns = line.Split(DELIMITER);

                if (lineIndex == HEADER_LINE_INDEX)
                {
                    FileHeaderValidation(columns);
                    lineIndex++;
                    continue;
                }

                //ignore the row if it has seperator
                if (Regex.IsMatch(columns[0].Trim(), SEPERATOR_REGEX))
                {
                    lineIndex++;
                    continue;
                }
                    

                ColumnCountValidation(columns);

                var firstColumn = columns[0].Split(RANK_NAME_DELIMITER);
                var englishPremierLeagueTeam = new EnglishPremierLeagueTeam
                {
                    Rank = firstColumn[0].ToNumber("Team", lineIndex),
                    Name = firstColumn[1].Trim(),
                    MatchesPlayed = columns[1].ToNumber("P", lineIndex),
                    MatchesWon = columns[2].ToNumber("W", lineIndex),
                    MatchesLost = columns[3].ToNumber("L", lineIndex),
                    MatchesDrawn = columns[4].ToNumber("D", lineIndex),
                    GoalsFor = columns[5].ToNumber("F", lineIndex),
                    GoalsAgainst = columns[7].ToNumber("A", lineIndex),
                    Points = columns[8].ToNumber("Pts", lineIndex),
                };
                englishPremierLeagueTeams.Add(englishPremierLeagueTeam);
                lineIndex++;
            }
            return englishPremierLeagueTeams;
        }
    }
}