using AutoMapper;
using FileSys.Retaguarda.Application.Admin.Interface;
using FileSys.Retaguarda.Application.Admin.ViewModel;
using FileSys.Shared.Application.Interface;
using FiveSys.Retaguarda.Core.Domain.Admin.Entity;
using FiveSys.Retaguarda.Core.Domain.Admin.Service;

namespace FileSys.Retaguarda.Application.Admin.Impl
{
    public class TipoPrecoAppService : BaseCrudAppService<TipoPrecoViewModel, TipoPreco>, ITipoPrecoAppService
    {
        private readonly ITipoPrecoService service;
        private readonly IMapper mapper;
        public TipoPrecoAppService(ITipoPrecoService service, IMapper mapper) : base(service, mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }
    }
}
