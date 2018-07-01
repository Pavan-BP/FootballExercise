using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FootballExcerciseService.Models;

namespace FootballExerciseService.Tests.ObjectMother
{
    public static class EnglishPremierLeagueObjectMother
    {

        public static EnglishPremierLeagueTeam GetEnglishPremierLeagueTeam(string name, int rank, int matchesPlayed, int matchesWon, int matchesLost, int matchesDrawn, int goalsFor, int goalsAgainst, int points)
        {
            return new EnglishPremierLeagueTeam
            {
                GoalsAgainst = goalsAgainst,
                GoalsFor = goalsFor,
                MatchesDrawn = matchesDrawn,
                MatchesLost = matchesLost,
                MatchesPlayed = matchesPlayed,
                MatchesWon = matchesWon,
                Name = name,
                Points = points,
                Rank = rank
            };
        }

        public static List<EnglishPremierLeagueTeam> GetEnglishPremierLeagueTeams()
        {
            return GetEnglishPremierLeagueTeams(
                GetEnglishPremierLeagueTeam("Manchester United", 1, 38, 30, 0, 8, 100, 20, 98),
                GetEnglishPremierLeagueTeam("Arsenal", 2, 38, 28, 2, 8, 90, 30, 92),
                GetEnglishPremierLeagueTeam("Liverpool", 3, 38, 29, 6, 3, 80, 30, 90),
                GetEnglishPremierLeagueTeam("Manchester City", 4, 38, 25, 4, 9, 70, 40, 84),
                GetEnglishPremierLeagueTeam("Chelsea", 5, 38, 24, 5, 9, 60, 50, 81),
                GetEnglishPremierLeagueTeam("Tottenham HotSpurs", 6, 38, 23, 6, 9, 65, 45, 78)
            );
        }

        public static List<EnglishPremierLeagueTeam> GetEnglishPremierLeagueTeamsWithSameGoalDifference()
        {
            return GetEnglishPremierLeagueTeams(
                GetEnglishPremierLeagueTeam("Manchester United", 1, 38, 30, 0, 8, 65, 35, 98),
                GetEnglishPremierLeagueTeam("Arsenal", 2, 38, 28, 2, 8, 60, 30, 92),
                GetEnglishPremierLeagueTeam("Liverpool", 3, 38, 29, 6, 3, 55, 25, 90),
                GetEnglishPremierLeagueTeam("Manchester City", 4, 38, 25, 4, 9, 50, 20, 84),
                GetEnglishPremierLeagueTeam("Chelsea", 5, 38, 24, 5, 9, 45, 15, 15),
                GetEnglishPremierLeagueTeam("Tottenham HotSpurs", 6, 38, 23, 6, 9, 40, 10, 78)
            );
        }

        public static List<EnglishPremierLeagueTeam> GetEnglishPremierLeagueTeams(params EnglishPremierLeagueTeam[] englishPremierLeagueTeams)
        {
            var _englishPremierLeagueTeams = new List<EnglishPremierLeagueTeam>();
            foreach (var englishPremierLeagueTeam in englishPremierLeagueTeams)
            {
                _englishPremierLeagueTeams.Add(englishPremierLeagueTeam);
            }
            return _englishPremierLeagueTeams;
        }
    }
}
