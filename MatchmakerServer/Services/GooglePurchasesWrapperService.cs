using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Google.Apis.AndroidPublisher.v3;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Util.Store;

namespace AmoebaGameMatcherServer.Services
{
    public class GooglePurchasesWrapperService
    {
        private readonly AndroidPublisherService service;
        
        public GooglePurchasesWrapperService()
        {
            try
            {
                var userCredential = GetUserCredential().Result;
                service = new AndroidPublisherService(new BaseClientService.Initializer
                {
                    HttpClientInitializer = userCredential,
                    ApplicationName = "Nebula Wars Battle Royale"
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        
        public void Validate(string productId, string token)
        {
            var request = service.Purchases.Products.Get(GoogleApiGlobals.PackageName, productId, token);
            var result = request.Execute();

            Console.WriteLine($"{nameof(result.OrderId)} {result.OrderId}");
            Console.WriteLine($"{nameof(result.DeveloperPayload)} {result.DeveloperPayload}");
            Console.WriteLine($"{nameof(result.Kind)} {result.Kind}");
            if (result.Quantity != null)
            {
                Console.WriteLine($"{nameof(result.Kind)} {result.Quantity.Value}");
            }
            Console.WriteLine($"{nameof(result.ETag)} {result.ETag}");
            
            if (result.AcknowledgementState != null)
            {
                Console.WriteLine($"{nameof(result.AcknowledgementState)} {result.AcknowledgementState.Value}");
            }

            var purchaseState = result.PurchaseState;
            if (purchaseState.HasValue)
            {
                switch (purchaseState.Value)
                {
                    case 0:
                        Console.WriteLine("Purchased");
                        break;
                    case 1:
                        Console.WriteLine("Canceled");
                        break;
                    case 2:
                        Console.WriteLine("Pending");
                        break;
                }
            }
        }
        
        private async Task<UserCredential> GetUserCredential()
        {
            UserCredential credential;
            try
            {
                string[] scopes = {"https://www.googleapis.com/auth/androidpublisher"};
                using (var stream = new FileStream("client_secrets.json", FileMode.Open, FileAccess.Read))
                {
                    credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                        GoogleClientSecrets.Load(stream).Secrets,
                        scopes,
                        "user", 
                        CancellationToken.None,
                        new FileDataStore("GoogleSukaRabotay"));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
            return credential;
        }
    }
}