
using RestSharp;
using RestSharp.Authenticators;
using System.Diagnostics;
using System.Text;
using System.Threading;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace MyApplicacion.Database;

public class Peticiones
{

    public static async Task DownloadDate(Label DataDownload)
    {
       
       using( HttpClient client = new HttpClient())
       {
            
            try
            {
                String url = "https://jsonplaceholder.typicode.com/posts/1";
                String url2 = "http://datacapturews.dur300.bruss-group.com/api/Referencias";
                String url3 = "http://datacapturews.dur300.bruss-group.com/api/MaterialQuantities";
                Console.WriteLine("------------Se ha iniciado la peticion-------------");
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip,deflate");
                client.DefaultRequestHeaders.Add("Connection", "keep-alive");

                var response = client.GetAsync(url).Result;
                DataDownload.Text = response.StatusCode.ToString();
                Console.WriteLine(response.StatusCode);
                var res = response.Content.ReadAsStringAsync().Result;
                Console.WriteLine(res);
                Console.WriteLine("------------Se ha acabado la peticion-------------");

                // Verificar si la solicitud fue exitosa
                if (response.IsSuccessStatusCode)
                {
                    // Leer el contenido de la respuesta
                    string responseBody = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("Respuesta del servicio:");
                    Console.WriteLine(responseBody);
                }
                else
                {
                    Console.WriteLine($"La solicitud falló con el código de estado: {response.StatusCode}");
                }



            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocurrió un error al realizar la petición: {ex.Message}");
            }
        
        }


    }

}

