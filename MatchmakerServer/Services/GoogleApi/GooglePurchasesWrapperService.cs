// using System;
// using System.IO;
// using System.Threading;
// using System.Threading.Tasks;
// using Google.Apis.AndroidPublisher.v3;
// using Google.Apis.Auth.OAuth2;
// using Google.Apis.Auth.OAuth2.Flows;
// using Google.Apis.Auth.OAuth2.Responses;
// using Google.Apis.Services;
// using Google.Apis.Util.Store;
//
// namespace AmoebaGameMatcherServer.Services
// {
//     public class GooglePurchasesWrapperService
//     {
//         private AndroidPublisherService service;
//         private UserCredential userCredential;
//         
//         public GooglePurchasesWrapperService()
//         {
//             try
//             {
//                 service = CreateService();
//                 if (service == null)
//                 {
//                     Console.WriteLine($"{nameof(service)} was null in ctor");
//                 }
//             }
//             catch (Exception e)
//             {
//                 Console.WriteLine(e);
//             }
//         }
//
//         private AndroidPublisherService CreateService()
//         {
//             if (userCredential == null)
//             {
//                 Console.WriteLine($"{nameof(userCredential)} was null");
//                 userCredential = GetUserCredential().Result;
//             }
//             
//             var serviceTmp = new AndroidPublisherService(new BaseClientService.Initializer
//             {
//                 HttpClientInitializer = userCredential,
//                 ApplicationName = "Nebula Wars Battle Royale"
//             });
//             return serviceTmp;
//         }
//         
//         public void Validate(string productId, string token)
//         {
//             if (service == null)
//             {
//                 service = CreateService();
//             }
//             
//             if (service == null)
//             {
//                 throw new Exception($"{nameof(service)} was null");
//             }
//             if (productId == null)
//             {
//                 throw new Exception($"{nameof(productId)} was null");
//             }
//             if (token == null)
//             {
//                 throw new Exception($"{nameof(token)} was null");
//             }
//             
//             var request = service.Purchases.Products.BuyImprovement(GoogleApiGlobals.PackageName, productId, token);
//             var result = request.Execute();
//
//             Console.WriteLine($"{nameof(result.OrderId)} {result.OrderId}");
//             Console.WriteLine($"{nameof(result.DeveloperPayload)} {result.DeveloperPayload}");
//             Console.WriteLine($"{nameof(result.Kind)} {result.Kind}");
//             if (result.Quantity != null)
//             {
//                 Console.WriteLine($"{nameof(result.Kind)} {result.Quantity.Value}");
//             }
//             Console.WriteLine($"{nameof(result.ETag)} {result.ETag}");
//             
//             if (result.AcknowledgementState != null)
//             {
//                 Console.WriteLine($"{nameof(result.AcknowledgementState)} {result.AcknowledgementState.Value}");
//             }
//
//             var purchaseState = result.PurchaseState;
//             if (purchaseState.HasValue)
//             {
//                 switch (purchaseState.Value)
//                 {
//                     case 0:
//                         Console.WriteLine("Purchased");
//                         break;
//                     case 1:
//                         Console.WriteLine("Canceled");
//                         break;
//                     case 2:
//                         Console.WriteLine("Pending");
//                         break;
//                 }
//             }
//         }
//         
//         private async Task<UserCredential> GetUserCredential()
//         {
//             UserCredential credential;
//             try
//             {
//                 string[] scopes = {"https://www.googleapis.com/auth/androidpublisher"};
//                 using (var stream = new FileStream("client_secrets.json", FileMode.Open, FileAccess.Read))
//                 {
//                     credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
//                         GoogleClientSecrets.Load(stream).Secrets,
//                         scopes,
//                         "Tikaytech", 
//                         CancellationToken.None,
//                         new FileDataStore("GoogleSukaRabotay"));
//                 }
//             }
//             catch (Exception e)
//             {
//                 Console.WriteLine(e.Message);
//                 throw;
//             }
//             return credential;
//         }
//     }
// }