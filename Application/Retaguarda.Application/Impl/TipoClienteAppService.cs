using AutoMapper;
using FileSys.Retaguarda.Application.Admin.Interface;
using FileSys.Retaguarda.Application.Admin.ViewModel;
using FileSys.Shared.Application.Interface;
using FiveSys.Retaguarda.Core.Domain.Admin.Entity;
using FiveSys.Retaguarda.Core.Domain.Admin.Interface.Service;

namespace FileSys.Retaguarda.Application.Admin.Impl
{
    public class TipoClienteAppService : BaseCrudAppService<TipoClienteViewModel, TipoCliente>, ITipoClienteAppService
    {
        private readonly ITipoClienteService service;
        private readonly IMapper mapper;
        public TipoClienteAppService(ITipoClienteService service,
            IMapper mapper) : base(service, mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }
    }
}
