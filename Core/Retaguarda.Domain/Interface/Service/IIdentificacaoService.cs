using System.Collections.Generic;
using FileSys.Shared.Core.Domain.Impl.Validator;
using FileSys.Shared.Core.Domain.Interface.Service;
using FiveSys.Retaguarda.Core.Domain.Admin.Entity;

namespace FiveSys.Retaguarda.Core.Domain.Admin.Interface.Service
{
    public interface IIdentificacaoService : IBaseService<Identificacao, string>
    {
        ResultadoValidacao Processar(Identificacao IdentificacaoModel, Identificacao IdentificacaoBancoDados, string metadataId);

        ResultadoValidacao Processar(IEnumerable<Identificacao> IdentificacaosModel, IEnumerable<Identificacao> IdentificacaosBancoDados, string metadataId);

        IEnumerable<Identificacao> RecuperarTodos(string metadataId);

        void RemoverPorId(string id);

        void RemoverTodos(string metadataId);
    }
}
