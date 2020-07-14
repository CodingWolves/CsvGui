namespace CsvGui
{
    partial class FormLoader
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
            this.LoadButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.FilePathTextBox = new System.Windows.Forms.TextBox();
            this.FilePathDialog = new System.Windows.Forms.OpenFileDialog();
            this.FilePathOpenDialog = new System.Windows.Forms.Button();
            this.FilePathOpenFolder = new System.Windows.Forms.Button();
            this.HeadCheckBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // LoadButton
            // 
            this.LoadButton.Location = new System.Drawing.Point(275, 53);
            this.LoadButton.Name = "LoadButton";
            this.LoadButton.Size = new System.Drawing.Size(136, 59);
            this.LoadButton.TabIndex = 0;
            this.LoadButton.Text = "Load Csv File";
            this.LoadButton.UseVisualStyleBackColor = true;
            this.LoadButton.Click += new System.EventHandler(this.LoadButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "File Path";
            // 
            // FilePathTextBox
            // 
            this.FilePathTextBox.Location = new System.Drawing.Point(12, 25);
            this.FilePathTextBox.Name = "FilePathTextBox";
            this.FilePathTextBox.Size = new System.Drawing.Size(232, 20);
            this.FilePathTextBox.TabIndex = 2;
            // 
            // FilePathDialog
            // 
            this.FilePathDialog.FileName = "openFileDialog1";
            this.FilePathDialog.Filter = "Csv|*.csv;";
            // 
            // FilePathOpenDialog
            // 
            this.FilePathOpenDialog.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(254)));
            this.FilePathOpenDialog.Location = new System.Drawing.Point(253, 25);
            this.FilePathOpenDialog.Name = "FilePathOpenDialog";
            this.FilePathOpenDialog.Size = new System.Drawing.Size(24, 20);
            this.FilePathOpenDialog.TabIndex = 0;
            this.FilePathOpenDialog.Text = "...";
            this.FilePathOpenDialog.UseVisualStyleBackColor = true;
            this.FilePathOpenDialog.Click += new System.EventHandler(this.FilePathOpenDialog_Click);
            // 
            // FilePathOpenFolder
            // 
            this.FilePathOpenFolder.Location = new System.Drawing.Point(283, 24);
            this.FilePathOpenFolder.Name = "FilePathOpenFolder";
            this.FilePathOpenFolder.Size = new System.Drawing.Size(75, 23);
            this.FilePathOpenFolder.TabIndex = 3;
            this.FilePathOpenFolder.Text = "Open Folder";
            this.FilePathOpenFolder.UseVisualStyleBackColor = true;
            this.FilePathOpenFolder.Click += new System.EventHandler(this.FilePathOpenFolder_Click);
            // 
            // HeadCheckBox
            // 
            this.HeadCheckBox.AutoSize = true;
            this.HeadCheckBox.Location = new System.Drawing.Point(12, 51);
            this.HeadCheckBox.Name = "HeadCheckBox";
            this.HeadCheckBox.Size = new System.Drawing.Size(104, 17);
            this.HeadCheckBox.TabIndex = 4;
            this.HeadCheckBox.Text = "Column Headers";
            this.HeadCheckBox.UseVisualStyleBackColor = true;
            // 
            // FormLoader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(423, 122);
            this.Controls.Add(this.HeadCheckBox);
            this.Controls.Add(this.FilePathOpenFolder);
            this.Controls.Add(this.FilePathTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.FilePathOpenDialog);
            this.Controls.Add(this.LoadButton);
            this.Name = "FormLoader";
            this.Text = "FormLoader";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button LoadButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox FilePathTextBox;
        private System.Windows.Forms.OpenFileDialog FilePathDialog;
        private System.Windows.Forms.Button FilePathOpenDialog;
        private System.Windows.Forms.Button FilePathOpenFolder;
        private System.Windows.Forms.CheckBox HeadCheckBox;
    }
}