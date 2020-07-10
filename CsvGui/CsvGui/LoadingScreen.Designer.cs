namespace CsvGui
{
    partial class LoadingScreen
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
            this.LoadingLabel = new System.Windows.Forms.Label();
            this.ProgressLabel = new System.Windows.Forms.Label();
            this.LoadingNameLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // LoadingLabel
            // 
            this.LoadingLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(254)));
            this.LoadingLabel.Location = new System.Drawing.Point(12, 9);
            this.LoadingLabel.Name = "LoadingLabel";
            this.LoadingLabel.Size = new System.Drawing.Size(241, 39);
            this.LoadingLabel.TabIndex = 0;
            this.LoadingLabel.Text = "Loading";
            this.LoadingLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.LoadingLabel.UseWaitCursor = true;
            // 
            // ProgressLabel
            // 
            this.ProgressLabel.Font = new System.Drawing.Font("Magneto", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(254)));
            this.ProgressLabel.Location = new System.Drawing.Point(113, 112);
            this.ProgressLabel.Name = "ProgressLabel";
            this.ProgressLabel.Size = new System.Drawing.Size(39, 36);
            this.ProgressLabel.TabIndex = 1;
            this.ProgressLabel.Text = "...";
            this.ProgressLabel.UseWaitCursor = true;
            // 
            // LoadingNameLabel
            // 
            this.LoadingNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(254)));
            this.LoadingNameLabel.Location = new System.Drawing.Point(12, 48);
            this.LoadingNameLabel.Name = "LoadingNameLabel";
            this.LoadingNameLabel.Size = new System.Drawing.Size(241, 64);
            this.LoadingNameLabel.TabIndex = 2;
            this.LoadingNameLabel.Text = "Form";
            this.LoadingNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.LoadingNameLabel.UseWaitCursor = true;
            // 
            // LoadingScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(265, 157);
            this.Controls.Add(this.LoadingNameLabel);
            this.Controls.Add(this.ProgressLabel);
            this.Controls.Add(this.LoadingLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "LoadingScreen";
            this.Text = "LoadingScreen";
            this.UseWaitCursor = true;
            this.Shown += new System.EventHandler(this.LoadingScreen_Shown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label LoadingLabel;
        private System.Windows.Forms.Label ProgressLabel;
        private System.Windows.Forms.Label LoadingNameLabel;
    }
}