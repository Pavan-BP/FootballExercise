using FootballExcerciseService.Services;
using FootballExcerciseService.Transformers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace FootballExercise.FootballExerciseUnityConfiguration
{
    internal static class FootballExerciseUnityContainer
    {
        public static IUnityContainer Container;

        static FootballExerciseUnityContainer()
        {
            if(Container == null)
            {
                Container = new UnityContainer();
            }
        }

    }

    public static class FootballExerciseContainerConfigurator
    {
        public static IUnityContainer FootballExerciseContainer { get; set; }
        public static void RegisterDependencies()
        {
            FootballExerciseContainer = FootballExerciseUnityContainer.Container;

            FootballExerciseContainer.RegisterType<IEnglishPremierLeagueService, EnglishPremierLeagueService>();
            FootballExerciseContainer.RegisterType<ITransformerFactory, TransformerFactory>();
        }
    }
}
