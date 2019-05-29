using System;
using System.Text.RegularExpressions;

namespace FileSys.Shared.CrossCutting.Tools
{
    public static class StringExtensions
    {
        public static string FormatarCNPJ(this string cnpj)
        {
            return Convert.ToUInt64(cnpj).ToString(@"00\.000\.000\/0000\-00");
        }

        public static string FormataCPF(this string cpf)
        {
            return Convert.ToUInt64(cpf).ToString(@"000\.000\.000\-00");
        }
        public static string FormataRG(this string rg)
        {
            return Convert.ToUInt64(rg).ToString(@"00\.000\.000\-0");
        }

        public static string FormataValor(this decimal valor)
        {
            return valor.ToString("N2");
        }

        public static string RemoverCaracteres(this string conteudo)
        {
            Regex soNumeros = new Regex(@"[^\d]");

            return soNumeros.Replace(conteudo, "");
        }
    }
}
