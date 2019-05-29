using AutoMapper;
using FileSys.Retaguarda.Application.Admin.Interface;
using FileSys.Retaguarda.Application.Admin.ViewModel;
using FileSys.Shared.Application.Interface;
using FiveSys.Retaguarda.Core.Domain.Admin.Entity;
using FiveSys.Retaguarda.Core.Domain.Admin.Interface.Service;

namespace FileSys.Retaguarda.Application.Admin.Impl
{
    public class TipoProdutoAppService : BaseCrudAppService<TipoProdutoViewModel, TipoProduto>, ITipoProdutoAppService
    {
        private readonly ITipoProdutoService service;
        private readonly IMapper mapper;
        public TipoProdutoAppService(ITipoProdutoService service, IMapper mapper) : base(service, mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }

    }
}
