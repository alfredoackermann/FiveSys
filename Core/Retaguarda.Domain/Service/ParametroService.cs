using System;
using FileSys.Shared.Core.Domain.Impl.Service;
using FileSys.Shared.Core.Domain.Impl.Validator;
using FiveSys.Retaguarda.Core.Domain.Admin.Entity;
using FiveSys.Retaguarda.Core.Domain.Admin.Interface.Repository;
using FiveSys.Retaguarda.Core.Domain.Admin.Interface.Service;

namespace FiveSys.Retaguarda.Core.Domain.Admin.Service
{
    public class ParametroService : BaseCrudService<Parametro>, IParametroService
    {
        private readonly IParametroRepository repository;

        public ParametroService(IParametroRepository repository) : base(repository)
        {
            this.repository = repository;
        }

        public override ResultadoValidacao Inserir(Parametro model)
        {
            if (VerificarDuplicado(model.Nome))
                throw new InvalidOperationException("Já existe um registro com o nome informado.");

            return base.Inserir(model);
        }

        Parametro IParametroService.RecuperarPorNome(string nome)
        {
            if (string.IsNullOrEmpty(nome))
                throw new ArgumentNullException("O nome deve ser informado");

            return repository.RecuperarPorNome(nome);
        }

        private bool VerificarDuplicado(string nome)
        {
            return repository.VerificarDuplicado(nome);
        }
    }
}
