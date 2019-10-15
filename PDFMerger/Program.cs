using PdfSharp;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace PDFMerger
{
    class Program
    {
        static void Main(string[] args)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            DirectoryInfo dir = Directory.CreateDirectory("files");
            DirectoryInfo outdir = Directory.CreateDirectory("output");
            FileInfo[] files = dir.GetFiles().OrderBy(i => i.Name).ToArray();
            List<PdfDocument> documents = new List<PdfDocument>();

            foreach (var file in files)
            {
                if(file.Extension == ".pdf")
                 documents.Add(PdfReader.Open(file.FullName, PdfDocumentOpenMode.Import));
            }

            using (PdfDocument outPdf = new PdfDocument())
            {
                foreach (var document in documents)
                {
                    CopyPages(document, outPdf);
                }

                outPdf.Save(outdir.FullName + "/output.pdf");
            }

            void CopyPages(PdfDocument from, PdfDocument to)
            {
                for (int i = 0; i < from.PageCount; i++)
                {
                    to.AddPage(from.Pages[i]);
                }
            }
        }
    }
}
