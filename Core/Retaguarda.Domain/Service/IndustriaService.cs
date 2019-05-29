using FileSys.Shared.Core.Domain.Impl;
using FileSys.Shared.Core.Domain.Impl.Service;
using FileSys.Shared.Core.Domain.Impl.Validator;
using FiveSys.Retaguarda.Core.Domain.Admin.Entity;
using FiveSys.Retaguarda.Core.Domain.Admin.Interface.Repository;
using FiveSys.Retaguarda.Core.Domain.Admin.Interface.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace FiveSys.Retaguarda.Core.Domain.Admin.Service
{
    public class IndustriaService : BaseCrudService<Industria>, IIndustriaService
    {
        private readonly IIndustriaRepository repository;

        public IndustriaService(IIndustriaRepository repository) : base(repository)
        {
            this.repository = repository;
        }
        public override ResultadoValidacao Inserir(Industria model)
        {
            if (VerificaDuplicado(model.Descricao))
                throw new InvalidOperationException(Textos.Geral_Mensagem_Erro_NomeDuplicado);

            return base.Inserir(model);
        }
        public bool VerificaDuplicado(string descricao)
        {
            return repository.VerificaDuplicado(descricao);
        }
    }
}
