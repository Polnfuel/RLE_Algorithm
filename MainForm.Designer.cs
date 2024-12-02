namespace RLE_Algorithm
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            SelectFileButton = new Button();
            filenameLabel = new Label();
            label1 = new Label();
            InputTextBox = new TextBox();
            OpenFileDialog = new OpenFileDialog();
            ArchiveButton = new Button();
            label2 = new Label();
            OutputTextBox = new TextBox();
            label3 = new Label();
            UnarchiveButton = new Button();
            SaveToFileButton = new Button();
            SaveFileDialog = new SaveFileDialog();
            ratioLabel = new Label();
            SuspendLayout();
            // 
            // SelectFileButton
            // 
            SelectFileButton.Location = new Point(53, 370);
            SelectFileButton.Name = "SelectFileButton";
            SelectFileButton.Size = new Size(119, 29);
            SelectFileButton.TabIndex = 0;
            SelectFileButton.Text = "Выбрать файл";
            SelectFileButton.UseVisualStyleBackColor = true;
            SelectFileButton.Click += SelectFileButton_Click;
            // 
            // filenameLabel
            // 
            filenameLabel.AutoSize = true;
            filenameLabel.Location = new Point(53, 402);
            filenameLabel.Name = "filenameLabel";
            filenameLabel.Size = new Size(50, 20);
            filenameLabel.TabIndex = 1;
            filenameLabel.Text = "label1";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(68, 4);
            label1.Name = "label1";
            label1.Size = new Size(107, 20);
            label1.TabIndex = 2;
            label1.Text = "Входной текст";
            // 
            // InputTextBox
            // 
            InputTextBox.Location = new Point(12, 27);
            InputTextBox.Multiline = true;
            InputTextBox.Name = "InputTextBox";
            InputTextBox.Size = new Size(239, 337);
            InputTextBox.TabIndex = 3;
            // 
            // ArchiveButton
            // 
            ArchiveButton.Location = new Point(341, 12);
            ArchiveButton.Name = "ArchiveButton";
            ArchiveButton.Size = new Size(121, 29);
            ArchiveButton.TabIndex = 6;
            ArchiveButton.Text = "Архивировать";
            ArchiveButton.UseVisualStyleBackColor = true;
            ArchiveButton.Click += ArchiveButton_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(405, 75);
            label2.Name = "label2";
            label2.Size = new Size(118, 20);
            label2.TabIndex = 7;
            label2.Text = "Выходной текст";
            // 
            // OutputTextBox
            // 
            OutputTextBox.Location = new Point(341, 98);
            OutputTextBox.Multiline = true;
            OutputTextBox.Name = "OutputTextBox";
            OutputTextBox.Size = new Size(270, 296);
            OutputTextBox.TabIndex = 9;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 374);
            label3.Name = "label3";
            label3.Size = new Size(35, 20);
            label3.TabIndex = 10;
            label3.Text = "или";
            // 
            // UnarchiveButton
            // 
            UnarchiveButton.Location = new Point(468, 13);
            UnarchiveButton.Name = "UnarchiveButton";
            UnarchiveButton.Size = new Size(143, 29);
            UnarchiveButton.TabIndex = 11;
            UnarchiveButton.Text = "Разархивировать";
            UnarchiveButton.UseVisualStyleBackColor = true;
            UnarchiveButton.Click += UnarchiveButton_Click;
            // 
            // SaveToFileButton
            // 
            SaveToFileButton.Location = new Point(405, 400);
            SaveToFileButton.Name = "SaveToFileButton";
            SaveToFileButton.Size = new Size(146, 29);
            SaveToFileButton.TabIndex = 12;
            SaveToFileButton.Text = "Сохранить в файл";
            SaveToFileButton.UseVisualStyleBackColor = true;
            SaveToFileButton.Click += SaveToFileButton_Click;
            // 
            // ratioLabel
            // 
            ratioLabel.AutoSize = true;
            ratioLabel.Location = new Point(341, 44);
            ratioLabel.Name = "ratioLabel";
            ratioLabel.Size = new Size(42, 20);
            ratioLabel.TabIndex = 13;
            ratioLabel.Text = "label";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(ratioLabel);
            Controls.Add(SaveToFileButton);
            Controls.Add(UnarchiveButton);
            Controls.Add(label3);
            Controls.Add(OutputTextBox);
            Controls.Add(label2);
            Controls.Add(ArchiveButton);
            Controls.Add(InputTextBox);
            Controls.Add(label1);
            Controls.Add(filenameLabel);
            Controls.Add(SelectFileButton);
            Name = "MainForm";
            Text = "Архиватор алгоритмом RLE";
            Load += MainForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button SelectFileButton;
        private Label filenameLabel;
        private Label label1;
        private TextBox InputTextBox;
        private OpenFileDialog OpenFileDialog;
        private Button ArchiveButton;
        private Label label2;
        private TextBox OutputTextBox;
        private Label label3;
        private Button UnarchiveButton;
        private Button SaveToFileButton;
        private SaveFileDialog SaveFileDialog;
        private Label ratioLabel;
    }
}
