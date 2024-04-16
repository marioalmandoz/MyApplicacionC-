
/* Cambio no fusionado mediante combinación del proyecto 'MyApplicacion (net8.0-android)'
Antes:
using System;
Después:
using MyApplicacion.Abstractions;
using System;
*/
using
/* Cambio no fusionado mediante combinación del proyecto 'MyApplicacion (net8.0-android)'
Antes:
using System.Threading.Tasks;
using MyApplicacion.Abstractions;
Después:
using System.Threading.Tasks;
*/
MyApplicacion.Abstractions;

namespace MyApplicacion.ViewModels
{
    public class SecondViewModel : BindableObject
    {
        private ITextProvider _textProvider;
        private string _name = "";
        private string _referencia = "";
        private string _cant = "";
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }
        public string Referencia
        {
            get => _referencia;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }
        public string Cant
        {
            get => _cant;
            set
            {
                _cant = value;
                OnPropertyChanged();
            }
        }
        public SecondViewModel(ITextProvider textProvider)
        {
            this._textProvider = textProvider;
            Name = _textProvider.GetTextProvider();
            Referencia = _textProvider.GetReferencia();
            Cant = _textProvider.GetCant();
        }
    }
}
