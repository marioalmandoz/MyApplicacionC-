
using RestSharp;
using RestSharp.Authenticators;
using System.Diagnostics;
using System.Text.Json;
using System.Threading;

namespace MyApplicacion.Database;

public class Peticiones
{

    public static async Task DownloadDate()
    {        
            try
            {
            
                Console.WriteLine("----paso1");
                var url = "http://adminetiquetas.dur.bruss-group.com/AdminDatosEtiquetas.aspx";
                var urlp = "https://jsonplaceholder.typicode.com/";
                var client = new RestClient(urlp);
                var request = new RestRequest("GET");
                var response = client.Execute(request);
                Console.WriteLine("----paso2");
                Console.WriteLine(response.StatusCode);
                Console.WriteLine(response.StatusDescription);
                Console.WriteLine(response.ResponseStatus);
                Console.WriteLine(response.Content);
            
                Console.WriteLine("----Terminado----");

               
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocurrió un error al realizar la petición: {ex.Message}");
            }
        
        
    }

}

