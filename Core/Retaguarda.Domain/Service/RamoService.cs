using FileSys.Shared.Core.Domain.Impl;
using FileSys.Shared.Core.Domain.Impl.Service;
using FileSys.Shared.Core.Domain.Impl.Validator;
using FiveSys.Retaguarda.Core.Domain.Admin.Entity;
using FiveSys.Retaguarda.Core.Domain.Admin.Interface.Repository;
using FiveSys.Retaguarda.Core.Domain.Admin.Interface.Service;
using System;

namespace FiveSys.Retaguarda.Core.Domain.Admin.Service
{
    public class RamoService : BaseCrudService<Ramo>, IRamoService
    {
        private readonly IRamoRepository repository;
        public RamoService(IRamoRepository repository) : base(repository)
        {
            this.repository = repository;
        }

        public override ResultadoValidacao Inserir(Ramo model)
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
