// using System;
// using System.Security.Cryptography.X509Certificates;
// using Google;
// using Google.Apis.AndroidPublisher.v2;
// using Google.Apis.AndroidPublisher.v2.Data;
// using Google.Apis.Auth.OAuth2;
// using Google.Apis.Services;
// using Microsoft.AspNetCore.Mvc;
// using Newtonsoft.Json;
//
// namespace AmoebaGameMatcherServer.Controllers
// {
//     [Route("[controller]")]
//     [ApiController]
//     public class GooglePurchasesValidationController: ControllerBase
//     {
//         private const string PackageName = "com.tikaytech.tikay";
//         
//         [Route("GetValidationResult")]
//         [HttpPost]
//         public ActionResult<string> GetValidationResult([FromForm] string inAppItemId,[FromForm] string purchaseToken)
//         {
//             Console.WriteLine("Пришёл запрос");
//             Console.WriteLine($"{nameof(inAppItemId)} {inAppItemId}");
//             Console.WriteLine($"{nameof(purchaseToken)} {purchaseToken}");
//             
//             string serviceAccountEmail = "testserviceaccount@tikay-16841251.iam.gserviceaccount.com";
//             var certificate = new X509Certificate2(@"someDich.p12", "notasecret", X509KeyStorageFlags.Exportable);
//
//             ServiceAccountCredential credential = new ServiceAccountCredential(
//                 new ServiceAccountCredential.Initializer(serviceAccountEmail)
//                 {
//                     Scopes = new[] { "https://www.googleapis.com/auth/androidpublisher" }
//                 }.FromCertificate(certificate));
//
//
//             var service = new AndroidPublisherService(
//                 new BaseClientService.Initializer
//                 {
//                     HttpClientInitializer = credential,
//                     ApplicationName = "Tikay"
//                 });
//
//             try
//             {
//                 // InappproductsResource.ListRequest request = service.Inappproducts.List("com.tikaytech.tikay");
//                 // InappproductsListResponse listResponse = request.Execute();
//                 // var test = service.;
//
//                 PurchasesResource.ProductsResource.GetRequest request =
//                     service.Purchases.Products.Create(PackageName, inAppItemId, purchaseToken);
//
//                 ProductPurchase result = request.Execute();
//                 Console.WriteLine(JsonConvert.SerializeObject(result));
//             }
//             catch (GoogleApiException e)
//             {
//                 Console.WriteLine($"{nameof(GoogleApiException)}");
//                 Console.WriteLine(e.Error.Message);
//                 Console.WriteLine(e.Message);
//             }
//             catch (Exception e)
//             {
//                 Console.WriteLine("Неожиданное исключение" + e.Message);
//             }
//             
//             return null;
//         }
//     }
// }