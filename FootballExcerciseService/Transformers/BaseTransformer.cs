using System;
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
        protected const int FILE_ROW_COUNT = 22;
        protected const int FILE_COLUMN_COUNT = 9;
        protected const char RANK_NAME_DELIMITER = '.';
        protected const string SEPARATOR = "-";
        protected const char DELIMITER = ',';

        protected virtual void EmptyFileValidation(StreamReader fileStream)
        {
            if (fileStream.Peek() <= 0)
                throw new EmptyFileUploadException();
        }

        protected virtual void FileHeaderValidation(string headerLine)
        {
            var headerColumns = headerLine.Split(DELIMITER);
            if(headerColumns == null || headerColumns.Length != FILE_COLUMN_COUNT)
                throw new InvalidFileFormatException();
        }

        protected virtual void FileSeparatorValidation(string separatorLine)
        {
            if(string.IsNullOrWhiteSpace(separatorLine) || !separatorLine.StartsWith(SEPARATOR))
                throw new InvalidFileFormatException();
        }

        protected virtual void FileRowValidation(int rowCount)
        {
            if(rowCount > FILE_ROW_COUNT)
                throw new InvalidFileFormatException();
        }

        public virtual List<EnglishPremierLeagueTeam> Transform(StreamReader fileStream)
        {
            //literally do nothing;
            return new List<EnglishPremierLeagueTeam>();
        }
    }
}
    