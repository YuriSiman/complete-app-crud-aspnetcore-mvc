using Microsoft.AspNetCore.Mvc.Razor;
using System;

namespace CompleteApp.App.Extensions
{
    public static class RazorExtensions
    {
        public static string FormataDocumento(this RazorPage page, int tipoFornecedor, string documento)
        {
            return tipoFornecedor == 1 ? Convert.ToInt64(documento).ToString(@"000\.000\.000\-00") : Convert.ToInt64(documento).ToString(@"00\.000\.000\/0000\-00");
        }
    }
}
