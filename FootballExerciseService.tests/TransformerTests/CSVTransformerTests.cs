using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FootballExcerciseService.Transformers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FootballExcerciseService.Models;
using Moq;
using FootballExerciseUtilities.Exceptions;

namespace FootballExerciseService.Tests.TransformerTests
{
    [TestClass]
    public class CSVTransformerTests
    {
        private CSVTransformer _target;
        private string currentWorkingDirectoryPath; 
        private StreamReader _validCSVStreamReader;
        private string _validCSVFilePath;
        private string _validCSVExtraRowsFilePath;
        private string _validCSVRandomRowSeparatorFilePath;
        private string _invalidEmptyCSVFilePath;
        private string _invalidCSVColumnsInterchangedFilePath;
        private string _invalidCSVEmptyRownInBetweenFilePath;
        private string _invalidCSVExtraColumnsFilePath;        
        private string _invalidCSVWithInCorrectDataFilePath;
        private string _invalidCSVLesserColumnsFilePath;        
        private string _invalidCSVWithWrongHeaderFilePath;

        [TestInitialize]
        public void Init()
        {
            currentWorkingDirectoryPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;

            _validCSVFilePath = currentWorkingDirectoryPath + @"\TestData\CorrectDataAndFormat\football.csv";
            _validCSVRandomRowSeparatorFilePath = currentWorkingDirectoryPath + @"\TestData\RandomRowSeparator\FootballRandomRowSeparator.csv";
            _validCSVExtraRowsFilePath = currentWorkingDirectoryPath + @"\TestData\ExtraRows\FootballWithExtraRows.csv";
            _invalidEmptyCSVFilePath = currentWorkingDirectoryPath + @"\TestData\EmptyFiles\EmptyFile.csv";
            _invalidCSVColumnsInterchangedFilePath = currentWorkingDirectoryPath + @"\TestData\ColumnsInterchanged\FootballColumnInterchanged.csv";
            _invalidCSVEmptyRownInBetweenFilePath = currentWorkingDirectoryPath + @"\TestData\EmptyRows\FootballHavingEmptyRowsInBetween.csv";
            _invalidCSVExtraColumnsFilePath = currentWorkingDirectoryPath + @"\TestData\ExtraColumns\FootballHavingExtraColumns.csv";            
            _invalidCSVWithInCorrectDataFilePath = currentWorkingDirectoryPath + @"\TestData\IncorrectData\FootballInCorrectData.csv";
            _invalidCSVLesserColumnsFilePath = currentWorkingDirectoryPath + @"\TestData\LesserColumns\FootballWithLesserColumns.csv";            
            _invalidCSVWithWrongHeaderFilePath = currentWorkingDirectoryPath + @"\TestData\WrongColumnHeaders\FootballWrongColumnHeaders.csv";

            _target = new CSVTransformer();
        }

        [TestMethod]
        public void CSVTransformer_Transform_Method_Returns_List_Of_PremierLeagueTeams_On_Valid_CSV_Import()
        {
            using (_validCSVStreamReader = new StreamReader(_validCSVFilePath))
            {
                var result = _target.Transform(_validCSVStreamReader);

                Assert.IsNotNull(result);
                Assert.IsInstanceOfType(result, typeof(List<EnglishPremierLeagueTeam>));
                Assert.IsTrue((result as List<EnglishPremierLeagueTeam>).Count > 0);
            }
        }

        [TestMethod]
        public void CSVTransformer_Transform_Method_Throws_An_Exception_On_Uploading_Empty_CSV_File()
        {
            //Arrange
            using (var emptyFileStream = new StreamReader(_invalidEmptyCSVFilePath))
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
        public void CSVTransformer_Transform_Method_Throws_An_Exception_With_Invalid_Column_Names_On_Uploading_A_CSV_File_With_Headers_In_Wrong_Sequence()
        {
            var expectedExceptionMessage = "The columns A, P, W, L, D, F, -,  are incorrect or not in correct sequence.";
            try
            {
                using (var inCorrectColumnFileStream = new StreamReader(_invalidCSVColumnsInterchangedFilePath))
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
        public void CSVTransformer_Transform_Method_Throws_An_Exception_With_Row_number_On_Uploading_A_CSV_File_With_Empty_Rows_InBetween()
        {
            var expectedExceptionMessage = "Invalid value in column: Team on Linenumber: 2.";
            try
            {
                using (var emptyRowsInBetweenFileStream = new StreamReader(_invalidCSVEmptyRownInBetweenFilePath))
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
        public void CSVTransformer_Transform_Method_Throws_An_Exception_On_Uploading_A_CSV_File_With_Extra_Columns()
        {
            var expectedExceptionMessage = "File has more columns than the standarad format. Cannot process the file.";
            try
            {
                using (var extraColumnsFileStream = new StreamReader(_invalidCSVExtraColumnsFilePath))
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
        public void CSVTransformer_Transform_Method_Returns_List_Of_PremierLeagueTeams_On_Valid_CSV_Import_With_More_Than_20_Records()
        {
            using (_validCSVStreamReader = new StreamReader(_validCSVExtraRowsFilePath))
            {
                var result = _target.Transform(_validCSVStreamReader);

                Assert.IsNotNull(result);
                Assert.IsInstanceOfType(result, typeof(List<EnglishPremierLeagueTeam>));
                Assert.IsTrue((result as List<EnglishPremierLeagueTeam>).Count > 20);
            }
        }

        [TestMethod]
        public void CSVTransformer_Transform_Method_Throws_An_Exception_With_Row_number_On_Uploading_A_CSV_File_With_InCorrect_Data()
        {
            var expectedExceptionMessage = "Invalid value in column: F on Linenumber: 3.";
            try
            {
                using (var inCorrectDataFileStream = new StreamReader(_invalidCSVWithInCorrectDataFilePath))
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
        public void CSVTransformer_Transform_Method_Throws_A_File_Format_Exception_On_Uploading_A_CSV_File_With_Lesser_Columns_Than_Standard_Format()
        {
            var expectedExceptionMessage = "File has lesser columns than the standarad format. Cannot process the file.";
            try
            {
                using (var lesserColumnFileStream = new StreamReader(_invalidCSVLesserColumnsFilePath))
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
        public void CSVTransformer_Transform_Method_Returns_List_Of_PremierLeagueTeams_On_Importing_a_CSV_File_With_Random_Separator()
        {
            using (_validCSVStreamReader = new StreamReader(_validCSVRandomRowSeparatorFilePath))
            {
                var result = _target.Transform(_validCSVStreamReader);

                Assert.IsNotNull(result);
                Assert.IsInstanceOfType(result, typeof(List<EnglishPremierLeagueTeam>));
                Assert.IsTrue((result as List<EnglishPremierLeagueTeam>).Count == 20);
            }
        }

        [TestMethod]
        public void CSVTransformer_Transform_Method_Throws_A_File_Format_Exception_On_Uploading_A_CSV_File_With_Header_Values_Than_Standard_Format()
        {
            var expectedExceptionMessage = "The columns Played, Won, Lost, Drawn, For, Against, Points,  are incorrect or not in correct sequence.";
            try
            {
                using (var wrongHeaderFileStream = new StreamReader(_invalidCSVWithWrongHeaderFilePath))
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
