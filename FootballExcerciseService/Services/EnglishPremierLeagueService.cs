using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FootballExcerciseService.Transformers;
using FootballExcerciseService.Models;
using FootballExerciseUtilities.Exceptions;

namespace FootballExcerciseService.Services
{
    public class EnglishPremierLeagueService : IEnglishPremierLeagueService
    {
        public BaseTransformer GetTransformer(FileExtensionType fileExtension)
        {
            switch (fileExtension)
            {
                case FileExtensionType.DAT:
                    return new DATTransformer();

                case FileExtensionType.CSV:
                    return new CSVTransformer();

                default:
                    throw new FileTypeNotSupportedException("This file extension import is not supported!");
            }
        }

        public EnglishPremierLeagueTeam GetTeamWithLeastGoalDifference(List<EnglishPremierLeagueTeam> englishPremierLeagueTeams)
        {
            if(englishPremierLeagueTeams!=null && englishPremierLeagueTeams.Any())
            {
                return englishPremierLeagueTeams.OrderBy(x => x.GoalDifference).Take(1).FirstOrDefault();
            }
            return null;
        }
    }
}
