namespace MyApplicacion.Views;

public partial class CajasPedidoPage : ContentPage
{
    int id;
	public CajasPedidoPage(int pId)
	{
		InitializeComponent();
        id = pId;
    }
    private async void Validar_Clicked(object sender, EventArgs e)
    {
        // Aqu� puedes acceder al valor introducido en el Entry
        try
        {
            int numeroPiezas = int.Parse(numeroPiezasEntry.Text);

            numeroPiezasEntry.Text = "";
            

            if (numeroPiezas>0)
            {
                // Aqu� puedes realizar la validaci�n y cualquier otra l�gica necesaria
                // Mostrar una alerta para confirmar si el usuario est� seguro
                bool respuesta = await DisplayAlert("Confirmaci�n", $"�Est�s seguro de querer retirar {numeroPiezas} piezas en total?", "S�", "No");

                // Verificar la respuesta del usuario
                if (respuesta)
                {
                    
                    App.dataAccess.RetirarPiezas(numeroPiezas,id);
                    //---------------------------------------------
                  //  await App.PalletRepo.RetirarPiezas(numeroPiezas,id);
                    //Console.WriteLine(App.PalletRepo.StatusMessage);

                    //De momento llevamos a la clase ProduccionPage

                    await Shell.Current.GoToAsync("///Views.PedidoPage");

                }
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Introduce el numero de piezas", "Ok");
        }
    }
    private async void Go_Back(object sender, EventArgs e)
    {
        // Acciones a realizar al pulsar este boton
        string referencia =  App.dataAccess.getReferencia(id);
        if (referencia != null)
        {
            await Shell.Current.Navigation.PushAsync(new ReferenciasPedidoPage(referencia));
        }
    }
}