using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using FootballExcerciseService.Transformers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FootballExcerciseService.Models;
using Moq;
using FootballExerciseUtilities.Exceptions;

namespace FootballExerciseService.tests.TransformerTests
{
    [TestClass]
    public class DATTransformerTest
    {

        private DATTransformer _target;
        private string currentWorkingDirectoryPath;
        private StreamReader _validDATStreamReader;

        private string _validDATFilePath;
        private string _validDATRandomRowSeparatorFilePath;
        private string _validDATExtraRowsFilePath;
        private string _invalidEmptyDATFilePath;
        private string _invalidDATColumnsInterchangedFilePath;
        private string _invalidDATEmptyRownInBetweenFilePath;
        private string _invalidDATExtraColumnsFilePath;        
        private string _invalidDATWithInCorrectDataFilePath;
        private string _invalidDATLesserColumnsFilePath;        
        private string _invalidDATWithWrongHeaderFilePath;

        [TestInitialize]
        public void Init()
        {
            currentWorkingDirectoryPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;

            _validDATFilePath = currentWorkingDirectoryPath + @"\TestData\CorrectDataAndFormat\football.dat";
            _invalidEmptyDATFilePath = currentWorkingDirectoryPath + @"\TestData\EmptyFiles\EmptyFile.dat";
            _invalidDATColumnsInterchangedFilePath = currentWorkingDirectoryPath + @"\TestData\ColumnsInterchanged\FootballColumnInterchanged.dat";
            _invalidDATEmptyRownInBetweenFilePath = currentWorkingDirectoryPath + @"\TestData\EmptyRows\FootballHavingEmptyRowsInBetween.dat";
            _invalidDATExtraColumnsFilePath = currentWorkingDirectoryPath + @"\TestData\ExtraColumns\FootballHavingExtraColumns.dat";
            _validDATExtraRowsFilePath = currentWorkingDirectoryPath + @"\TestData\ExtraRows\FootballWithExtraRows.dat";
            _invalidDATWithInCorrectDataFilePath = currentWorkingDirectoryPath + @"\TestData\IncorrectData\FootballInCorrectData.dat";
            _invalidDATLesserColumnsFilePath = currentWorkingDirectoryPath + @"\TestData\LesserColumns\FootballWithLesserColumns.dat";
            _validDATRandomRowSeparatorFilePath = currentWorkingDirectoryPath + @"\TestData\RandomRowSeparator\FootballRandomRowSeparator.dat";
            _invalidDATWithWrongHeaderFilePath = currentWorkingDirectoryPath + @"\TestData\WrongColumnHeaders\FootballWrongColumnHeaders.dat";

            _target = new DATTransformer();
        }

        [TestMethod]
        public void DATTransformer_Transform_Method_Returns_List_Of_PremierLeagueTeams_On_Valid_DAT_Import()
        {
            using (_validDATStreamReader = new StreamReader(_validDATFilePath))
            {
                var result = _target.Transform(_validDATStreamReader);

                Assert.IsNotNull(result);
                Assert.IsInstanceOfType(result, typeof(List<EnglishPremierLeagueTeam>));
                Assert.IsTrue((result as List<EnglishPremierLeagueTeam>).Count > 0);
            }

        }

        [TestMethod]
        public void DATTransformer_Transform_Method_Throws_An_Exception_On_Uploading_Empty_DAT_File()
        {
            //Arrange
            using (var emptyFileStream = new StreamReader(_invalidEmptyDATFilePath))
            {
                try
                {
                    //Act
                    _target.Transform(emptyFileStream);
                }
                catch (Exception ex)
                {
                    //Assert
                    Assert.IsInstanceOfType(ex, typeof(EmptyFileUploadException));
                }
            }
        }
        
        [TestMethod]
        public void DATTransformer_Transform_Method_Throws_An_Exception_With_Invalid_Column_Names_On_Uploading_A_DAT_File_With_Headers_In_Wrong_Sequence()
        {
            var expectedExceptionMessage = "The columns -, P, W, L, D, F,  are incorrect or not in correct sequence.";
            try
            {
                using (var inCorrectColumnFileStream = new StreamReader(_invalidDATColumnsInterchangedFilePath))
                {
                    _target.Transform(inCorrectColumnFileStream);
                }
                
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(InvalidFileFormatException));
                Assert.IsTrue(ex.Message.ToUpperInvariant() == expectedExceptionMessage.ToUpperInvariant());
            }
        }

