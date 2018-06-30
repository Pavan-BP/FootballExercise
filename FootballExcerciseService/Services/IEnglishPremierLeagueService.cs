using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FootballExcerciseService.Transformers;
using FootballExcerciseService.Models;

namespace FootballExcerciseService.Services
{
    public interface IEnglishPremierLeagueService
    {
        BaseTransformer GetTransformer(FileExtensionType fileExtension);

        EnglishPremierLeagueTeam GetTeamWithLeastGoalDifference(List<EnglishPremierLeagueTeam> englishPremierLeagueTeams);
    }
}
