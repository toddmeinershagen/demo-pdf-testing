using FluentAssertions;
using James.Testing.Pdf;
using NUnit.Framework;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Demo.PdfTesting
{
    [TestFixture]
    public class ContentBuilderTests
    {
        [Test]
        public async Task given_pdf_when_extracting_then_return_content()
        {
            var key = Environment.GetEnvironmentVariable("COMPUTER_VISION_KEY", EnvironmentVariableTarget.Machine);
            var endpoint = Environment.GetEnvironmentVariable("COMPUTER_VISION_ENDPOINT", EnvironmentVariableTarget.Machine);
            var path = Path.Combine(Environment.CurrentDirectory, "eap-overview.pdf");

            var content = await ContentBuilder
                .UsingEndpoint(endpoint, key)
                .ExtractFromAsync(path);

            content
                .NumberOfPages.Should().Be(1);
            content
                .GetPage(1).Lines.Should().HaveCount(70);
        }
    }
}
