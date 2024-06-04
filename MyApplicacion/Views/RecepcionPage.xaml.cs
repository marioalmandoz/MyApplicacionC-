using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.Messaging;
using MyApplicacion.Database;
using ProduccionAlmacen.Views;
using System.Text.RegularExpressions;

namespace MyApplicacion.Views;

public partial class RecepcionPage : ContentPage
{
    int cantidad;
    string baan;
    string referencia;
    string scaneo;
    public RecepcionPage()
    {
        InitializeComponent();
        LimpiarDatos();
        palletList.ItemsSource = "";
        App.CurrentPage = "RecepcionPage";
        WeakReferenceMessenger.Default.Register<String>(this, OnDataReceivedRecepcion); 
    }


    private async void mostrarPallets()
    {
        //List<Pallet> lista = App.dataAccess.MostrarDatos();
        List<Pallet> lista = App.dataAccess.MostrarProducido(referencia);
        palletList.ItemsSource = lista;
        Console.WriteLine("Mostrando");
        //----------------------------------------------
        // List<Pallet> pallet = await App.PalletRepo.MostrarProducciones(referencia);
        //palletList.ItemsSource = pallet;
    }
    private void OnDataReceivedRecepcion(object recipient, string message)
    {
        var parts = message.Split('|');
        if (parts.Length>1 && parts[0]== "RecepcionPage")
        {
            Console.WriteLine(message);
            scaneo = parts[1];
            string pattern = @"Q(\d+).+2K([0-9]{5}).+\/\s([0-9]{5})";
            string pattern1 = @"Q(\d+).+2K([0-9]{5})\/([0-9]{5})";
            Match match = Regex.Match(scaneo, pattern);
            if (!match.Success)
            {
                match = Regex.Match(scaneo, pattern1);
            }
            if (match.Success)
            {
                cantidad = int.Parse(match.Groups[1].Value);
                baan = match.Groups[2].Value;
                referencia = match.Groups[3].Value;
                Console.WriteLine("Datos: " + baan + " " + referencia);
                ScanResultLabel.Text = "Datos: "+ baan + " " + referencia;
                mostrarPallets();
            }
            else
            {
                Console.WriteLine("No se encontraron coincidencias Referencia.");
            }

        }
    }
    private async void Go_Back(object sender, EventArgs e)
    {
        LimpiarDatos();
        await Shell.Current.GoToAsync("///Views.AlmacenPage");
    }

    private async void ItemButton_Clicked(object sender, EventArgs e)
    {
        // Obtener el objeto de datos asociado a la fila seleccionada
        var item = (Pallet)((Button)sender).BindingContext;

        // Realizar alguna acción con el objeto de datos, por ejemplo:
        var popup = new PopUpPage("Incidencia", $"¿QUIERES RECEPCIONAR EL PALLET?", 2);
        var respuesta = await this.ShowPopupAsync(popup) as bool?;
        //bool respuesta = await DisplayAlert("Confirmación", $"¿QUIERES RETIRAR EL PALLET ENTERO DE {item.nPiezas} piezas?", "Sí", "No");

        // Verificar la respuesta del usuario
        if (respuesta.HasValue && respuesta.Value)
        {
            App.dataAccess.tickAlmacen(item.Id);
            //-------------------------------------------------
            //await App.PalletRepo.tickAlmacen(item.Id);
            //string statusMessage = App.PalletRepo.StatusMessage;
            //Console.WriteLine(statusMessage);
            LimpiarDatos();
            await Shell.Current.GoToAsync("///Views.AlmacenPage");
        }
        else
        {
            //WeakReferenceMessenger.Default.Send(item.Id.ToString());
            LimpiarDatos();
            await Shell.Current.Navigation.PushAsync(new IncidenciasPage(item.Id,cantidad));
            //await Shell.Current.GoToAsync("///Views.IncidenciasPage");
        }
    }
    private void LimpiarDatos()
    {
        ScanResultLabel.Text = null;
        palletList.ItemsSource = null;
    }
}