using System.Collections.Generic;
using FileSys.Shared.Core.Domain.Impl.Validator;
using FileSys.Shared.Core.Domain.Interface.Service;
using FiveSys.Retaguarda.Core.Domain.Admin.Entity;

namespace FiveSys.Retaguarda.Core.Domain.Admin.Interface.Service
{
    public interface IEnderecoService : IBaseService<Endereco, string>
    {
        ResultadoValidacao Processar(Endereco enderecoModel, Endereco enderecoBancoDados, string metadataId);

        ResultadoValidacao Processar(IEnumerable<Endereco> enderecosModel, IEnumerable<Endereco> enderecosBancoDados, string metadataId);

        IEnumerable<Endereco> RecuperarTodos(string metadataId);

        void RemoverPorId(string id);

        void RemoverTodos(string metadataId);
    }
}
