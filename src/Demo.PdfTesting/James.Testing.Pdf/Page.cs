using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using System.Collections.Generic;

namespace James.Testing.Pdf
{
    public class Page : IPage
    {
        internal Page(TextRecognitionResult result)
        {
            Lines = new List<ILine>();
            foreach (var line in result.Lines)
            {
                Lines.Add(new Line(line));
            }
        }

        public IList<ILine> Lines { get; }
    }
}
