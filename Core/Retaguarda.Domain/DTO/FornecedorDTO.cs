using System;

namespace FiveSys.Retaguarda.Core.Domain.Admin.DTO
{
    public class FornecedorDTO
    {
        public string Id { get; set; }
        public int Numero { get; set; }

        public string Nome { get; set; }
        public string NomeFantasia { get; set; }

        public DateTime Cadastro { get; set; }


    }
}
