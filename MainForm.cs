using System.Windows.Forms;
using System.Drawing;

namespace RLE_Algorithm
{
    public partial class MainForm : Form
    {
        ArchiveManager manager;
        public string ArchiveDirectory { get; set; }
        public MainForm()
        {
            InitializeComponent();
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            filenameLabel.Text = string.Empty;
            ratioLabel.Text = string.Empty;

            ArchiveDirectory = Directory.GetCurrentDirectory();
            for (int i = 0; i <= 2; i++)
                ArchiveDirectory = Directory.GetParent(ArchiveDirectory).FullName;
            ArchiveDirectory = Path.Combine(ArchiveDirectory, "Archive");
            Directory.CreateDirectory(ArchiveDirectory);

            manager = new ArchiveManager(new RLECompressor(), ArchiveDirectory, InputTextBox, OutputTextBox, ratioLabel);
        }
        private void SelectFileButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog.Filter = "Text files (*.TXT)|*.txt";
            if (OpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                manager.InputFilePath = OpenFileDialog.FileName;
                filenameLabel.Text = $"Τΰιλ: {Path.GetFileName(manager.InputFilePath)}";
                InputTextBox.Text = manager.ReadFromFile(manager.InputFilePath);
            }
        }
        private void ArchiveButton_Click(object sender, EventArgs e)
        {
            manager.Compress();
        }
        private void UnarchiveButton_Click(object sender, EventArgs e)
        {
            manager.Decompress();
        }
        private void SaveToFileButton_Click(object sender, EventArgs e)
        {
            manager.SaveToFile();
        }
    }
}
