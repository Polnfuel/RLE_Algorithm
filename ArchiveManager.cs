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
        private readonly DocxFileHandler docx;
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

        public ArchiveManager(ICompressor compressor, string archivedirectory, TextBox inputbox, TextBox outputbox) 
        {
            this.compressor = compressor;
            logger = new Logger(this);
            docx = new DocxFileHandler(archivedirectory, this);
            LogFilePath = Path.Combine(archivedirectory, "archive.log");
            InputTextBox = inputbox;
            OutputTextBox = outputbox;
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
                string input = File.ReadAllText(path);
                return input;
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }
        public void SaveToFile(string path, string input)
        {
            try
            {
                File.WriteAllText(path, input);
            }
            catch (Exception)
            {
                throw new Exception();
            }
            ArchiveArgs.FileName = path;
            OnFileSaved(ArchiveArgs);
        }
        public void Compress(Label ratiolabel)
        {
            RatioLabel = ratiolabel;
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
                ArchiveArgs.ErrorComment = "Поле ввода пусто!";
                OnErrorOccured(ArchiveArgs);
            }
                
        }
        public void Decompress()
        {
            string alltext = InputTextBox.Text;
            if (string.IsNullOrEmpty(alltext))
            {
                ArchiveArgs.ErrorComment = "Поле ввода пусто";
                OnErrorOccured(ArchiveArgs);
            }
            else
            {
                string outputtext = compressor.DecompressText(alltext);
                OutputTextBox.Text = outputtext;
                ArchiveArgs.InputTextSize = alltext.Length;
                ArchiveArgs.OutputTextSize = outputtext.Length;
                OnTextDecompressed(ArchiveArgs);
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
            TextDecompressed?.Invoke(this, args);
        }
        protected virtual void OnFileSaved(ArchiveEventArgs args)
        {
            FileSaved?.Invoke(this, args);
        }
        protected virtual void OnErrorOccured(ArchiveEventArgs args)
        {
            MessageBox.Show(args.ErrorComment, "Ошибка", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            ErrorOccured?.Invoke(this, args);
        }
    }
}
