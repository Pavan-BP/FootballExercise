using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FootballExcerciseService.Transformers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FootballExcerciseService.Models;
using FootballExerciseUtilities.Exceptions;

namespace FootballExerciseService.tests.TransformerTests
{
    
    [TestClass]
    public class BaseTransformerTest
    {
        private FileExtensionType _fileExtensionType;

        [TestMethod]
        public void BaseTransformer_GetTransformer_Returns_CSVTransformer_Instance_On_CSV_File_Upload()
        {
            //Arrange
            _fileExtensionType = FileExtensionType.CSV;

            //Act
            var result = BaseTransformer.GetTransformer(_fileExtensionType);

            //Assert
            Assert.IsInstanceOfType(result, typeof(CSVTransformer));

        }

        [TestMethod]
        public void BaseTransformer_GetTransformer_Returns_DATTransformer_Instance_On_DAT_File_Upload()
        {
            //Arrange
            _fileExtensionType = FileExtensionType.DAT;

            //Act
            var result = BaseTransformer.GetTransformer(_fileExtensionType);

            //Assert
            Assert.IsInstanceOfType(result, typeof(DATTransformer));

        }

        [TestMethod]
        public void BaseTransformer_GetTransformer_Throws_FileTypeNotSupportedException_On_File_Upload_With_OTHER_Extension()
        {
            //Arrange
            _fileExtensionType = FileExtensionType.OTHER;

            //Act
            try
            {
                BaseTransformer.GetTransformer(_fileExtensionType);
            }
            catch (Exception ex)
            {
                //Assert
                Assert.IsInstanceOfType(ex, typeof(FileTypeNotSupportedException));
            }
        }
    }
}
