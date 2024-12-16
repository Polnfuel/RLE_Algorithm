using DocumentFormat.OpenXml.ExtendedProperties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace RLE_Algorithm
{
    public class ArchiveManager
    {
        private readonly ICompressor compressor;
        private readonly Logger logger;
        private readonly FileHandler docx;
        private ArchiveEventArgs ArchiveArgs { get; set; }
        private TextBox InputTextBox { get; set; }
        private TextBox OutputTextBox { get; set; }
        private Label RatioLabel { get; set; }
        public string InputFilePath { get; set; }
        private string LogFilePath { get; set; }

        public event EventHandler<ArchiveEventArgs> TextCompressed;
        public event EventHandler<ArchiveEventArgs> TextDecompressed;
        public event EventHandler<ArchiveEventArgs> FileSaved;
        public event EventHandler<ArchiveEventArgs> ErrorOccured;

        public ArchiveManager(ICompressor compressor, string archivedirectory, TextBox inputbox, TextBox outputbox, Label ratiolabel) 
        {
            this.compressor = compressor;
            logger = new Logger(this);
            docx = new FileHandler(archivedirectory, this);
            LogFilePath = Path.Combine(archivedirectory, "archive.log");
            InputTextBox = inputbox;
            OutputTextBox = outputbox;
            RatioLabel = ratiolabel;
            ArchiveArgs = new ArchiveEventArgs() { 
                LogFilePath = LogFilePath, 
                InputTextBox = InputTextBox, 
                OutputTextBox = OutputTextBox
            };
        }
        public string ReadFromFile(string path)
        {
            try
            {
                return FileHandler.ReadTextFromFile(path);
            }
            catch (ArgumentException)
            {
                MessageBox.Show("Выбранный файл пуст!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (Exception)
            {
                ArchiveArgs.ErrorComment = "Не удалось открыть файл";
                OnErrorOccured(ArchiveArgs);
            }
            return string.Empty;
        }
        public void SaveToFile()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text files (*.TXT)|*.txt";
            try
            {
                if (OutputTextBox.Text == string.Empty)
                    throw new ArgumentException();
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    FileHandler.SaveTextToFile(saveFileDialog.FileName, OutputTextBox.Text);
                    ArchiveArgs.FileName = saveFileDialog.FileName;
                    OnFileSaved(ArchiveArgs);
                }
            }
            catch (ArgumentException)
            {
                if (MessageBox.Show("Текстовое поле пусто! Все равно сохранить?", "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        FileHandler.SaveTextToFile(saveFileDialog.FileName, OutputTextBox.Text);
                        ArchiveArgs.FileName = saveFileDialog.FileName;
                        OnFileSaved(ArchiveArgs);
                    }
                }
            }
            catch (Exception)
            {
                ArchiveArgs.ErrorComment = "Не удалось сохранить в файл";
                OnErrorOccured(ArchiveArgs);
            }
        }
        public void Compress()
        {
            string alltext = InputTextBox.Text;
            try
            {
                string outputtext = compressor.CompressText(alltext);
                OutputTextBox.Text = outputtext;
                ArchiveArgs.InputTextSize = alltext.Length;
                ArchiveArgs.OutputTextSize = outputtext.Length;
                OnTextCompressed(ArchiveArgs);
            }
            catch (FormatException)
            {
                ArchiveArgs.ErrorComment = "Введенный текст не пригоден к архивации из-за наличия чисел";
                OnErrorOccured(ArchiveArgs);
            }
            catch (ArgumentException)
            {
                ArchiveArgs.ErrorComment = "Сперва введите текст!";
                OnErrorOccured(ArchiveArgs);
            }  
        }
        public void Decompress()
        {
            string alltext = InputTextBox.Text;
            try
            {
                string outputtext = compressor.DecompressText(alltext);
                OutputTextBox.Text = outputtext;
                ArchiveArgs.InputTextSize = alltext.Length;
                ArchiveArgs.OutputTextSize = outputtext.Length;
                OnTextDecompressed(ArchiveArgs);
            }
            catch (FormatException)
            {
                ArchiveArgs.ErrorComment = "Введенный текст не в сжатом формате";
                OnErrorOccured(ArchiveArgs);
            }
            catch (ArgumentException)
            {
                ArchiveArgs.ErrorComment = "Сперва введите текст!";
                OnErrorOccured(ArchiveArgs);
            }
        }
        protected virtual void OnTextCompressed(ArchiveEventArgs args)
        {
            double ratio = (double)args.InputTextSize / args.OutputTextSize;
            RatioLabel.Text = $"Текст сжат в ({args.InputTextSize}/{args.OutputTextSize}) = {Math.Round(ratio, 2)} раз";
            TextCompressed?.Invoke(this, args);
        }
        protected virtual void OnTextDecompressed(ArchiveEventArgs args)
        {
            RatioLabel.Text = string.Empty;
            TextDecompressed?.Invoke(this, args);
        }
        protected virtual void OnFileSaved(ArchiveEventArgs args)
        {
            FileSaved?.Invoke(this, args);
        }
        protected virtual void OnErrorOccured(ArchiveEventArgs args)
        {
            MessageBox.Show(args.ErrorComment, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            ErrorOccured?.Invoke(this, args);
        }
    }
}
