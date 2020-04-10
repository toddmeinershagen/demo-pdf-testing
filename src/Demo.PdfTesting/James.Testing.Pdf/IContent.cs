using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using System.Collections.Generic;

namespace James.Testing.Pdf
{
    public interface IContent
    {
        int NumberOfPages { get; }
        IPage GetPage(int pageNumber);
    }
}
