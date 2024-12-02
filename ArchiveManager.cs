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
        public ArchiveEventArgs archiveArgs { get; set; }
        private TextBox InputTextBox { get; set; }
        private TextBox OutputTextBox { get; set; }
        public string InputFilePath { get; set; }
        public string LogFilePath { get; set; }

        public event EventHandler<ArchiveEventArgs> TextCompressed;
        public event EventHandler<ArchiveEventArgs> TextDecompressed;
        public event EventHandler<ArchiveEventArgs> FileSaved;
        public event EventHandler<ArchiveEventArgs> ErrorOccured;

        public ArchiveManager(ICompressor compressor, string archivedirectory, TextBox inputbox, TextBox outputbox) 
        {
            this.compressor = compressor;
            logger = new Logger(this);
            LogFilePath = Path.Combine(archivedirectory, "archive.log");
            InputTextBox = inputbox;
            OutputTextBox = outputbox;
            archiveArgs = new ArchiveEventArgs() { 
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
            archiveArgs.FileName = path;
            OnFileSaved(archiveArgs);
        }
        public void Compress(Label ratiolabel)
        {
            string alltext = InputTextBox.Text;
            if (string.IsNullOrEmpty(alltext))
            {
                archiveArgs.ErrorComment = "Поле ввода пусто";
                OnErrorOccured(archiveArgs);
            }
            else
            {
                string outputtext = compressor.CompressText(alltext);
                OutputTextBox.Text = outputtext;
                archiveArgs.InputTextSize = alltext.Length;
                archiveArgs.OutputTextSize = outputtext.Length;
                archiveArgs.RatioLabel = ratiolabel;
                OnTextCompressed(archiveArgs);
            }
        }
        public void Decompress()
        {
            string alltext = InputTextBox.Text;
            if (string.IsNullOrEmpty(alltext))
            {
                archiveArgs.ErrorComment = "Поле ввода пусто";
                OnErrorOccured(archiveArgs);
            }
            else
            {
                string outputtext = compressor.DecompressText(alltext);
                OutputTextBox.Text = outputtext;
                archiveArgs.InputTextSize = alltext.Length;
                archiveArgs.OutputTextSize = outputtext.Length;
                OnTextDecompressed(archiveArgs);
            }
        }
        protected virtual void OnTextCompressed(ArchiveEventArgs args)
        {
            double ratio = (double)args.InputTextSize / args.OutputTextSize;
            args.RatioLabel.Text = $"Текст сжат в ({args.InputTextSize}/{args.OutputTextSize}) = {Math.Round(ratio, 2)} раз";
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
