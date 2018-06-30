using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FootballExcerciseService.Models;

namespace FootballExcerciseService.Transformers
{
    public interface ITransformer
    {
        List<EnglishPremierLeagueTeam> Transform(StreamReader fileStream);
    }
}
