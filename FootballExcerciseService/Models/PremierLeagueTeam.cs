using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballExcerciseService.Models
{
    public class EnglishPremierLeagueTeam
    {
        public string Name { get; set; }
        public int Rank { get; set; }
        public int MatchesPlayed { get; set; }
        public int MatchesWon { get; set; }
        public int MatchesLost { get; set; }
        public int MatchesDrawn { get; set; }
        public int GoalsFor { get; set; }
        public int GoalsAgainst { get; set; }
        public int Points { get; set; }
        public int GoalDifference   
        {
            get
            {
                return GoalsFor - GoalsAgainst;
            }
        }
    }
}
