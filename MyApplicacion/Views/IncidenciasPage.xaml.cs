namespace MyApplicacion.Views;

public partial class IncidenciasPage : ContentPage
{
    public IncidenciasPage()
    {
        InitializeComponent();
        CargarElementos();
    }

    private void CargarElementos()
    {
        List<string> elementos = new List<string> { "Pallets y cajas rotas", "Ref en pallet que no corresponde", "Ref mezclada en mismo pallet", "Pallet incompletos", "Pallet con mas KLT´s o cajas que la instrucción de trabajo.", "Varios" };
        foreach (var elemento in elementos)
        {
            ddlIncidencias.Items.Add(elemento);
        }
    }

    /*
     * •	Pallets y cajas rotas
•	Ref en pallet que no corresponde
•	Ref mezclada en mismo pallet
•	Pallet incompletos 
•	Pallet con mas KLT´s o cajas que la instrucción de trabajo.

     */

    private async void BtnBack_Clicked(object sender, EventArgs e)
    {
        // Acciones a realizar al pulsar este boton
        await Shell.Current.GoToAsync("///Views.RecepcionPage");
        System.Diagnostics.Debug.WriteLine("¡Se hizo clic en el botón Back");

    }

    private async void BtnValidar_Clicked(object sender, EventArgs e)
    {
        //Aqui se Volvera a la pagina de recepcionarPallet
        await Shell.Current.GoToAsync("///Views.RecepcionPage");
    }

    private async void BtnCancelar_Clicked(object sender, EventArgs e)
    {
        //Aqui se Volvera a la pagina de recepcionarPallet
        await Shell.Current.GoToAsync("///Views.RecepcionPage");
    }
}