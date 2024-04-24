
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

    public static async Task DownloadDate()
    {
       //var url = "http://datacapturews.dur300.bruss-group.com/api/MaterialQuantities?MaterialRef=44772";
       using( HttpClient client = new HttpClient())
       {
            String url = "https://jsonplaceholder.typicode.com/posts/1";
            String url2 = "http://datacapturews.dur300.bruss-group.com/api/MaterialQuantities?MaterialRef=44772";
            client.DefaultRequestHeaders.Clear();
            var response = client.GetAsync(url2).Result;

            var res = response.Content.ReadAsStringAsync().Result;
            dynamic r= JObject.Parse(res);
            Console.WriteLine(response.StatusCode);
            Console.WriteLine(r);
           /* try
            {
                /*
                    Console.WriteLine("----paso1");
                    var url = "http://datacapturews.dur300.bruss-group.com/api/MaterialQuantities?MaterialRef=44772";
                    var client = new RestClient(url);
                    var request = new RestRequest("GET");
                    var response = client.Execute(request);
                    Console.WriteLine("----paso2");
                    Console.WriteLine(response.StatusCode);
                    Console.WriteLine(response.StatusDescription);
                    Console.WriteLine(response.ResponseStatus);
                    Console.WriteLine(response.Content);

                    Console.WriteLine("----Terminado----");
                // Realizar la solicitud GET al servicio web

                HttpResponseMessage response = await client.GetAsync(url);

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
            }*/
        
        }


    }

}

