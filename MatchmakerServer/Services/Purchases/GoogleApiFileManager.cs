using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using DataLayer;
using Newtonsoft.Json;

namespace AmoebaGameMatcherServer.Services
{
    public static class GoogleApiFileManager
    {
        public static async Task WriteGoogleApiDataToFile(MyGoogleApiData data)
        {
            string text = JsonConvert.SerializeObject(data);
            using (StreamWriter sw = new StreamWriter(GoogleApiGlobals.FileName, false, Encoding.UTF8))
            {
                await sw.WriteLineAsync(text);
            }
        }
        
        public static async Task<MyGoogleApiData> GetApiDataFromFile()
        {
            try
            {
                using (StreamReader sr = new StreamReader(GoogleApiGlobals.FileName, Encoding.UTF8))
                {
                    string fileContent = await sr.ReadToEndAsync();
                    if (string.IsNullOrEmpty(fileContent))
                    {
                        return null;
                    }

                    try
                    {
                        var result = JsonConvert.DeserializeObject<MyGoogleApiData>(fileContent);
                     
                        return result;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(
                            "Брошено исключение при демериализации файла с данными для доступа к google api. " +
                            e.Message);
                        return null;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        
        public static void RemoveFile()
        {
            if (File.Exists(GoogleApiGlobals.FileName))
            {
                File.Delete(GoogleApiGlobals.FileName);
            }
        }

        public static string GetCurrentDirectory()
        {
            return Directory.GetCurrentDirectory();
        }
    }
}