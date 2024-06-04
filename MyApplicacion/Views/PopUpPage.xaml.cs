namespace ProduccionAlmacen.Views;
using CommunityToolkit.Maui.Views;

public partial class PopUpPage : Popup
{
	public PopUpPage(String pTitulo, string pTexto, int nButtons)
	{
		InitializeComponent();
		Titulo.Text = pTitulo;
		Texto.Text = pTexto;
		if(nButtons == 1) {
			cancelBtn.IsVisible = false;
		}
		if(pTitulo == "Cuesti�n")
		{
			cancelBtn.Text = "Por cajas";
		}else if (pTitulo == "Incidencia")
		{
			cancelBtn.Text = "Incidencias";
		}
	}

	private void OKButton_Clicked(object sender, EventArgs e)
	{
		Close(true);
	}
	private void CancelClicked(object sender, EventArgs e)
	{
		Close(false);
	}
}