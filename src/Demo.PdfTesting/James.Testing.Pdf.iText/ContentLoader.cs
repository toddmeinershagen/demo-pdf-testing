using iText.Kernel.Pdf;
using iText.Layout.Element;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace James.Testing.Pdf.iText
{
    public class ContentLoader : IContentLoader
    {
        public async Task<IContent> ExtractFromAsync(string path)
        {
            using (var reader = new PdfReader(path))
            {
                var document = new PdfDocument(reader);
                return await Task.FromResult(new Content(document));
            }
        }
    }

    public class Content : IContent
    {
        public Content(PdfDocument document)
        {
            Pages = new List<IPage>();

            for (int pageNumber = 1; pageNumber <= document.GetNumberOfPages(); pageNumber++)
            {
                var page = document.GetPage(pageNumber);
                Pages.Add(new Page(page));
            }
        }

        public List<IPage> Pages { get; private set; }
    }

    public class Page : IPage
    {
        public Page(PdfPage page)
        {
            Lines = new List<ILine>();
            
            var contents = page.GetPdfObject();
            foreach (var key in contents.KeySet())
            {
                var line = contents.Get(key);
                Lines.Add(new Line(line));
            }
        }

        public IList<ILine> Lines { get; private set; }
    }

    public class Line : ILine
    {
        public Line(PdfObject line)
        {
            Text = line.ToString();
        }

        public Point TopLeft { get; private set; }
        public Point TopRight { get; private set; }
        public Point BottomRight { get; private set; }
        public Point BottomLeft { get; private set; }

        public string Text { get; private set; }
    }
}
