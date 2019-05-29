using System.Collections.Generic;
using FileSys.Shared.Core.Domain.Interface.Repository;
using FiveSys.Retaguarda.Core.Domain.Admin.Entity;

namespace FiveSys.Retaguarda.Core.Domain.Admin.Interface.Repository
{
    public interface IIdentificacaoRepository : IBaseRepository<Identificacao, string>
    {
        void Remover(Identificacao Identificacao);
        void RemoverPorId(string id);
        void RemoverTodos(string metadataId);
        void Inserir(Identificacao Identificacao);
        void Atualizar(Identificacao Identificacao);
        IEnumerable<Identificacao> RecuperarTodos(string metadataId);
    }
}
