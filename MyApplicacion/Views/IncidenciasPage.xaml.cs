using CommunityToolkit.Mvvm.Messaging;

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
        List<string> elementos = new List<string> { "Pallets y cajas rotas", "Ref en pallet que no corresponde", "Ref mezclada en mismo pallet", "Pallet incompletos", "Pallet con mas KLT´s o cajas que la instrucción de trabajo.", "Varios" };
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


        System.Diagnostics.Debug.WriteLine("¡Se hizo clic en el botón Back");

    }
    
    private async void BtnValidar_Clicked(object sender, EventArgs e)
    {
        Console.WriteLine(id);
        await App.PalletRepo.addIncidencia(id,ddlIncidencias.SelectedItem.ToString());
        
        //Aqui se Volvera a la pagina de recepcionarPallet
        await Shell.Current.GoToAsync("///Views.AlmacenPage");
    }

    private async void BtnCancelar_Clicked(object sender, EventArgs e)
    {
        //Aqui se Volvera a la pagina de recepcionarPallet
        await Shell.Current.GoToAsync("///Views.RecepcionPage");
    }
}