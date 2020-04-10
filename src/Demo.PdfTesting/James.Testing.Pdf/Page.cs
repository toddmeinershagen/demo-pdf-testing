using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using System.Collections.Generic;

namespace James.Testing.Pdf
{
    public class Page : IPage
    {
        internal Page(IList<Line> lines)
        {
            Lines = lines;
        }

        public IList<Line> Lines { get; }
    }
}
