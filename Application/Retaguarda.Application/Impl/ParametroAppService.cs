using AutoMapper;
using FileSys.Retaguarda.Application.Admin.Interface;
using FileSys.Retaguarda.Application.Admin.ViewModel;
using FileSys.Shared.Application.Impl;
using FileSys.Shared.Application.Interface;
using FiveSys.Retaguarda.Core.Domain.Admin.Entity;
using FiveSys.Retaguarda.Core.Domain.Admin.Enum;
using FiveSys.Retaguarda.Core.Domain.Admin.Interface.Service;

namespace FileSys.Retaguarda.Application.Admin.Impl
{
    public class ParametroAppService : BaseCrudAppService<ParametroViewModel, Parametro>, IParametroAppService
    {
        private readonly IParametroService service;
        private readonly IMapper mapper;

        public ParametroAppService(IParametroService service,
            IMapper mapper):base(service, mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }

        IResultadoApplication<ParametroViewModel> IParametroAppService.RecuperarPorNome(string nome)
        {
            var result = new ResultadoApplication<ParametroViewModel>();

            try
            {
                result.DefinirData(mapper.Map<ParametroViewModel>(service.RecuperarPorNome(nome)));
                result.ExecutadoComSuccesso();
            }
            catch (System.Exception ex)
            {
                result.ExecutadoComErro(ex);
            }

            return result;
        }
    }
}
