using FluentAssertions;
using James.Testing.Pdf.AzCognitiveService;
using NUnit.Framework;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Demo.PdfTesting
{
    [TestFixture]
    public class ContentLoaderTests
    {
        [Test]
        public async Task given_pdf_when_extracting_then_return_content()
        {
            var key = Environment.GetEnvironmentVariable("COMPUTER_VISION_KEY", EnvironmentVariableTarget.Machine);
            var endpoint = Environment.GetEnvironmentVariable("COMPUTER_VISION_ENDPOINT", EnvironmentVariableTarget.Machine);
            var path = Path.Combine(Environment.CurrentDirectory, "eap-overview.pdf");

            var content = await ContentLoader
                .UsingEndpoint(endpoint, key)
                .ExtractFromAsync(path);

            //var content2 = await new James.Testing.Pdf.iText.ContentLoader().ExtractFromAsync(path);

            content
                .Pages.Count.Should().Be(1);
            content
                .Pages[0].Lines.Should().HaveCount(70);

            var counter = 0;
            foreach (var page in content.Pages)
            {
                await Console.Out.WriteLineAsync($"Page:  {++counter}");
                foreach (var line in page.Lines)
                {
                    counter++;
                    await Console.Out.WriteLineAsync($"Line:  {counter} - {line.Text}");
                }
            }
        }
    }
}
