using FileSys.Shared.Core.Domain.Interface.Repository;
using FiveSys.Retaguarda.Core.Domain.Admin.DTO;
using FiveSys.Retaguarda.Core.Domain.Admin.Entity;
using System.Collections.Generic;

namespace FiveSys.Retaguarda.Core.Domain.Admin.Interface.Repository
{
    public interface IFornecedorRepository : IBaseCrudRepository<Fornecedor>
    {
        bool VerificarDuplicado(string RazaoSocial, string id = null);

        bool VerificarDuplicado(string nome, string identificador, string id = null);
        string ProximoFornecedor();

        IEnumerable<FornecedorDTO> Listar();
    }
}
