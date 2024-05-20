using CommunityToolkit.Mvvm.Messaging;
using MyApplicacion.Database;
using System.Text.RegularExpressions;

namespace MyApplicacion.Views;

public partial class ReferenciasPedidoPage : ContentPage
{
    string ubicacion;
    string referencia;
    string scaneo;
    
    public ReferenciasPedidoPage(string referencia)
    {
        InitializeComponent();
        this.referencia = referencia;
        mostrarPallets();
        WeakReferenceMessenger.Default.Register<String>(this, OnDataReceived);
        
        
    }

    private async void mostrarPallets()
    {
        List<Pallet> pallet = App.dataAccess.MostrarAlmacen(referencia);
        palletList.ItemsSource = pallet;
        //---------------------------------------------------------------
        //List<Pallet> pallet = await App.PalletRepo.MostrarAlmacenados(referencia);
        //palletList.ItemsSource = pallet;
    }
    private void OnDataReceived(object recipient, string message)
    {
        if (message != null)
        {
            Console.WriteLine(message);
            scaneo = message;
            string pattern = @"([a-zA-Z]\d{1,2})";
            Match match = Regex.Match(scaneo, pattern);
            if (match.Success)
            {
                
                ubicacion = match.Groups[1].Value;
                Console.WriteLine("Datos: " + ubicacion);
                ScanResultLabel.Text = "Datos: " + ubicacion;
            }
            else
            {
                Console.WriteLine("No se encontraron coincidencias Referencia.");
            }

        }
    }

    private async void Go_Back(object sender, EventArgs e)
    {
        // Acciones a realizar al pulsar este boton
        await Shell.Current.GoToAsync("///Views.PedidoPage");
        System.Diagnostics.Debug.WriteLine("¡Se hizo clic en el botón Back");

    }

    private async void ItemButton_Clicked(object sender, EventArgs e)
    {
        // Obtener el objeto de datos asociado a la fila seleccionada
        var item = (Pallet)((Button)sender).BindingContext;

        // Realizar alguna acción con el objeto de datos, por ejemplo:
        bool respuesta = await DisplayAlert("Confirmación", $"¿QUIERES RETIRAR EL PALLET ENTERO DE {item.nPiezas} piezas?", "Sí", "No");

        // Verificar la respuesta del usuario
        if (respuesta)
        {
            if (referencia != null)
            {
                App.dataAccess.EliminarPallet(item.Id);
                //-----------------------------------
                //await App.PalletRepo.EliminarPalletPorId(item.Id);
                //string statusMessage = App.PalletRepo.StatusMessage;
                //Console.WriteLine(statusMessage);
            }
            await Shell.Current.GoToAsync("///Views.PedidoPage");

        }
        else
        {
            await Shell.Current.Navigation.PushAsync(new CajasPedidoPage(item.Id));
            //await Shell.Current.GoToAsync("///Views.CajasPedidoPage");
        }
    }
}