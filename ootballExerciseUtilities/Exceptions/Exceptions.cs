using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballExerciseUtilities.Exceptions
{
    public class FootballExerciseException: Exception
    {
        public FootballExerciseException()
        {

        }

        public FootballExerciseException(string message): base(message)
        {

        }
    }

    public class FileTypeNotSupportedException: FootballExerciseException
    {
        public FileTypeNotSupportedException()
        {

        }

        public FileTypeNotSupportedException(string message) : base(message)
        {

        }

    }

    public class ImportFileInUseException : FootballExerciseException
    {
        public ImportFileInUseException()
        {

        }

        public ImportFileInUseException(string message) : base(message)
        {

        }

    }
}
