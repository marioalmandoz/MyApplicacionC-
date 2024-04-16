
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

namespace MyApplicacion.Services
{
    public class TextProviderService : ITextProvider
    {
        public string GetTextProvider()
        {
            return "Text service";
        }
        public string GetReferencia()
        {
            return "78978";
        }

        public string GetCant()
        {
            return "LA CANTIDAD DE PIEZAS POR CAJA ES: 255";
        }

    }
}
