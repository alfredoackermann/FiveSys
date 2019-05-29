using System.Collections.Generic;
using FileSys.Shared.Core.Domain.Impl.Validator;
using FileSys.Shared.Core.Domain.Interface.Service;
using FiveSys.Retaguarda.Core.Domain.Admin.Entity;

namespace FiveSys.Retaguarda.Core.Domain.Admin.Interface.Service
{
    public interface ITelefoneService : IBaseService<Telefone, string>
    {
        ResultadoValidacao Processar(Telefone telefoneModel, Telefone telefoneBancoDados, string metadataId);

        ResultadoValidacao Processar(IEnumerable<Telefone> telefonesModel, IEnumerable<Telefone> telefonesBancoDados, string metadataId);

        IEnumerable<Telefone> RecuperarTodos(string metadataId);

        void RemoverPorId(string id);

        void RemoverTodos(string metadataId);
    }
}
