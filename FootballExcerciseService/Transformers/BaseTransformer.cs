using System.Collections.Generic;
using System.IO;
using FootballExcerciseService.Models;
using FootballExerciseUtilities.Exceptions;

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

        protected virtual void CheckFileSize(StreamReader fileStream)
        {
            if (fileStream.Peek() <= 0)
                throw new EmptyFileUploadException();
        }

        public static BaseTransformer GetTransformer(FileExtensionType fileExtensionType)
        {
            switch (fileExtensionType)
            {
                case FileExtensionType.DAT:
                    return new DATTransformer();

                case FileExtensionType.CSV:
                    return new CSVTransformer();

                default:
                    throw new FileTypeNotSupportedException();
            }
        }
    }
}
    