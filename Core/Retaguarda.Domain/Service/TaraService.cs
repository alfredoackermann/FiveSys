using FileSys.Shared.Core.Domain.Impl;
using FileSys.Shared.Core.Domain.Impl.Service;
using FileSys.Shared.Core.Domain.Impl.Validator;
using FiveSys.Retaguarda.Core.Domain.Admin.Entity;
using FiveSys.Retaguarda.Core.Domain.Admin.Interface.Repository;
using FiveSys.Retaguarda.Core.Domain.Admin.Interface.Service;
using System;

namespace FiveSys.Retaguarda.Core.Domain.Admin.Service
{
    public class TaraService : BaseCrudService<Tara>, ITaraService
    {
        private readonly ITaraRepository repository;
        public TaraService(ITaraRepository repository) : base(repository)
        {
            this.repository = repository;
        }

        public bool VerificaDuplicado(string descricao)
        {
            return repository.VerificaDuplicado(descricao);
        }

        public override ResultadoValidacao Inserir(Tara model)
        {
            if (VerificaDuplicado(model.Descricao))
                throw new InvalidOperationException(Textos.Geral_Mensagem_Erro_NomeDuplicado);

            return base.Inserir(model);
        }
    }
}
