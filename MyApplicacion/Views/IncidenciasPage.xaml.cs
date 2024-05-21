using CommunityToolkit.Mvvm.Messaging;
using Plugin.Maui.Audio;

namespace MyApplicacion.Views;

public partial class IncidenciasPage : ContentPage
{
    int id;
    public IncidenciasPage(int pId)
    {
        InitializeComponent();
        CargarElementos();
        id = pId;
       // WeakReferenceMessenger.Default.Register<String>(this, getElemento);
    }

    private void CargarElementos()
    {
        List<string> elementos = new List<string> { "Pallets y cajas rotas", "Ref en pallet que no corresponde", "Ref mezclada en mismo pallet", "Pallet incompletos", "Pallet con mas KLT�s o cajas que la instrucci�n de trabajo.", "Varios" };
        foreach (var elemento in elementos)
        {
            ddlIncidencias.Items.Add(elemento);
        }
    }


    private async void Go_Back(object sender, EventArgs e)
    {
        // Acciones a realizar al pulsar este boton
        //string referencia = await App.PalletRepo.getReferencia(id);
        //if (referencia != null)
        //{
        //    await Shell.Current.Navigation.PushAsync(new RecepcionPage());
        //}
        await Shell.Current.GoToAsync("///Views.RecepcionPage");


        System.Diagnostics.Debug.WriteLine("�Se hizo clic en el bot�n Back");

    }
    
    private async void BtnValidar_Clicked(object sender, EventArgs e)
    {
        Console.WriteLine(id);
        int row = App.dataAccess.addIncidencias(id, ddlIncidencias.SelectedItem.ToString());
        if (row > 0)
        {
            AudioManager.Current.CreatePlayer(await FileSystem.OpenAppPackageFileAsync("aprobacion_sound.wav")).Play();
            await DisplayAlert("Confirmaci�n", $"Se ha Recepcionado el pallet", "Ok");
        }
        else
        {
            AudioManager.Current.CreatePlayer(await FileSystem.OpenAppPackageFileAsync("error_sound.wav")).Play();
            await DisplayAlert("Error", $"No se ha podido recepcionar el pallet", "Ok");
        }
        // await App.PalletRepo.addIncidencia(id,ddlIncidencias.SelectedItem.ToString());


        //Aqui se Volvera a la pagina de recepcionarPallet
        await Shell.Current.GoToAsync("///Views.AlmacenPage");
    }

    private async void BtnCancelar_Clicked(object sender, EventArgs e)
    {
        //Aqui se Volvera a la pagina de recepcionarPallet
        await Shell.Current.GoToAsync("///Views.RecepcionPage");
    }
}