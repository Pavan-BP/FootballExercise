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
        protected const int FILE_ROW_COUNT = 22;
        protected const int FILE_COLUMN_COUNT = 9;
        protected const char RANK_NAME_DELIMITER = '.';
        protected const string SEPERATOR_REGEX = ".*-";
        protected const string SEPARATOR = "-";
        protected const char DELIMITER = ',';
        protected string[] expectedColumnHeaders = new string[] { "Team", "P", "W", "L", "D", "F", "-", "A", "Pts" };
        

        protected virtual void EmptyFileValidation(StreamReader fileStream)
        {
            if (fileStream.Peek() <= 0)
                throw new EmptyFileUploadException();
        }

        protected virtual void FileHeaderValidation(string[] headerColumns)
        {
            ColumnCountValidation(headerColumns);
            HeaderColumnSequenceValidation(headerColumns);
        }

        protected virtual void ColumnCountValidation(string[] columns, int columnCount = FILE_COLUMN_COUNT)
        {
            if (columns != null)
            {
                if (columns.Length > columnCount)
                    throw new InvalidFileFormatException("File has more columns than the standarad format. Cannot process the file.");
                if (columns.Length < columnCount)
                    throw new InvalidFileFormatException("File has lesser columns than the standarad format. Cannot process the file.");
            }
        }

        protected virtual void HeaderColumnSequenceValidation(string[] headerColumns)
        {
            if (headerColumns == null)
                throw new InvalidFileFormatException("File has an empty row. Cannot process the file.");
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

        public virtual List<EnglishPremierLeagueTeam> Transform(StreamReader fileStream)
        {
            //literally do nothing;
            return new List<EnglishPremierLeagueTeam>();
        }
    }
}
    