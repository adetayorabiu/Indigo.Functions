using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using System.IO;
using System.Threading.Tasks;

namespace Indigo.Functions.Redis.IntegrationTests.Target
{
    public static class Function
    {
        [FunctionName("GetString")]
        public static IActionResult GetString(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "{path}")] HttpRequest request,
            [Redis(Key = "{path}")] string cachedValue,
            TraceWriter log)
        {
            return new OkObjectResult(cachedValue);
        }

        [FunctionName("SetString")]
        public static async Task<IActionResult> SetString(
            [HttpTrigger(AuthorizationLevel.Function, "POST", Route = "{path}")] HttpRequest request,
            [Redis(Key = "{path}")] IAsyncCollector<string> collector,
            TraceWriter log)
        {
            string value;
            using (var reader = new StreamReader(request.Body))
            {
                value = reader.ReadToEnd();
                await collector.AddAsync(value);
            }
            return new OkObjectResult(value);
        }
    }
}
