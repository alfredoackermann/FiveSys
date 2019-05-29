using AutoMapper;
using FileSys.Retaguarda.Application.Acesso.Interface;
using FileSys.Retaguarda.Application.Acesso.ViewModel;
using FileSys.Shared.Application.Interface;
using FiveSys.Retaguarda.Core.Domain.Acesso.Entity;
using FiveSys.Retaguarda.Core.Domain.Acesso.Interface.Service;

namespace FileSys.Retaguarda.Application.Acesso.Impl
{
    public class PerfilAppService : BaseCrudAppService<PerfilViewModel, Perfil>, IPerfilAppService
    {
        private readonly IPerfilService service;
        private readonly IMapper mapper;
        public PerfilAppService(IPerfilService service, IMapper mapper) : base(service, mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }
    }
}