        [TestMethod]
        public void DATTransformer_Transform_Method_Throws_Upload_File_Format_Exception_On_Uploading_A_DAT_File_With_Empty_Rows_InBetween()
        {
            var expectedExceptionMessage = "Cannot process the file as it is not in the agreed format.";
            try
            {
                using (var emptyRowsInBetweenFileStream = new StreamReader(_invalidDATEmptyRownInBetweenFilePath))
                {
                    _target.Transform(emptyRowsInBetweenFileStream);
                }

            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(InvalidFileFormatException));
                Assert.IsTrue(ex.Message.ToUpperInvariant() == expectedExceptionMessage.ToUpperInvariant());
            }
        }

        [TestMethod]
        public void DATTransformer_Transform_Method_Throws_An_Exception_On_Uploading_A_DAT_File_With_Extra_Columns()
        {
            var expectedExceptionMessage = "File has more columns than the standarad format. Cannot process the file.";
            try
            {
                using (var extraColumnsFileStream = new StreamReader(_invalidDATExtraColumnsFilePath))
                {
                    _target.Transform(extraColumnsFileStream);
                }

            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(InvalidFileFormatException));
                Assert.IsTrue(ex.Message.ToUpperInvariant() == expectedExceptionMessage.ToUpperInvariant());
            }
        }

        [TestMethod]
        public void DATTransformer_Transform_Method_Returns_List_Of_PremierLeagueTeams_On_Valid_DAT_Import_With_More_Than_20_Records()
        {
            using (_validDATStreamReader = new StreamReader(_validDATExtraRowsFilePath))
            {
                var result = _target.Transform(_validDATStreamReader);

                Assert.IsNotNull(result);
                Assert.IsInstanceOfType(result, typeof(List<EnglishPremierLeagueTeam>));
                Assert.IsTrue((result as List<EnglishPremierLeagueTeam>).Count > 20);
            }
        }

        [TestMethod]
        public void DATTransformer_Transform_Method_Throws_An_Exception_With_Row_number_On_Uploading_A_DAT_File_With_InCorrect_Data()
        {
            var expectedExceptionMessage = "Invalid value in column: F on Linenumber: 5.";
            try
            {
                using (var inCorrectDataFileStream = new StreamReader(_invalidDATWithInCorrectDataFilePath))
                {
                    _target.Transform(inCorrectDataFileStream);
                }

            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(InvalidFileFormatException));
                Assert.IsTrue(ex.Message.ToUpperInvariant() == expectedExceptionMessage.ToUpperInvariant());
            }
        }

        [TestMethod]
        public void DATTransformer_Transform_Method_Throws_A_File_Format_Exception_On_Uploading_A_DAT_File_With_Lesser_Columns_Than_Standard_Format()
        {
            var expectedExceptionMessage = "File has lesser columns than the standarad format. Cannot process the file.";
            try
            {
                using (var lesserColumnFileStream = new StreamReader(_invalidDATLesserColumnsFilePath))
                {
                    _target.Transform(lesserColumnFileStream);
                }

            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(InvalidFileFormatException));
                Assert.IsTrue(ex.Message.ToUpperInvariant() == expectedExceptionMessage.ToUpperInvariant());
            }
        }

        [TestMethod]
        public void DATTransformer_Transform_Method_Returns_List_Of_PremierLeagueTeams_On_Importing_a_DAT_File_With_Random_Separator()
        {
            using (_validDATStreamReader = new StreamReader(_validDATRandomRowSeparatorFilePath))
            {
                var result = _target.Transform(_validDATStreamReader);

                Assert.IsNotNull(result);
                Assert.IsInstanceOfType(result, typeof(List<EnglishPremierLeagueTeam>));
                Assert.IsTrue((result as List<EnglishPremierLeagueTeam>).Count == 20);
            }
        }

        [TestMethod]
        public void DATTransformer_Transform_Method_Throws_A_File_Format_Exception_On_Uploading_A_DAT_File_With_Header_Values_Than_Standard_Format()
        {
            var expectedExceptionMessage = "The columns Played, Won, Lost, Drawn, For, Against, Points,  are incorrect or not in correct sequence.";
            try
            {
                using (var wrongHeaderFileStream = new StreamReader(_invalidDATWithWrongHeaderFilePath))
                {
                    _target.Transform(wrongHeaderFileStream);
                }

            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(InvalidFileFormatException));
                Assert.IsTrue(ex.Message.ToUpperInvariant() == expectedExceptionMessage.ToUpperInvariant());
            }
        }
    }
}
