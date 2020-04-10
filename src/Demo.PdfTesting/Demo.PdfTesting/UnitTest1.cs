using System;
using NUnit.Framework;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;

namespace Demo.PdfTesting
{
    public class Tests
    {
        /// <summary>
        /// Requires the set up of a Computer Vision Services resource.  Name it demo-pdf-testing-comvsn.
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task Test1()
        {
            var key = Environment.GetEnvironmentVariable("COMPUTER_VISION_KEY", EnvironmentVariableTarget.Machine);
            var endpoint = Environment.GetEnvironmentVariable("COMPUTER_VISION_ENDPOINT", EnvironmentVariableTarget.Machine);
            var client = Authenticate(endpoint, key);
            BatchReadFileInStreamHeaders headers;

            var path = Path.Combine(Environment.CurrentDirectory, "eap-overview.pdf");
            using (var stream = new FileStream(path, FileMode.Open))
            { 
                headers = await client.BatchReadFileInStreamAsync(stream);
            }

            string location = headers.OperationLocation;
            await Console.Out.WriteLineAsync(location);

            const int numberOfCharsInOperationId = 36;
            string operationId = location.Substring(location.Length - numberOfCharsInOperationId);

            int i = 0;
            int maxRetries = 10;
            ReadOperationResult results;

            do
            {
                results = await client.GetReadOperationResultAsync(operationId);
                await Task.Delay(TimeSpan.FromSeconds(1));
            }
            while ((
                results.Status == TextOperationStatusCodes.Running ||
                results.Status == TextOperationStatusCodes.NotStarted) && i++ < maxRetries
            );

            var counter = 0;
            foreach (var result in results.RecognitionResults)
            {
                await Console.Out.WriteLineAsync($"Page:  {result.Page}");
                foreach (var line in result.Lines)
                {
                    counter++;
                    await Console.Out.WriteLineAsync($"Line:  {counter} - {line.Text}");
                }
            }
        }

        public static ComputerVisionClient Authenticate(string endpoint, string key)
        {
            ComputerVisionClient client = new ComputerVisionClient(new ApiKeyServiceClientCredentials(key)){ Endpoint = endpoint };
            return client;
        }
    }
}