using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyApplicacion.Abstractions;

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
