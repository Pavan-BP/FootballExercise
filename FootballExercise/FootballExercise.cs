using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FootballExcerciseService.Services;
using FootballExcerciseService.Models;
using FootballExerciseUtilities.Exceptions;
using FootballExcerciseService.Transformers;

namespace FootballExercise
{
    public partial class FootballExercise : Form
    {
        private IEnglishPremierLeagueService _englishPremierLeagueService;
        public FootballExercise(IEnglishPremierLeagueService englishPremierLeagueService)
        {
            InitializeComponent();
            _englishPremierLeagueService = englishPremierLeagueService;
        }

        private void buttonFileBrowse_Click(object sender, EventArgs e)
        {
            ResetControls();
            List<EnglishPremierLeagueTeam> englishPremierLeagueTeams;
            using (var openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = "Select a file to import.";
                openFileDialog.Filter = "CSV or Data files|*.dat;*.csv";
                try
                {
                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        var fileExtension = Path.GetExtension(openFileDialog.FileName);
                        var fileExtensionType = GetFileExtensionType(fileExtension);
                        StreamReader fileStream;
                        try
                        {
                            using (fileStream = new StreamReader(openFileDialog.OpenFile()))
                            {
                                englishPremierLeagueTeams = _englishPremierLeagueService.GetTeamsWithLeastGoalDifference(fileStream, fileExtensionType);
                            }                                
                        }
                        catch (Exception)
                        {
                            throw new ImportFileInUseException();
                        }
                                                
                        if(englishPremierLeagueTeams !=null && englishPremierLeagueTeams.Any())
                        {
                            var teamNamesWithLeastGoalDifference = new StringBuilder();
                            foreach (var englishPremierLeagueTeam in englishPremierLeagueTeams)
                            {
                                teamNamesWithLeastGoalDifference.Append(englishPremierLeagueTeam.Name);
                                teamNamesWithLeastGoalDifference.Append(", ");
                            }
                            labelLeastGoalDifferenceTeam.Text = teamNamesWithLeastGoalDifference.ToString().TrimEnd(',', ' ');
                        }                        
                    }
                }
                catch (FootballExerciseException ex)
                {
                    ShowErrorMessage(ex.Message);
                }
                catch (Exception)
                {
                    ShowErrorMessage("There was an issue when uploading the file. Please try again later.");
                }
            }
        }

        private void ShowErrorMessage(string message)
        {
            labelErrorMessage.Text = message;
        }

        private static FileExtensionType GetFileExtensionType(string fileExtension)
        {
            switch (fileExtension.ToUpperInvariant())
            {
                case ".CSV":
                    return FileExtensionType.CSV;
                case ".DAT":
                    return FileExtensionType.DAT;
                default:
                    return FileExtensionType.OTHER;
            }
        }

        private void ResetControls()
        {
            labelErrorMessage.Text = string.Empty;
            labelLeastGoalDifferenceTeam.Text = string.Empty;
        }
    }
}
