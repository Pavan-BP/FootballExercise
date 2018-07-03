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
        private ITransformerFactory _transformerfactory;

        public EnglishPremierLeagueService(ITransformerFactory transformerfactory)
        {
            _transformerfactory = transformerfactory;
        }

        public List<EnglishPremierLeagueTeam> GetTeamsWithLeastGoalDifference(StreamReader fileStream, FileExtensionType fileExtensionType)
        {
            try
            {
                _transformer = _transformerfactory.FetchTransformer(fileExtensionType);
                var englishPremierLeagueTeams = _transformer.Transform(fileStream);
                return GetTeamWithLeastGoalDifference(englishPremierLeagueTeams);
            }
            catch (Exception)
            {
                throw;
            }
            
        }


        private List<EnglishPremierLeagueTeam> GetTeamWithLeastGoalDifference(List<EnglishPremierLeagueTeam> englishPremierLeagueTeams)
        {
            if(englishPremierLeagueTeams!=null && englishPremierLeagueTeams.Any())
            {
                var leastGoalDifference = englishPremierLeagueTeams.OrderBy(x => x.GoalDifference).Take(1).FirstOrDefault().GoalDifference;
                return englishPremierLeagueTeams.FindAll(x => x.GoalDifference == leastGoalDifference);
            }
            return null;
        }
    }
}
