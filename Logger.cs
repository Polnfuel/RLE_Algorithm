using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RLE_Algorithm
{
    public class Logger
    {
        private ArchiveManager manager;
        public Logger(ArchiveManager manager)
        {
            this.manager = manager;
            this.manager.TextCompressed += TextCompressed;
            this.manager.TextDecompressed += TextDecompressed;
            this.manager.ErrorOccured += ErrorOccured;
        }
        private void TextCompressed(object sender, ArchiveEventArgs args)
        {
            string message = $"{args.Time}: исходный текст длиной {args.InputTextSize} симв. сжат в {Math.Round((double)args.InputTextSize / args.OutputTextSize, 2)} раз до размера {args.OutputTextSize} симв. \n";
            File.AppendAllText(args.LogFilePath, message);
        }
        private void TextDecompressed(object sender, ArchiveEventArgs args)
        {
            string message = $"{args.Time}: сжатый текст длиной {args.InputTextSize} симв. был восстановлен до исходного длиной {args.OutputTextSize} симв. \n";
            File.AppendAllText(args.LogFilePath, message);
        }
        private void FileSaved(object sender, ArchiveEventArgs args)
        {

        }
        private void ErrorOccured(object sender, ArchiveEventArgs args)
        {
            string message = $"{args.Time}: произошла ошибка \"{args.ErrorComment}\"\n";
            File.AppendAllText(args.LogFilePath, message);
        }
    }
}
