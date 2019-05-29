using AutoMapper;
using FileSys.Retaguarda.Application.Admin.Interface;
using FileSys.Retaguarda.Application.Admin.ViewModel;
using FileSys.Shared.Application.Impl;
using FileSys.Shared.Application.Interface;
using FiveSys.Retaguarda.Core.Domain.Admin.Entity;
using FiveSys.Retaguarda.Core.Domain.Admin.Interface.Service;
using System.Collections.Generic;

namespace FileSys.Retaguarda.Application.Admin.Impl
{
    public class LojaAppService : BaseCrudAppService<LojaViewModel, Loja>, ILojaAppService
    {
        private readonly ILojaService service;
        private readonly IEmailService emailService;
        private readonly IEnderecoService enderecoService;
        private readonly ITelefoneService telefoneService;
        private readonly IMapper mapper;

        public LojaAppService(ILojaService service,
                                IEmailService emailService,
                                IEnderecoService enderecoService,
                                ITelefoneService telefoneService,
                                IMapper mapper) : base(service, mapper)
        {
            this.service = service;
            this.emailService = emailService;
            this.enderecoService = enderecoService;
            this.telefoneService = telefoneService;
            this.mapper = mapper;
        }

        public string ProximaLoja()
        {
            return service.ProximaLoja();
        }

        IResultadoApplication<ICollection<LojaListaViewModel>> ILojaAppService.Listar()
        {
            var result = new ResultadoApplication<ICollection<LojaListaViewModel>>();

            try
            {
                result.DefinirData(mapper.Map<ICollection<LojaListaViewModel>>(service.Listar()));
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
