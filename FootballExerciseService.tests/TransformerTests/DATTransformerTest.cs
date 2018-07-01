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
        private string _emptyDATFilePath;
        private string _invalidDATFilePath;

        [TestInitialize]
        public void Init()
        {
            currentWorkingDirectoryPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            _validDATFilePath = currentWorkingDirectoryPath + @"\TestData\footballvalid.dat";
            _emptyDATFilePath = currentWorkingDirectoryPath + @"\TestData\EmptyFile.dat";
            _invalidDATFilePath = currentWorkingDirectoryPath + @"\TestData\FootballInvalidColumn.dat";
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
            using (var emptyFileStream = new StreamReader(_emptyDATFilePath))
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
        public void DATTransformer_Transform_Method_Throws_An_Exception_On_Uploading_DAT_File_With_Invalid_Data()
        {
            //Arrange
            using (var invalidFileStream = new StreamReader(_invalidDATFilePath))
            {
                try
                {
                    //Act
                    _target.Transform(invalidFileStream);
                }
                catch (Exception ex)
                {
                    //Assert
                    Assert.IsInstanceOfType(ex, typeof(InvalidFileFormatException));
                }
            }
        }

        [TestMethod]
        public void DATTransformer_Transform_Method_Throws_An_Exception_With_Invalid_Column_And_Row_Number_On_Uploading_DAT_File_With_Invalid_Data()
        {
            //Arrange
            var expectedErrorMessage = "Invalid value in column: Pts on Linenumber: 22.";
            using (var invalidFileStream = new StreamReader(_invalidDATFilePath))
            {
                try
                {
                    //Act
                    _target.Transform(invalidFileStream);
                }
                catch (Exception ex)
                {
                    //Assert
                    Assert.IsInstanceOfType(ex, typeof(InvalidFileFormatException));
                    Assert.IsTrue(ex.Message.ToUpperInvariant() == expectedErrorMessage.ToUpperInvariant());
                }
            }
        }
    }
}
