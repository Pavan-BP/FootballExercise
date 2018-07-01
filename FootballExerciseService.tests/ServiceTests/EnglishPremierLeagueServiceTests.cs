using System;
using System.IO;
using Moq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FootballExcerciseService.Services;
using FootballExcerciseService.Transformers;
using FootballExerciseService.Tests.ObjectMother;
using FootballExcerciseService.Models;
using FootballExerciseUtilities.Exceptions;

namespace FootballExerciseService.Tests
{
    [TestClass]
    public class EnglishPremierLeagueServiceTests
    {
        private EnglishPremierLeagueService _target;
        private Mock<ITransformer> _transformer;
        private string currentWorkingDirectoryPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + @"\TestData\";

        [TestInitialize]
        public void Init()
        {
            _transformer = new Mock<ITransformer>();
            //_transformer.Setup(x => x.Transform(new StreamReader(currentWorkingDirectoryPath + @"\test.txt"))).
            _transformer.Setup(x => x.Transform(null)).
                Returns(EnglishPremierLeagueObjectMother.GetEnglishPremierLeagueTeams());
            _target = new EnglishPremierLeagueService(_transformer.Object);

        }

        [TestMethod]
        public void GetTeamWithLeastGoalDifference_Fetches_Right_Result_On_CSV_File_Upload()
        {
            //Arrange

            //Act
            var result = _target.GetTeamsWithLeastGoalDifference(null, FileExtensionType.CSV);
            

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(List<EnglishPremierLeagueTeam>));
            Assert.AreEqual((result as List<EnglishPremierLeagueTeam>).Count, 1);
            Assert.IsTrue((result as List<EnglishPremierLeagueTeam>)[0].Name.ToUpperInvariant() == "CHELSEA");
        }

        [TestMethod]
        public void GetTeamWithLeastGoalDifference_Fetches_Right_Result_On_DAT_File_Upload()
        {
            //Arrange

            //Act
            var result = _target.GetTeamsWithLeastGoalDifference(null, FileExtensionType.DAT);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(List<EnglishPremierLeagueTeam>));
            Assert.AreEqual((result as List<EnglishPremierLeagueTeam>).Count, 1);
            Assert.IsTrue((result as List<EnglishPremierLeagueTeam>)[0].Name.ToUpperInvariant() == "CHELSEA");
        }

        [TestMethod]
        public void GetTeamWithLeastGoalDifference_Returns_Null_On_Empty_File_Upload()
        {
            //Arrange
            _transformer.Setup(x => x.Transform(null)).Returns(new List<EnglishPremierLeagueTeam>());
            //Act
            var result = _target.GetTeamsWithLeastGoalDifference(null, FileExtensionType.OTHER);

            Assert.IsNull(result);
            

        }
    }
}
