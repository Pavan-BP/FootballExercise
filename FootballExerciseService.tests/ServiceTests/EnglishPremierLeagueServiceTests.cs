using System.IO;
using Moq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FootballExcerciseService.Services;
using FootballExcerciseService.Transformers;
using FootballExerciseService.Tests.ObjectMother;
using FootballExcerciseService.Models;

namespace FootballExerciseService.Tests.ServiceTests
{
    [TestClass]
    public class EnglishPremierLeagueServiceTests
    {
        private EnglishPremierLeagueService _target;
        private Mock<ITransformer> _transformer;
        private Mock<ITransformerFactory> _transformerFactory;

        [TestInitialize]
        public void Init()
        {
            _transformer = new Mock<ITransformer>();
            _transformerFactory = new Mock<ITransformerFactory>();

            _transformer.Setup(x => x.Transform(It.IsAny<StreamReader>())).Returns(EnglishPremierLeagueObjectMother.GetEnglishPremierLeagueTeams());
            _transformerFactory.Setup(x => x.FetchTransformer(It.IsAny<FileExtensionType>())).Returns(_transformer.Object);

            _target = new EnglishPremierLeagueService(_transformerFactory.Object);

        }

        [TestMethod]
        public void EnglishPremierLeagueService_GetTeamWithLeastGoalDifference_Returns_Right_Result_On_Single_Team_With_Least_GoalDifference_On_CSV_Upload()
        {
            //Arrange
            
            //Act
            var result = _target.GetTeamsWithLeastGoalDifference(It.IsAny<StreamReader>(), FileExtensionType.CSV);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(List<EnglishPremierLeagueTeam>));
            Assert.IsTrue((result as List<EnglishPremierLeagueTeam>).Count > 0);
        }

        [TestMethod]
        public void EnglishPremierLeagueService_GetTeamWithLeastGoalDifference_Returns_Right_Result_On_Single_Team_With_Least_GoalDifference_On_DAT_Upload()
        {
            //Arrange

            //Act
            var result = _target.GetTeamsWithLeastGoalDifference(It.IsAny<StreamReader>(), FileExtensionType.DAT);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(List<EnglishPremierLeagueTeam>));
            Assert.IsTrue((result as List<EnglishPremierLeagueTeam>).Count > 0);
        }

        [TestMethod]
        public void EnglishPremierLeagueService_GetTeamWithLeastGoalDifference_Fetches_Right_Result_On_Multiple_Teams_With_Same_GoalDifference_On_CSV_Upload()
        {
            //Arrange
            _transformer.Setup(x => x.Transform(It.IsAny<StreamReader>())).
                Returns(EnglishPremierLeagueObjectMother.GetEnglishPremierLeagueTeamsWithSameGoalDifference());

            //Act
            var result = _target.GetTeamsWithLeastGoalDifference(It.IsAny<StreamReader>(), FileExtensionType.CSV);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(List<EnglishPremierLeagueTeam>));
            Assert.IsTrue((result as List<EnglishPremierLeagueTeam>).Count >  0);
        }

        [TestMethod]
        public void EnglishPremierLeagueService_GetTeamWithLeastGoalDifference_Fetches_Right_Result_On_Multiple_Teams_With_Same_GoalDifference_On_DAT_Upload()
        {
            //Arrange
            _transformer.Setup(x => x.Transform(It.IsAny<StreamReader>())).
                Returns(EnglishPremierLeagueObjectMother.GetEnglishPremierLeagueTeamsWithSameGoalDifference());

            //Act
            var result = _target.GetTeamsWithLeastGoalDifference(It.IsAny<StreamReader>(), FileExtensionType.DAT);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(List<EnglishPremierLeagueTeam>));
            Assert.IsTrue((result as List<EnglishPremierLeagueTeam>).Count > 0);
        }

        [TestMethod]
        public void EnglishPremierLeagueService_GetTeamWithLeastGoalDifference_Returns_Null_On_Empty_CSV_File_Upload()
        {
            //Arrange
            _transformer.Setup(x => x.Transform(It.IsAny<StreamReader>())).Returns(new List<EnglishPremierLeagueTeam>());

            //Act
            var result = _target.GetTeamsWithLeastGoalDifference(null,FileExtensionType.CSV);

            //Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void EnglishPremierLeagueService_GetTeamWithLeastGoalDifference_Returns_Null_On_Empty_DAT_File_Upload()
        {
            //Arrange
            _transformer.Setup(x => x.Transform(It.IsAny<StreamReader>())).Returns(new List<EnglishPremierLeagueTeam>());

            //Act
            var result = _target.GetTeamsWithLeastGoalDifference(null, FileExtensionType.DAT);

            //Assert
            Assert.IsNull(result);
        }
    }
}
