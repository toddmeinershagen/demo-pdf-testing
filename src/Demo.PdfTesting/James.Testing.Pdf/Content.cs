using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using System;
using System.Collections.Generic;
using System.Threading;

namespace James.Testing.Pdf
{
    public class Content : IContent
    {
        private static readonly ThreadLocal<IContent> CurrentContent = new ThreadLocal<IContent>();

        internal Content(ReadOperationResult results)
        {
            Pages = new List<IPage>();
            foreach (var result in results.RecognitionResults)
            {
                Pages.Add(new Page(result));
            };
            CurrentContent.Value = this;
        }

        public List<IPage> Pages { get; private set; }

        public static IContent Current()
        {
            return CurrentContent.Value;
        }
    }
}
