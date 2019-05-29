using AutoMapper;
using FileSys.Retaguarda.Application.Admin.Interface;
using FileSys.Retaguarda.Application.Admin.ViewModel;
using FileSys.Shared.Application.Interface;
using FiveSys.Retaguarda.Core.Domain.Admin.Entity;
using FiveSys.Retaguarda.Core.Domain.Admin.Interface.Service;

namespace FileSys.Retaguarda.Application.Admin.Impl
{
    public class RamoAppService : BaseCrudAppService<RamoViewModel, Ramo>, IRamoAppService
    {
        private readonly IRamoService service;
        private readonly IMapper mapper;
        public RamoAppService(IRamoService service, IMapper mapper) : base(service, mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }
    }
}
