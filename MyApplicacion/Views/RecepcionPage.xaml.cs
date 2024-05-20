using CommunityToolkit.Mvvm.Messaging;
using MyApplicacion.Database;
using System.Text.RegularExpressions;

namespace MyApplicacion.Views;

public partial class RecepcionPage : ContentPage
{

    string baan;
    string referencia;
    string scaneo;
    public RecepcionPage()
    {
        InitializeComponent();
        palletList.ItemsSource = "";
        WeakReferenceMessenger.Default.Register<String>(this, OnDataReceived);
       
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
    private void OnDataReceived(object recipient, string message)
    {
        if (message != null)
        {
            Console.WriteLine(message);
            scaneo = message;
            string pattern = @"2K([0-9]{5}).+\/\s([0-9]{5})";
            Match match = Regex.Match(scaneo, pattern);
            if (match.Success)
            {
                baan = match.Groups[1].Value;
                referencia = match.Groups[2].Value;
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
        await Shell.Current.GoToAsync("///Views.AlmacenPage");
    }

    private async void ItemButton_Clicked(object sender, EventArgs e)
    {
        // Obtener el objeto de datos asociado a la fila seleccionada
        var item = (Pallet)((Button)sender).BindingContext;

        // Realizar alguna acción con el objeto de datos, por ejemplo:

        bool respuesta = await DisplayAlert("Alerta", $"¿VALIDAR EL PALLET DE FECHA: {item.fecha_hora}? ", "VALIDAR", "INCIDENCIA");
        if (respuesta)
        {
            App.dataAccess.tickAlmacen(item.Id);
            //-------------------------------------------------
            //await App.PalletRepo.tickAlmacen(item.Id);
            //string statusMessage = App.PalletRepo.StatusMessage;
            //Console.WriteLine(statusMessage);
            mostrarPallets();
            await Shell.Current.GoToAsync("///Views.AlmacenPage");
        }
        else
        {
            //WeakReferenceMessenger.Default.Send(item.Id.ToString());

            await Shell.Current.Navigation.PushAsync(new IncidenciasPage(item.Id));
            //await Shell.Current.GoToAsync("///Views.IncidenciasPage");
        }
    }
}