using FootballExcerciseService.Services;
using FootballExercise.FootballExerciseUnityConfiguration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unity;

namespace FootballExercise
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            FootballExerciseContainerConfigurator.RegisterDependencies();
            var englishPremierLeagueService = FootballExerciseContainerConfigurator.FootballExerciseContainer.Resolve<IEnglishPremierLeagueService>();
            Application.Run(new FootballExercise(englishPremierLeagueService));
        }
    }
}
