using System;
using System.Collections.Generic;
using System.IO;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FootballExcerciseService.Services;
using FootballExcerciseService.Models;
using FootballExerciseUtilities.Exceptions;

namespace FootballExercise
{
    public partial class FootballExercise : Form
    {
        public FootballExercise()
        {
            InitializeComponent();
        }

        private void buttonFileBrowse_Click(object sender, EventArgs e)
        {
            ResetControls();
            var teams = new List<EnglishPremierLeagueTeam>();
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = "Select a file to import.";
                openFileDialog.Filter = "CSV or Data files|*.dat;*.csv";
                try
                {
                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        var footballExerciseService = new EnglishPremierLeagueService();
                        var fileExtension = Path.GetExtension(openFileDialog.FileName);
                        var fileType = GetFileExtnsionType(fileExtension);
                        StreamReader fileStream;
                        try
                        {
                            fileStream = new StreamReader(openFileDialog.OpenFile());
                        }
                        catch (Exception)
                        {
                            throw new ImportFileInUseException("The file used for upload is currently being used by another process");
                        }
                        var transformer = footballExerciseService.GetTransformer(fileType);
                        var englishPremierLeagueTeams = transformer.Transform(fileStream);
                        var englishPremierLeagueTeam = footballExerciseService.GetTeamWithLeastGoalDifference(englishPremierLeagueTeams);
                        labelLeastGoalDifferenceTeam.Text = englishPremierLeagueTeam != null ? englishPremierLeagueTeam.Name : string.Empty;
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

        private static FileExtensionType GetFileExtnsionType(string fileExtension)
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
