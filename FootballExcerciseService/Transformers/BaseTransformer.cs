using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
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
        protected string[] expectedColumnHeaders = new string[] { "Team", "P", "W", "L", "D", "F", "-", "A", "Pts" };

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
            ColumnSequenceValidation(headerColumns);
        }

        protected virtual void ColumnSequenceValidation(string[] headerColumns)
        {
            var errorList = new StringBuilder();
            int i = 0;
            for(i=0; i< FILE_COLUMN_COUNT; i++)
            {
                if(headerColumns[i].ToUpperInvariant() != expectedColumnHeaders[i].ToUpperInvariant())
                {
                    errorList.Append(headerColumns[i]);
                    errorList.Append(", ");
                }
            }
            if (!string.IsNullOrWhiteSpace(errorList.ToString()))
            {
                errorList.ToString().TrimStart(',', ' ');
                errorList.ToString().TrimEnd(',',' ');
                throw new InvalidFileFormatException("The columns " + errorList.ToString() + " are not in agreed correct sequence.");
            }
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
    