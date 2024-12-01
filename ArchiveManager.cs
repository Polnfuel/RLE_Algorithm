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
        public string InputFilePath { get; set; }
        public string LogFilePath { get; set; }

        public event EventHandler<ArchiveEventArgs> TextCompressed;
        public event EventHandler<ArchiveEventArgs> ErrorOccured;

        public ArchiveManager(ICompressor compressor, string archivedirectory) 
        {
            this.compressor = compressor;
            logger = new Logger(this);
            LogFilePath = Path.Combine(archivedirectory, "archive.log");
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
        }
        public void Compress(TextBox inputbox, TextBox outputbox, Label ratiolabel)
        {
            string alltext = inputbox.Text;
            if (string.IsNullOrEmpty(alltext))
            {
                OnErrorOccured(new ArchiveEventArgs() { LogFilePath = LogFilePath, ErrorComment = "Поле ввода пусто"});
            }
            else
            {
                string outputtext = compressor.CompressText(alltext);
                outputbox.Text = outputtext;
                OnTextCompressed(new ArchiveEventArgs() { InputTextSize = alltext.Length, 
                    OutputTextSize = outputtext.Length, InputTextBox = inputbox, 
                    OutputTextBox = outputbox, RatioLabel = ratiolabel, LogFilePath = LogFilePath });
            }
        }
        public void Decompress()
        {

        }
        protected virtual void OnTextCompressed(ArchiveEventArgs args)
        {
            double ratio = (double)args.InputTextSize / args.OutputTextSize;
            args.RatioLabel.Text = $"Текст сжат в ({args.InputTextSize}/{args.OutputTextSize}) = {Math.Round(ratio, 2)} раз";
            TextCompressed?.Invoke(this, args);
        }
        protected virtual void OnErrorOccured(ArchiveEventArgs args)
        {
            MessageBox.Show(args.ErrorComment, "Ошибка", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            ErrorOccured?.Invoke(this, args);
        }
    }
}
