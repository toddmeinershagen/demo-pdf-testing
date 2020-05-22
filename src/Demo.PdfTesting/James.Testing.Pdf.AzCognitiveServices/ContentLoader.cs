using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace James.Testing.Pdf.AzCognitiveService
{
    public class ContentLoader : IContentLoader
    {
        private readonly ThreadLocal<string> _endpoint = new ThreadLocal<string>();
        private readonly ThreadLocal<string> _key = new ThreadLocal<string>();
        
        private ContentLoader(string endpoint, string key)
        {
            _endpoint.Value = endpoint;
            _key.Value = key;
        }

        private ComputerVisionClient Authenticate(string endpoint, string key)
        {
            ComputerVisionClient client = new ComputerVisionClient(new ApiKeyServiceClientCredentials(key)) { Endpoint = endpoint };
            return client;
        }

        public static ContentLoader UsingEndpoint(string endpoint, string key)
        {
            return new ContentLoader(endpoint, key);
        }

        public async Task<IContent> ExtractFromAsync(string path)
        {
            var client = Authenticate(_endpoint.Value, _key.Value);
            BatchReadFileInStreamHeaders headers;

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

            return new Content(results);
        }
    }
}
