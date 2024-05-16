using CommunityToolkit.Mvvm.Messaging;
using MyApplicacion.Database;
using System.Text.RegularExpressions;
namespace MyApplicacion.Views;

public partial class UbicacionPage : ContentPage
{
    string referencia;
    string scaneo;
    string ubicacion;
    public UbicacionPage()
    {
        InitializeComponent();
        WeakReferenceMessenger.Default.Register<String>(this, OnDataReceived);
        
    }

    private void OnDataReceived(object recipient, string message)
    {
        if (message != null)
        {
            DataDownload.Text = "";
            Console.WriteLine(message);
            scaneo = message;
            DataDownload.Text = message;
            string pattern = @"Q(\d+).+2K([0-9]{5}).+\/\s([0-9]{5})";
            
            Match match = Regex.Match(scaneo, pattern);
            if (match.Success)
            {
                
                referencia = match.Groups[3].Value;

                Console.WriteLine("Datos: " + referencia);
                DataDownload.Text = "Datos: " + referencia;
                mostrarPallets();

            }
            else
            {
                Console.WriteLine("No se encontraron coincidencias Referencia.");
            }
            string pattern2 = @"^.+([a-zA-Z]\d)$";
            Match match1 = Regex.Match(scaneo, pattern2);
            if (match1.Success)
            {
                ubicacion = match1.Groups[1].Value;
                Console.WriteLine("Ubicacion: " + ubicacion);
                DataDownload.Text = "Ubicacion: " + ubicacion;
              
            }
        }
    }
    private async void mostrarPallets()
    {
        List<Pallet> pallet = await App.PalletRepo.MostrarAlmacenados(referencia);
        palletList.ItemsSource = pallet;
    }
    private async void Go_Back(object sender, EventArgs e)
    {
        // Acciones que quieres realizar cuando se hace clic en el botón 1
        // Por ejemplo, mostrar un mensaje en la consola
        await Shell.Current.GoToAsync("///Views.AlmacenPage");

        System.Diagnostics.Debug.WriteLine("¡Se hizo clic en el botón Back");

    }

    private async void ItemButton_Clicked(object sender, EventArgs e)
    {
        // Obtener el objeto de datos asociado a la fila seleccionada
        var item = (Pallet)((Button)sender).BindingContext;

        // Realizar alguna acción con el objeto de datos, por ejemplo:
        bool respuesta = await DisplayAlert("Confirmación", $"¿QUIERES REUBICAR EL PALLET {item.Id}?", "Sí", "No");

        // Verificar la respuesta del usuario
        if (respuesta)
        {

            if (referencia != null)
            {
                if (ubicacion == null)
                {
                    await DisplayAlert("Error", $"ESCANEE LA UBICACION", "Ok");
                }
                else
                {
                    await App.PalletRepo.AddUbicacion(item.Id, ubicacion);
                    string statusMessage = App.PalletRepo.StatusMessage;
                    Console.WriteLine(statusMessage);
                }
                

               
            }

            

            await Shell.Current.GoToAsync("///Views.AlmacenPage");

        }
        
    }

    //TODO: Aqui Tiene que haber un boton en el que se tenga que poner una ubicacion por lo que se accedera a la Base de datos

    private async void descargarDatos(object sender, EventArgs e)
    {
        Peticionesprueba.DownloadAndPrintJson<Pallet>();
    }
    

}