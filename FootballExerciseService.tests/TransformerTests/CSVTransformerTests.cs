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

namespace FootballExerciseService.tests.TransformerTests
{
    [TestClass]
    public class CSVTransformerTests
    {
        private CSVTransformer _target;
        private string currentWorkingDirectoryPath; 
        private StreamReader _validCSVStreamReader;
        private string _validCSVFilePath;
        private string _emptyCSVFilePath;
        private string _invalidCSVFilePath;

        [TestInitialize]
        public void Init()
        {
            currentWorkingDirectoryPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            _validCSVFilePath = currentWorkingDirectoryPath + @"\TestData\footballvalid.csv";
            _emptyCSVFilePath = currentWorkingDirectoryPath + @"\TestData\EmptyFile.csv";
            _invalidCSVFilePath = currentWorkingDirectoryPath + @"\TestData\FootballInvalidColumn.csv";
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
            using (var emptyFileStream = new StreamReader(_emptyCSVFilePath))
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
        public void CSVTransformer_Transform_Method_Throws_An_Exception_On_Uploading_CSV_File_With_Invalid_Data()
        {
            //Arrange
            using (var invalidFileStream = new StreamReader(_invalidCSVFilePath))
            {
                try
                {
                    //Act
                    _target.Transform(invalidFileStream);
                }
                catch (Exception ex)
                {
                    //Assert
                    Assert.IsInstanceOfType(ex, typeof(InvalidColumnTypeOnTransformationException));
                }
            }
        }

        [TestMethod]
        public void CSVTransformer_Transform_Method_Throws_An_Exception_With_Invalid_Column_And_Row_Number_On_Uploading_CSV_File_With_Invalid_Data()
        {
            //Arrange
            var expectedErrorMessage = "Invalid value in column: L on Linenumber: 2.";
            using (var invalidFileStream = new StreamReader(_invalidCSVFilePath))
            {
                try
                {
                    //Act
                    _target.Transform(invalidFileStream);
                }
                catch (Exception ex)
                {
                    //Assert
                    Assert.IsInstanceOfType(ex, typeof(InvalidColumnTypeOnTransformationException));
                    Assert.IsTrue(ex.Message.ToUpperInvariant() == expectedErrorMessage.ToUpperInvariant());
                }
            }
        }




        ////not working
        //[TestMethod]
        //public void CSVTransformer_Transform_Method_Throws_An_Exception_On_Trying_To_Import_An_Open_CSV_File()
        //{
        //    File.OpenRead(_validCSVFilePath);

        //    Assert.ThrowsException<ImportFileInUseException>(() => Transform_Method_Returns_List_Of_PremierLeagueTeams_On_Valid_CSV_Import());


        //}
    }
}
