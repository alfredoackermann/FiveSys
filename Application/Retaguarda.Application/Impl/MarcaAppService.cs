using AutoMapper;
using FileSys.Retaguarda.Application.Admin.Interface;
using FileSys.Retaguarda.Application.Admin.ViewModel;
using FileSys.Shared.Application.Interface;
using FiveSys.Retaguarda.Core.Domain.Admin.Entity;
using FiveSys.Retaguarda.Core.Domain.Admin.Interface.Service;

namespace FileSys.Retaguarda.Application.Admin.Impl
{
    public class MarcaAppService : BaseCrudAppService<MarcaViewModel, Marca>, IMarcaAppService
    {
        private readonly IMarcaService service;
        private readonly IMapper mapper;

        public MarcaAppService(IMarcaService service, IMapper mapper) : base(service, mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }
    }
}
