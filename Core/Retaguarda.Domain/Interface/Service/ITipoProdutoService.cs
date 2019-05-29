using FileSys.Shared.Core.Domain.Interface.Service;
using FiveSys.Retaguarda.Core.Domain.Admin.Entity;

namespace FiveSys.Retaguarda.Core.Domain.Admin.Interface.Service
{
    public interface ITipoProdutoService : IBaseCrudService<TipoProduto>
    {
        bool VerificaDuplicado(string descricao);
    }
}
