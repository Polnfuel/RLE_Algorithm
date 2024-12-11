using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

namespace RLE_Algorithm
{
    public class FileHandler
    {
        public string DocxFilePath { get; set; }
        private readonly ArchiveManager manager;
        public FileHandler(string archiveDirPath, ArchiveManager manager)
        {
            DocxFilePath = Path.Combine(archiveDirPath, "archive.docx");
            this.manager = manager;
            this.manager.TextCompressed += TextCompressed;
            this.manager.TextDecompressed += TextDecompressed;
            CreateDocx();
        }
        public void CreateDocx()
        {
            bool success = false;
            while (!success)
            {
                try
                {
                    using (WordprocessingDocument worddoc = WordprocessingDocument.Create(DocxFilePath, DocumentFormat.OpenXml.WordprocessingDocumentType.Document))
                    {
                        MainDocumentPart mainpart = worddoc.AddMainDocumentPart();
                        mainpart.Document = new Document(new Body());
                    }
                    success = true;
                }
                catch (IOException)
                {
                    MessageBox.Show($"Сперва закройте файл {Path.GetFileName(DocxFilePath)}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        public void AppendText(string text, bool alignCenter = false)
        {
            using (WordprocessingDocument worddoc = WordprocessingDocument.Open(DocxFilePath, true))
            {
                Body body = worddoc.MainDocumentPart.Document.Body;

                RunProperties runprops = new RunProperties();
                runprops.Append(new RunFonts { Ascii = "Consolas" });
                runprops.Append(new FontSize { Val = (12 * 2).ToString() });
                ParagraphProperties parprops = new ParagraphProperties();
                parprops.Append(new Justification { Val = alignCenter ? JustificationValues.Center : JustificationValues.Left });
                parprops.Append(new SpacingBetweenLines { Before = 0.ToString(), After = 0.ToString() });

                Run run = new Run(runprops);
                run.Append(new Text(text));
                Paragraph paragraph = new Paragraph(parprops);
                paragraph.Append(run);
                body.Append(paragraph);
                worddoc.MainDocumentPart.Document.Save();
            }
        }
        public static string ReadTextFromFile(string path)
        {
            string input = File.ReadAllText(path);
            if (string.IsNullOrEmpty(input))
                throw new ArgumentException();
            return input;
        }
        public static void SaveTextToFile(string path, string input)
        {
            File.WriteAllText(path, input);
        }
        private void TextCompressed(object sender, ArchiveEventArgs args)
        {
            try
            {
                AppendText("==== Исходный текст ====", true);
                AppendText(args.InputTextBox.Text);
                AppendText("========================", true);
                AppendText($"==== Архивированный текст (сжат в {Math.Round((double)args.InputTextSize / args.OutputTextSize, 2)}) раз ====", true);
                AppendText(args.OutputTextBox.Text);
                AppendText("============================================", true);
            }
            catch (IOException)
            {
                MessageBox.Show($"Не удалось добавить содержимое в файл {Path.GetFileName(DocxFilePath)}, " +
                    $"так как он открыт в другой программе. Закройте файл и попробуйте снова", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void TextDecompressed(object sender, ArchiveEventArgs args)
        {
            try
            {
                AppendText("==== Архивированный текст ====", true);
                AppendText(args.InputTextBox.Text);
                AppendText("=============================", true);
                AppendText($"==== Разархивированный текст ====", true);
                AppendText(args.OutputTextBox.Text);
                AppendText("===============================", true);
            }
            catch (IOException)
            {
                MessageBox.Show($"Не удалось добавить содержимое в файл {Path.GetFileName(DocxFilePath)}, " +
                    $"так как он открыт в другой программе. Закройте файл и попробуйте снова", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
