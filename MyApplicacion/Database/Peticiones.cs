
/* Cambio no fusionado mediante combinación del proyecto 'MyApplicacion (net8.0-android)'
Antes:
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
Después:
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
*/
using RestSharp;
using RestSharp.Authenticators;
using System.Diagnostics;
using System.Text.Json;
using System.Threading;

namespace MyApplicacion.Database;

public class Peticiones
{

    public static async void DownloadDate()
    {
       // string url = "http://datacapturews.dur300.bruss-group.com/api/MaterialQuantities"; 
        try
        {
            //var client = new RestClient("http://datacapturews.dur300.bruss-group.com/api");

            //var request = new RestRequest("Referencias", Method.Get);
            //// The cancellation token comes from the caller. You can still make a call without it.
            //var queryResult = client.Execute(request);

            //if (queryResult.IsSuccessful)
            //{
            //    Console.WriteLine("Respuesta: ");
            //    Console.WriteLine(queryResult);
            //}
            //else
            //{
            //    Console.WriteLine("Error al realizar la solicitud: " + queryResult.ErrorMessage);
            //}
            Console.WriteLine("----paso1");
            var url = "http://datacapturews.dur300.bruss-group.com/api/MaterialQuantities";
            var client = new RestClient(url);
            var response = client.Execute(new RestRequest());
            Console.WriteLine("----paso2");
            Console.WriteLine(response.Content);
            Console.WriteLine(response.ResponseStatus);
            Console.WriteLine("----Terminado----");

        }
        catch (Exception ex)
            {
            Console.WriteLine($"Ocurrió un error al realizar la petición: {ex.Message}");
        }
    }

}

