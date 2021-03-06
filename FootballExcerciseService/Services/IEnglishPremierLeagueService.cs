﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FootballExcerciseService.Transformers;
using FootballExcerciseService.Models;
using System.IO;

namespace FootballExcerciseService.Services
{
    public interface IEnglishPremierLeagueService
    {
        List<EnglishPremierLeagueTeam> GetTeamsWithLeastGoalDifference(StreamReader fileStream, FileExtensionType fileExtensionType);

    }
}
