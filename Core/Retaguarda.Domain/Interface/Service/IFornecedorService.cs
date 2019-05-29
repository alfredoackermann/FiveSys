using FileSys.Shared.Core.Domain.Interface.Service;
using FiveSys.Retaguarda.Core.Domain.Admin.DTO;
using FiveSys.Retaguarda.Core.Domain.Admin.Entity;
using System.Collections.Generic;

namespace FiveSys.Retaguarda.Core.Domain.Admin.Interface.Service
{
    public interface IFornecedorService : IBaseCrudService<Fornecedor>
    {
        string ProximoFornecedor();
        bool VerificarDuplicado(string razaoSocial, string id);
        IEnumerable<FornecedorDTO> Listar();
    }
}
