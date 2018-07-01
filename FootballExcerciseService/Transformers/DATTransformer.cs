using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using FootballExcerciseService.Models;
using FootballExerciseUtilities;
using FootballExerciseUtilities.Exceptions;

namespace FootballExcerciseService.Transformers
{
    public class DATTransformer : BaseTransformer
    {
        public override List<EnglishPremierLeagueTeam> Transform(StreamReader fileStream)
        {
            string line;
            var lineIndex = 1;
            var englishPremierLeagueTeams = new List<EnglishPremierLeagueTeam>();

            EmptyFileValidation(fileStream);

            while ((line = fileStream.ReadLine()) != null)
            {
                if (string.IsNullOrWhiteSpace(line))
                    throw new InvalidFileFormatException();

                if (lineIndex == HEADER_LINE_INDEX)
                {
                    FileHeaderValidation(line);
                    lineIndex++;
                    continue;
                }
                if (lineIndex == SEPARATOR_LINE_INDEX)
                {
                    FileSeparatorValidation(line.Trim());
                    lineIndex++;
                    continue;
                }

                string[] columns = FormatLineToCSVSeperated(ref line);
                var firstColumn = columns[0].Split(RANK_NAME_DELIMITER);

                TeamColumnValidation(firstColumn);

                var englishPremierLeagueTeam = new EnglishPremierLeagueTeam
                {
                    Rank = firstColumn[0].ToNumber("Team", lineIndex),
                    Name = columns[1].Trim(),
                    MatchesPlayed = columns[2].ToNumber("P", lineIndex),
                    MatchesWon = columns[3].ToNumber("W", lineIndex),
                    MatchesLost = columns[4].ToNumber("L", lineIndex),
                    MatchesDrawn = columns[5].ToNumber("D", lineIndex),
                    GoalsFor = columns[6].ToNumber("F", lineIndex),
                    GoalsAgainst = columns[8].ToNumber("A", lineIndex),
                    Points = columns[9].ToNumber("Pts", lineIndex),
                };
                englishPremierLeagueTeams.Add(englishPremierLeagueTeam);
                lineIndex++;
            }
            return englishPremierLeagueTeams;
        }

        protected override void FileHeaderValidation(string line)
        {
            string[] headerColumns = FormatLineToCSVSeperated(ref line);
            if (headerColumns == null || headerColumns.Length != FILE_COLUMN_COUNT+1)
                throw new InvalidFileFormatException();
        }

        private string[] FormatLineToCSVSeperated(ref string line)
        {
            StringBuilder formattedLine = new StringBuilder();
            line = line.Trim();
            line = line.Replace(' ', DELIMITER);
            var columns = line.Split(DELIMITER);

            foreach (var column in columns)
            {
                if (!string.IsNullOrWhiteSpace(column))
                {
                    formattedLine.Append(column);
                    formattedLine.Append(DELIMITER);
                }
            }
            columns = formattedLine.ToString().Split(DELIMITER);
            return columns;
        }

        private void TeamColumnValidation(string[] firstColumn)
        {
            if (!string.IsNullOrWhiteSpace(firstColumn[1]))
                throw new InvalidFileFormatException("The file format is invalid. Please provide a space between team name and team rank after the .(dot).");
        }
        
    }
}