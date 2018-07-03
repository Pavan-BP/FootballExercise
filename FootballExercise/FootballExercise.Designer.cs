namespace FootballExercise
{
    partial class FootballExercise
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.labelErrorMessage = new System.Windows.Forms.Label();
            this.labelLeastGoalDifference = new System.Windows.Forms.Label();
            this.labelLeastGoalDifferenceTeam = new System.Windows.Forms.Label();
            this.buttonFileBrowse = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.labelErrorMessage);
            this.flowLayoutPanel1.Controls.Add(this.labelLeastGoalDifference);
            this.flowLayoutPanel1.Controls.Add(this.labelLeastGoalDifferenceTeam);
            this.flowLayoutPanel1.Controls.Add(this.buttonFileBrowse);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(12, 12);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(538, 157);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // labelErrorMessage
            // 
            this.labelErrorMessage.AutoSize = true;
            this.labelErrorMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelErrorMessage.ForeColor = System.Drawing.Color.Red;
            this.labelErrorMessage.Location = new System.Drawing.Point(5, 5);
            this.labelErrorMessage.Margin = new System.Windows.Forms.Padding(5);
            this.labelErrorMessage.MinimumSize = new System.Drawing.Size(455, 0);
            this.labelErrorMessage.Name = "labelErrorMessage";
            this.labelErrorMessage.Size = new System.Drawing.Size(455, 16);
            this.labelErrorMessage.TabIndex = 0;
            // 
            // labelLeastGoalDifference
            // 
            this.labelLeastGoalDifference.AutoSize = true;
            this.labelLeastGoalDifference.Location = new System.Drawing.Point(5, 31);
            this.labelLeastGoalDifference.Margin = new System.Windows.Forms.Padding(5, 5, 0, 5);
            this.labelLeastGoalDifference.MinimumSize = new System.Drawing.Size(160, 13);
            this.labelLeastGoalDifference.Name = "labelLeastGoalDifference";
            this.labelLeastGoalDifference.Size = new System.Drawing.Size(183, 13);
            this.labelLeastGoalDifference.TabIndex = 1;
            this.labelLeastGoalDifference.Text = "Teams with the least goal difference: ";
            // 
            // labelLeastGoalDifferenceTeam
            // 
            this.labelLeastGoalDifferenceTeam.AutoSize = true;
            this.labelLeastGoalDifferenceTeam.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelLeastGoalDifferenceTeam.Location = new System.Drawing.Point(188, 31);
            this.labelLeastGoalDifferenceTeam.Margin = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.labelLeastGoalDifferenceTeam.MinimumSize = new System.Drawing.Size(280, 0);
            this.labelLeastGoalDifferenceTeam.Name = "labelLeastGoalDifferenceTeam";
            this.labelLeastGoalDifferenceTeam.Size = new System.Drawing.Size(280, 13);
            this.labelLeastGoalDifferenceTeam.TabIndex = 2;
            // 
            // buttonFileBrowse
            // 
            this.buttonFileBrowse.Location = new System.Drawing.Point(5, 54);
            this.buttonFileBrowse.Margin = new System.Windows.Forms.Padding(5);
            this.buttonFileBrowse.Name = "buttonFileBrowse";
            this.buttonFileBrowse.Size = new System.Drawing.Size(144, 23);
            this.buttonFileBrowse.TabIndex = 3;
            this.buttonFileBrowse.Text = "Browse a csv or a dat file";
            this.buttonFileBrowse.UseVisualStyleBackColor = true;
            this.buttonFileBrowse.Click += new System.EventHandler(this.buttonFileBrowse_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog";
            // 
            // FootballExercise
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(562, 181);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "FootballExercise";
            this.Text = "FootballExercise";
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Label labelErrorMessage;
        private System.Windows.Forms.Label labelLeastGoalDifference;
        private System.Windows.Forms.Label labelLeastGoalDifferenceTeam;
        private System.Windows.Forms.Button buttonFileBrowse;
    }
}