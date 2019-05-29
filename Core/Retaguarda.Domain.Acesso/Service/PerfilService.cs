using System;
using System.Linq;
using FileSys.Shared.Core.Domain.Impl.Service;
using FileSys.Shared.Core.Domain.Impl.Validator;
using FiveSys.Retaguarda.Core.Domain.Acesso.Entity;
using FiveSys.Retaguarda.Core.Domain.Acesso.Interface.Repository;
using FiveSys.Retaguarda.Core.Domain.Acesso.Interface.Service;

namespace FiveSys.Retaguarda.Core.Domain.Acesso.Service
{
    public class PerfilService : BaseCrudService<Perfil>, IPerfilService
    {
        private readonly IPerfilRepository repository;
        private readonly IPermissaoService permissaoService;

        public PerfilService(IPerfilRepository repository,
            IPermissaoService permissaoService) : base(repository)
        {
            this.repository = repository;
            this.permissaoService = permissaoService;
        }

        public override ResultadoValidacao Inserir(Perfil model)
        {
            if (VerificaDuplicado(model.Descricao, null))
                throw new InvalidOperationException("Já existe um registro com o nome informado.");

            return base.Inserir(model);
        }

        public override ResultadoValidacao Atualizar(Perfil model)
        {
            if (VerificaDuplicado(model.Descricao, model.Id))
                throw new InvalidOperationException("Já existe um registro com o nome informado.");

            var perfil = base.RecuperarPorId(model.Id);

            // Remove permissoes desmarcadas
            perfil.Permissoes.Where(x => !model.Permissoes.Select(m => m.Funcionalidade).ToArray().Contains(x.Funcionalidade))
                .ToList()
                .ForEach(x => permissaoService.Remover(x));

            var permissoesPerfil = perfil.Permissoes.ToList();

            foreach(var permissao in model.Permissoes)
            {
                var permissaoExistente = perfil.Permissoes.SingleOrDefault(x => x.Funcionalidade == permissao.Funcionalidade);
                if (permissaoExistente == null)
                    permissoesPerfil.Add(permissao);
                else
                    permissaoExistente.Acoes = permissao.Acoes;
            }

            perfil.Permissoes = permissoesPerfil;

            return base.Atualizar(model);
        }

        private bool VerificaDuplicado(string descricao, string id)
        {
            return repository.VerificaDuplicado(descricao, id);
        }
    }
}
