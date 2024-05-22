using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace MyApplicacion.Database
{
    internal class Peticiones
    {

        public class MaterialQuantity
        {
            public int Id { get; set; }
            public string Material { get; set; }
            public int Quantity { get; set; }
            public DateTime FechaInserted { get; set; }
            public bool Active { get; set; }
        }

        public static void DownloadAndPrintJson<T>()
        {
            using (var w = new WebClient())
            {
                var json_data = string.Empty;
                // intenta descargar los datos JSON como una cadena
                try
                {
                    // String url = "http://datacapturews.dur300.bruss-group.com/api/MaterialQuantities";
                    json_data = w.DownloadString("http://datacapturews.dur300.bruss-group.com/api/Referencias");
                }
                catch (Exception)
                {
                    Console.WriteLine("Error al descargar los datos JSON.");
                    return;
                }

                // si la cadena con los datos JSON no está vacía, imprime el resultado JSON en la consola
                if (!string.IsNullOrEmpty(json_data))
                {
                    Console.WriteLine("Datos JSON descargados:");
                    // Obtener solo las primeras 10 instancias del JSON
                    // Dividir el JSON completo en sus objetos individuales
                    var jsonObjects = JsonConvert.DeserializeObject<JArray>(json_data);

                    // Tomar solo los primeros 10 objetos del JSON
                    var first10Objects = jsonObjects.Take(10);

                    // Convertir los primeros 10 objetos de nuevo a JSON
                    var first10Json = JsonConvert.SerializeObject(first10Objects);
                    Console.WriteLine(first10Json);
                    //Console.WriteLine(json_data);
                    // Ruta donde se guardará el archivo JSON

                    // Obtener el directorio de almacenamiento interno específico de la aplicación
                    string internalStoragePath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);

                    // Combinar el directorio de almacenamiento interno con el nombre del archivo
                    string filePath = Path.Combine(internalStoragePath, "datos.json");

                   

                    
                    try
                    {

                        // Escribir el contenido JSON en el archivo
                        File.WriteAllText(filePath, first10Json);
                        Console.WriteLine($"Datos JSON guardados en el archivo: {filePath}");
                        // Leer y mostrar el contenido del archivo JSON
                        string jsonFromFile = File.ReadAllText(filePath);
                        Console.WriteLine("Contenido del archivo JSON:");
                        Console.WriteLine(jsonFromFile);
                    }
                    catch (Exception e) {
                        Console.WriteLine(e.Message);
                        Console.WriteLine($"Error al escribir datos en el archivo: {filePath}");
                    }
                }
                else
                {
                    Console.WriteLine("Los datos JSON están vacíos.");
                }
            }
        }

        public static bool ComprobarDatos(string pReferencia, int pCant)
        {
            using (var w = new WebClient())
            {
                var json_data = string.Empty;
                bool isQuantity = false;
                // intenta descargar los datos JSON como una cadena
                try
                {
                     string referencia = pReferencia;
                    json_data = w.DownloadString($"http://datacapturews.dur300.bruss-group.com/api/MaterialQuantities?MaterialRef={pReferencia}");
                }
                catch (Exception)
                {
                    Console.WriteLine("Error al descargar los datos JSON.");
                    return false;
                }

                // si la cadena con los datos JSON no está vacía, imprime el resultado JSON en la consola
                if (!string.IsNullOrEmpty(json_data))
                {
                    Console.WriteLine("Datos JSON descargados:");
                    Console.WriteLine(json_data);
                    Console.WriteLine("Listo.-------------------------");
                    try
                    {
                        var jsonObject = JObject.Parse(json_data);
                        var materialQuantities = jsonObject["Queryable"].ToObject<List<MaterialQuantity>>();
                        foreach(var material in materialQuantities)
                        {
                            if(material.Material == pReferencia)
                            {
                                Console.WriteLine($"Material: {material.Material}, Quantity: {material.Quantity}");
                                // Comprobamos si la cantidad es igual a pCant
                                if(material.Quantity == pCant)
                                {
                                    Console.WriteLine(material.Quantity.ToString());    
                                    isQuantity = true;
                                    break;
                                }
                            }
                        }
                    }
                    catch (JsonException ex)
                    {
                        Console.WriteLine($"Error al parsear los datos JSON{ex.Message}");
                        return false;
                    }
                }
                else
                {
                    Console.WriteLine("Los datos JSON están vacíos.");
                    return false;
                }
                return isQuantity;
            }
        }
    }
}
