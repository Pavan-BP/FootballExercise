using System.IO;
using Moq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FootballExcerciseService.Services;
using FootballExcerciseService.Transformers;
using FootballExerciseService.Tests.ObjectMother;
using FootballExcerciseService.Models;

namespace FootballExerciseService.Tests
{
    [TestClass]
    public class EnglishPremierLeagueServiceTests
    {
        private EnglishPremierLeagueService _target;
        private Mock<ITransformer> _transformer;
        //private string currentWorkingDirectoryPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + @"\TestData\";

        [TestInitialize]
        public void Init()
        {
            _transformer = new Mock<ITransformer>();
            _transformer.Setup(x => x.Transform(null)).
                Returns(EnglishPremierLeagueObjectMother.GetEnglishPremierLeagueTeams());
            _target = new EnglishPremierLeagueService(_transformer.Object);

        }

        [TestMethod]
        public void EnglishPremierLeagueService_GetTeamWithLeastGoalDifference_Returns_Right_Result_On_Single_Team_With_Least_GoalDifference()
        {
            //Arrange

            //Act
            var result = _target.GetTeamsWithLeastGoalDifference(null);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(List<EnglishPremierLeagueTeam>));
            Assert.IsTrue((result as List<EnglishPremierLeagueTeam>).Count > 0);
        }

        [TestMethod]
        public void EnglishPremierLeagueService_GetTeamWithLeastGoalDifference_Fetches_Right_Result_On_Multiple_Teams_With_Same_GoalDifference()
        {
            //Arrange
            _transformer.Setup(x => x.Transform(null)).
                Returns(EnglishPremierLeagueObjectMother.GetEnglishPremierLeagueTeamsWithSameGoalDifference());

            //Act
            var result = _target.GetTeamsWithLeastGoalDifference(null);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(List<EnglishPremierLeagueTeam>));
            Assert.IsTrue((result as List<EnglishPremierLeagueTeam>).Count >  0);
        }

        [TestMethod]
        public void EnglishPremierLeagueService_GetTeamWithLeastGoalDifference_Returns_Null_On_Empty_File_Upload()
        {
            //Arrange
            _transformer.Setup(x => x.Transform(null)).Returns(new List<EnglishPremierLeagueTeam>());

            //Act
            var result = _target.GetTeamsWithLeastGoalDifference(null);

            //Assert
            Assert.IsNull(result);
        }
    }
}
