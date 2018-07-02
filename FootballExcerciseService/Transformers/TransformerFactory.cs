using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FootballExcerciseService.Models;
using FootballExerciseUtilities.Exceptions;

namespace FootballExcerciseService.Transformers
{
    public class TransformerFactory : ITransformerFactory
    {
        public ITransformer FetchTransformer(FileExtensionType fileExtensionType)
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
