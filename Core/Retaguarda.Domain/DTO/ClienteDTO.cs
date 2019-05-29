using System;
using FiveSys.Retaguarda.Core.Domain.Admin.Enum;

namespace FiveSys.Retaguarda.Core.Domain.Admin.DTO
{
    public class ClienteDTO
    {
        public string Id { get; set; }

        public string Nome { get; set; }

        public DateTime Cadastro { get; set; }

        public RegiaoEnum? Regiao { get; set; }

        public string TipoCliente { get; set; }

        public decimal? Limite { get; set; }

        public DateTime? UltimaCompra { get; set; }

        public decimal? Pontos { get; set; }

        public int TipoPessoa { get; set; }
    }
}
