using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

namespace RLE_Algorithm
{
    public class DocxFileHandler
    {
        public string DocxFilePath { get; set; }
        private readonly ArchiveManager manager;
        public DocxFileHandler(string archiveDirPath, ArchiveManager manager)
        {
            DocxFilePath = Path.Combine(archiveDirPath, "archive.docx");
            this.manager = manager;
            this.manager.TextCompressed += TextCompressed;
            this.manager.TextDecompressed += TextDecompressed;
            CreateDocx();
        }
        public void CreateDocx()
        {
            using (WordprocessingDocument worddoc = WordprocessingDocument.Create(DocxFilePath, DocumentFormat.OpenXml.WordprocessingDocumentType.Document))
            {
                MainDocumentPart mainpart = worddoc.AddMainDocumentPart();
                mainpart.Document = new Document(new Body());
            }
        }
        public void AppendText(string text)
        {
            using (WordprocessingDocument worddoc = WordprocessingDocument.Open(DocxFilePath, true))
            {
                Body body = worddoc.MainDocumentPart.Document.Body;

                RunProperties runprops = new RunProperties();
                runprops.Append(new RunFonts { Ascii = "Consolas" });
                runprops.Append(new FontSize { Val = (12 * 2).ToString() });
                ParagraphProperties parprops = new ParagraphProperties();
                parprops.Append(new Justification { Val = JustificationValues.Left });
                parprops.Append(new SpacingBetweenLines { Before = 0.ToString(), After = 0.ToString() });

                Run run = new Run(runprops);
                run.Append(new Text(text));
                Paragraph paragraph = new Paragraph(parprops);
                paragraph.Append(run);
                body.Append(paragraph);
                worddoc.MainDocumentPart.Document.Save();
            }
        }
        private void TextCompressed(object sender, ArchiveEventArgs args)
        {
            AppendText("============== Исходный текст ================");
            AppendText(args.InputTextBox.Text);
            AppendText("==============================================");
            AppendText($"========== Архивированный текст (сжат в {Math.Round((double)args.InputTextSize / args.OutputTextSize, 2)}) раз =========");
            AppendText(args.OutputTextBox.Text);
            AppendText("===================================");
        }
        private void TextDecompressed(object sender, ArchiveEventArgs args)
        {
            AppendText("============= Архивированный текст ===========");
            AppendText(args.InputTextBox.Text);
            AppendText("==============================================");
            AppendText($"=========== Разархивированный текст =========");
            AppendText(args.OutputTextBox.Text);
            AppendText("==============================================");
        }
    }
}
