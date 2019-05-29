using FileSys.Shared.Core.Domain.Interface.Repository;
using FiveSys.Retaguarda.Core.Domain.Admin.Entity;

namespace FiveSys.Retaguarda.Core.Domain.Admin.Interface.Repository
{
    public interface ITipoPrecoRepository : IBaseCrudRepository<TipoPreco>
    {
        bool VerificaDuplicado(string descricao);
    }
}
