using FileSys.Shared.Core.Domain.Impl.Service;
using FiveSys.Retaguarda.Core.Domain.Acesso.Entity;
using FiveSys.Retaguarda.Core.Domain.Acesso.Interface.Repository;
using FiveSys.Retaguarda.Core.Domain.Acesso.Interface.Service;

namespace FiveSys.Retaguarda.Core.Domain.Acesso.Service
{
    public class PermissaoService : BaseService<Permissao, string>, IPermissaoService
    {
        private readonly IPermissaoRepository repository;

        public PermissaoService(IPermissaoRepository repository) : base(repository)
        {
            this.repository = repository;
        }

        void IPermissaoService.Remover(Permissao entity)
        {
            repository.Remover(entity);
        }
    }
}
