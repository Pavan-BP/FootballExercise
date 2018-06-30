using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FootballExcerciseService.Transformers;
using FootballExcerciseService.Models;
using System.IO;

namespace FootballExcerciseService.Services
{
    public class EnglishPremierLeagueService : IEnglishPremierLeagueService
    {
        private ITransformer _transformer;
        public EnglishPremierLeagueService(ITransformer transformer)
        {
            _transformer = transformer;
        }

        public EnglishPremierLeagueTeam GetTeamWithLeastGoalDifference(StreamReader fileStream, FileExtensionType fileExtension)
        {
            var englishPremierLeagueTeams = _transformer.Transform(fileStream);
            return GetTeamWithLeastGoalDifference(englishPremierLeagueTeams);
        }


        private EnglishPremierLeagueTeam GetTeamWithLeastGoalDifference(List<EnglishPremierLeagueTeam> englishPremierLeagueTeams)
        {
            if(englishPremierLeagueTeams!=null && englishPremierLeagueTeams.Any())
            {
                return englishPremierLeagueTeams.OrderBy(x => x.GoalDifference).Take(1).FirstOrDefault();
            }
            return null;
        }
    }
}
