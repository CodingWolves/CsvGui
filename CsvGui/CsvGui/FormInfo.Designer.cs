namespace CsvGui
{
    partial class FormInfo
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
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
            "123"}, -1, System.Drawing.Color.Empty, System.Drawing.SystemColors.Window, null);
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("5643");
            System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem("hgj");
            this.label1 = new System.Windows.Forms.Label();
            this.FilenameTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.ColumnHeaderComboBox = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.RowCountTextBox = new System.Windows.Forms.TextBox();
            this.ColumnHeaderValuesPanel = new System.Windows.Forms.Panel();
            this.NullValuesLabel = new System.Windows.Forms.Label();
            this.ColumnHeaderValuesListView = new System.Windows.Forms.ListView();
            this.UniqueValuesLabel = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.ColumnIndexComboBox = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.OpenGridViewButton = new System.Windows.Forms.Button();
            this.ColumnHeaderValuesPanel.SuspendLayout();
            this.panel2.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "File Name";
            // 
            // FilenameTextBox
            // 
            this.FilenameTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(254)));
            this.FilenameTextBox.Location = new System.Drawing.Point(77, 27);
            this.FilenameTextBox.Multiline = true;
            this.FilenameTextBox.Name = "FilenameTextBox";
            this.FilenameTextBox.ReadOnly = true;
            this.FilenameTextBox.Size = new System.Drawing.Size(284, 62);
            this.FilenameTextBox.TabIndex = 1;
            this.FilenameTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Column";
            // 
            // ColumnHeaderComboBox
            // 
            this.ColumnHeaderComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ColumnHeaderComboBox.FormattingEnabled = true;
            this.ColumnHeaderComboBox.Items.AddRange(new object[] {
            "56",
            "58",
            "5876",
            "5876",
            "65776",
            "78",
            "8"});
            this.ColumnHeaderComboBox.Location = new System.Drawing.Point(66, 20);
            this.ColumnHeaderComboBox.MaxDropDownItems = 100;
            this.ColumnHeaderComboBox.Name = "ColumnHeaderComboBox";
            this.ColumnHeaderComboBox.Size = new System.Drawing.Size(102, 21);
            this.ColumnHeaderComboBox.TabIndex = 3;
            this.ColumnHeaderComboBox.SelectedIndexChanged += new System.EventHandler(this.ColumnHeaderComboBox_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 98);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Row Count";
            // 
            // RowCountTextBox
            // 
            this.RowCountTextBox.Location = new System.Drawing.Point(77, 95);
            this.RowCountTextBox.Name = "RowCountTextBox";
            this.RowCountTextBox.ReadOnly = true;
            this.RowCountTextBox.Size = new System.Drawing.Size(100, 20);
            this.RowCountTextBox.TabIndex = 1;
            // 
            // ColumnHeaderValuesPanel
            // 
            this.ColumnHeaderValuesPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ColumnHeaderValuesPanel.Controls.Add(this.NullValuesLabel);
            this.ColumnHeaderValuesPanel.Controls.Add(this.ColumnHeaderValuesListView);
            this.ColumnHeaderValuesPanel.Controls.Add(this.UniqueValuesLabel);
            this.ColumnHeaderValuesPanel.Location = new System.Drawing.Point(3, 47);
            this.ColumnHeaderValuesPanel.Name = "ColumnHeaderValuesPanel";
            this.ColumnHeaderValuesPanel.Padding = new System.Windows.Forms.Padding(5);
            this.ColumnHeaderValuesPanel.Size = new System.Drawing.Size(380, 125);
            this.ColumnHeaderValuesPanel.TabIndex = 4;
            this.ColumnHeaderValuesPanel.Visible = false;
            // 
            // NullValuesLabel
            // 
            this.NullValuesLabel.AutoSize = true;
            this.NullValuesLabel.Location = new System.Drawing.Point(178, 5);
            this.NullValuesLabel.Name = "NullValuesLabel";
            this.NullValuesLabel.Size = new System.Drawing.Size(69, 13);
            this.NullValuesLabel.TabIndex = 2;
            this.NullValuesLabel.Text = "2 Null Values";
            // 
            // ColumnHeaderValuesListView
            // 
            this.ColumnHeaderValuesListView.HideSelection = false;
            listViewItem1.StateImageIndex = 0;
            this.ColumnHeaderValuesListView.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3});
            this.ColumnHeaderValuesListView.Location = new System.Drawing.Point(8, 21);
            this.ColumnHeaderValuesListView.Name = "ColumnHeaderValuesListView";
            this.ColumnHeaderValuesListView.Size = new System.Drawing.Size(364, 96);
            this.ColumnHeaderValuesListView.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.ColumnHeaderValuesListView.TabIndex = 1;
            this.ColumnHeaderValuesListView.UseCompatibleStateImageBehavior = false;
            this.ColumnHeaderValuesListView.View = System.Windows.Forms.View.SmallIcon;
            // 
            // UniqueValuesLabel
            // 
            this.UniqueValuesLabel.AutoSize = true;
            this.UniqueValuesLabel.Location = new System.Drawing.Point(8, 5);
            this.UniqueValuesLabel.Name = "UniqueValuesLabel";
            this.UniqueValuesLabel.Size = new System.Drawing.Size(85, 13);
            this.UniqueValuesLabel.TabIndex = 0;
            this.UniqueValuesLabel.Text = "3 Unique Values";
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.ColumnIndexComboBox);
            this.panel2.Controls.Add(this.ColumnHeaderValuesPanel);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.ColumnHeaderComboBox);
            this.panel2.Location = new System.Drawing.Point(11, 121);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(390, 179);
            this.panel2.TabIndex = 5;
            // 
            // ColumnIndexComboBox
            // 
            this.ColumnIndexComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ColumnIndexComboBox.FormattingEnabled = true;
            this.ColumnIndexComboBox.Location = new System.Drawing.Point(174, 20);
            this.ColumnIndexComboBox.MaxDropDownItems = 100;
            this.ColumnIndexComboBox.Name = "ColumnIndexComboBox";
            this.ColumnIndexComboBox.Size = new System.Drawing.Size(49, 21);
            this.ColumnIndexComboBox.TabIndex = 6;
            this.ColumnIndexComboBox.SelectedIndexChanged += new System.EventHandler(this.ColumnIndexComboBox_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(181, 4);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(33, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "Index";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(98, 4);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "Name";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 6;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.DefaultExt = "csv";
            this.saveFileDialog.Filter = "Csv|*.csv;";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.DefaultExt = "csv";
            this.openFileDialog.FileName = "form.csv";
            this.openFileDialog.Filter = "Csv|*.csv;";
            // 
            // OpenGridViewButton
            // 
            this.OpenGridViewButton.Location = new System.Drawing.Point(11, 306);
            this.OpenGridViewButton.Name = "OpenGridViewButton";
            this.OpenGridViewButton.Size = new System.Drawing.Size(99, 23);
            this.OpenGridViewButton.TabIndex = 7;
            this.OpenGridViewButton.Text = "Open Grid View";
            this.OpenGridViewButton.UseVisualStyleBackColor = true;
            this.OpenGridViewButton.Click += new System.EventHandler(this.OpenGridViewButton_Click);
            // 
            // FormInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.OpenGridViewButton);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.RowCountTextBox);
            this.Controls.Add(this.FilenameTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormInfo";
            this.Text = "FormInfo";
            this.ColumnHeaderValuesPanel.ResumeLayout(false);
            this.ColumnHeaderValuesPanel.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox FilenameTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox ColumnHeaderComboBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox RowCountTextBox;
        private System.Windows.Forms.Panel ColumnHeaderValuesPanel;
        private System.Windows.Forms.ListView ColumnHeaderValuesListView;
        private System.Windows.Forms.Label UniqueValuesLabel;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ComboBox ColumnIndexComboBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label NullValuesLabel;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Button OpenGridViewButton;
    }
}