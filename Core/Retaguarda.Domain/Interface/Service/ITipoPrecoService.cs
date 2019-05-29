using FileSys.Shared.Core.Domain.Interface.Service;
using FiveSys.Retaguarda.Core.Domain.Admin.Entity;

namespace FiveSys.Retaguarda.Core.Domain.Admin.Service
{
    public interface ITipoPrecoService : IBaseCrudService<TipoPreco>
    {
        bool VerificaDuplicado(string descricao);
    }
}
