using System.Collections.Generic;
using System.IO;
using FootballExcerciseService.Models;

namespace FootballExcerciseService.Transformers
{
    public abstract class BaseTransformer: ITransformer
    {
        protected const int HEADER_LINE_INDEX = 1;
        protected const int SEPARATOR_LINE_INDEX = 19;
        //protected char separator = '\t';
        protected char separator = ',';

        public virtual List<EnglishPremierLeagueTeam> Transform(StreamReader fileStream)
        {
            return new List<EnglishPremierLeagueTeam>();
        }
    }
}
    