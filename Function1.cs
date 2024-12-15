using Azure.Core;
using Azure.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace TestJwtTokenApp
{
    public class Function1
    {
        private readonly ILogger<Function1> _logger;

        public Function1(ILogger<Function1> logger)
        {
            _logger = logger;
        }

        [Function("Function1")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            try
            {
                var objDefaultAzureCredentialOptions = new DefaultAzureCredentialOptions
                {
                    ExcludeEnvironmentCredential = false,
                    ExcludeManagedIdentityCredential = false,
                    ExcludeSharedTokenCacheCredential = true,
                    ExcludeVisualStudioCredential = false,
                    ExcludeVisualStudioCodeCredential = false,
                    ExcludeAzureCliCredential = true,
                    ExcludeInteractiveBrowserCredential = true
                };

                var tokenCredential = new DefaultAzureCredential(objDefaultAzureCredentialOptions);

               AccessToken accessToken = await tokenCredential.GetTokenAsync(
                        new TokenRequestContext(scopes: new[] { "https://management.azure.com" }));
            }
            catch (AuthenticationFailedException ex)
            {

            }
            catch (ArgumentException ex)
            {

            }
            catch (InvalidOperationException ex)
            {

            }
            catch (Exception ex)
            {

            }

            return new OkObjectResult("Welcome to Azure Functions!");
        }
    }
}
