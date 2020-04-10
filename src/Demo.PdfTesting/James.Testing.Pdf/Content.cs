using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using System.Threading;

namespace James.Testing.Pdf
{
    public class Content : IContent
    {
        private static readonly ThreadLocal<IContent> CurrentContent = new ThreadLocal<IContent>();
        private ReadOperationResult Results { get; set; }

        internal Content(ReadOperationResult results)
        {
            Results = results;
            CurrentContent.Value = this;
        }

        public int NumberOfPages => Results.RecognitionResults.Count;
        public IPage GetPage(int pageNumber)
        {
            return new Page(Results.RecognitionResults[pageNumber - 1].Lines);
        }

        public static IContent Current()
        {
            return CurrentContent.Value;
        }
    }
}
