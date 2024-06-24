using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.Messaging;
using MyApplicacion.Database;
using ProduccionAlmacen.Views;
using System.Text.RegularExpressions;
namespace MyApplicacion.Views;

public partial class UbicacionPage : ContentPage
{
    string referencia;
    string baan;
    string scaneo;
    string ubicacion;
    public UbicacionPage()
    {
        InitializeComponent();
        LimpiarDatos();
        App.CurrentPage = "UbicacioPage";
        
        WeakReferenceMessenger.Default.Register<String>(this, OnDataReceived);
        
    }

    private void OnDataReceived(object recipient, string message)
    {
        var parts = message.Split('|');
        if (parts.Length > 1 && parts[0] == "UbicacioPage")
        {
            ScanResultLabel.Text = "";
            Console.WriteLine(message);
            scaneo = parts[1];
            ScanResultLabel.Text = message;
            string pattern = @".+2K([0-9]{5}).+\/\s([0-9]{5})";
            string pattern1 = @".+2K([0-9]{5})\/([0-9]{5})";
            Match match = Regex.Match(scaneo, pattern);
            if (!match.Success)
            {
                match = Regex.Match(scaneo, pattern1);
            }
            if (match.Success)
            {
                baan = match.Groups[1].Value;
                referencia = match.Groups[2].Value;

                Console.WriteLine("Datos: " + referencia);
                ScanResultLabel.Text = "Datos: " + referencia;
                mostrarPallets();

            }
            else
            {
                Console.WriteLine("No se encontraron coincidencias Referencia.");
            }
            string pattern2 = @".+([HhJjIiABCD]\d{1,2}|SUELO|BASQUEPACK)$";
            Match match1 = Regex.Match(scaneo, pattern2);
            if (match1.Success)
            {
                ubicacion = match1.Groups[1].Value;
                Console.WriteLine("Ubicacion: " + ubicacion);
                ScanResultLabel.Text = "Ubicacion: " + ubicacion;
                
              
            }
        }
    }
    private async void mostrarPallets()
    {
        //List<Pallet> lista = App.dataAccess.MostrarDatos();
        List<Pallet> pallet = App.dataAccess.MostrarAlmacen(baan);
        palletList.ItemsSource = pallet;
        //--------------------------------------------------
        //List<Pallet> pallet = await App.PalletRepo.MostrarAlmacenados(referencia);
        //palletList.ItemsSource = pallet;
    }
    private async void Go_Back(object sender, EventArgs e)
    {
        // Acciones que quieres realizar cuando se hace clic en el botón 1
        // Por ejemplo, mostrar un mensaje en la consola
        LimpiarDatos();
        await Shell.Current.GoToAsync("///Views.AlmacenPage");

        System.Diagnostics.Debug.WriteLine("¡Se hizo clic en el botón Back");

    }

    private async void ItemButton_Clicked(object sender, EventArgs e)
    {
        // Obtener el objeto de datos asociado a la fila seleccionada
        var item = (Pallet)((Button)sender).BindingContext;

        // Realizar alguna acción con el objeto de datos, por ejemplo:
       // bool respuesta = await DisplayAlert("Confirmación", $"¿QUIERES REUBICAR EL PALLET {item.Id}?", "Sí", "No");
        var popup = new PopUpPage("Confirmación", $"¿QUIERES REUBICAR EL PALLET {item.baan} en la ubicacion {ubicacion}?", 2);
        var respuesta = await this.ShowPopupAsync(popup) as bool?;

        // Verificar la respuesta del usuario
        if (respuesta.HasValue&&respuesta.Value)
        {

            if (referencia != null)
            {
                if (ubicacion == null)
                {
                    popup = new PopUpPage("Error", $"ESCANEE LA UBICACION", 1);
                    await this.ShowPopupAsync(popup);
                   // await DisplayAlert("Error", $"ESCANEE LA UBICACION", "Ok");
                }
                else
                {
                    App.dataAccess.ModificarUbicacion(item.Id, ubicacion);
                    LimpiarDatos();
                    //--------------------------------------------- 
                    //Realizar la subida de datos
                    //Peticiones.EnviarDatos(referencia, item.ubicacion, ubicacion, item.nPiezas);
                    await Shell.Current.GoToAsync("///Views.AlmacenPage");
                }
            }
        }
        
    }
    private void LimpiarDatos()
    {
        ScanResultLabel.Text = null;
        palletList.ItemsSource = null;
        ubicacion = null;
    }
}