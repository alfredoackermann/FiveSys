using FileSys.Shared.Core.Domain.Impl;
using FileSys.Shared.Core.Domain.Impl.Service;
using FileSys.Shared.Core.Domain.Impl.Validator;
using FileSys.Shared.CrossCutting.Tools;
using FiveSys.Retaguarda.Core.Domain.Admin.Entity;
using FiveSys.Retaguarda.Core.Domain.Admin.Interface.Repository;
using FiveSys.Retaguarda.Core.Domain.Admin.Interface.Service;
using System;
using System.Linq;


namespace FiveSys.Retaguarda.Core.Domain.Admin.Service
{
    public class UsuarioService : BaseCrudService<Usuario>, IUsuarioService
    {
        private readonly IUsuarioRepository repository;
        private readonly IEmailService emailService;

        public UsuarioService(IUsuarioRepository repository, IEmailService emailService) : base(repository)
        {
            this.emailService = emailService;
            this.repository = repository;
        }

        public override ResultadoValidacao Inserir(Usuario model)
        {
            if (VerificaDuplicado(model.Nome, model.Login))
                throw new InvalidOperationException(Textos.Geral_Mensagem_Erro_NomeDuplicado);

            var resultado = base.Inserir(model);

            if (resultado.IsValid)
            {
                model.Senha = MetodosEstaticos.CriptografarSenha(model.Login, MetodosEstaticos.GerarSenha());
                model.Senha = MetodosEstaticos.CriptografarSenha(model.Login, "123456");
                model.SenhaTemporaria = true;

                if (!string.IsNullOrEmpty(model.Email?.Endereco))
                {
                    resultado.AdicionarMensagens(emailService.Processar(model.Email, null, model.Id));
                }
            }
            return resultado;
        }

        public override ResultadoValidacao Atualizar(Usuario model)
        {
            if (VerificaDuplicado(model.Nome, model.Login, model.Id))
                throw new InvalidOperationException(Textos.Geral_Mensagem_Erro_NomeDuplicado);

            var usuario = this.RecuperarPorId(model.Id);
            usuario.PreencherDados(model);

            var resultado = base.Atualizar(model);

            if (resultado.IsValid)
            {
                if (!string.IsNullOrEmpty(model.Email?.Endereco))
                {
                    resultado.AdicionarMensagens(emailService.Processar(model.Email, usuario.Email, model.Id));
                }
            }
            return resultado;

        }

        public override Usuario RecuperarPorId(string id)
        {
            var usuario = base.RecuperarPorId(id);

            usuario.Email = emailService.RecuperarTodos(id).SingleOrDefault();

            return usuario;
        }

        public override void RemoverPorId(string id)
        {
            emailService.RemoverTodos(id);

            base.RemoverPorId(id);
        }

        Usuario IUsuarioService.ValidarLogin(string login, string senha)
        {
            var usuario = repository.RecuperarPorLogin(login);

            if (usuario == null)
                throw new Exception(Textos.Geral_Mensagem_Login_Invalido);

            if (!MetodosEstaticos.CompararSenha(usuario.Senha, senha, login))
                throw new Exception(Textos.Geral_Mensagem_Login_Invalido);

            if (usuario.PerfilId == null)
                throw new Exception(Textos.Geral_Mensagem_Usuario_Sem_Perfil);

            if (usuario.Status.ValorNumerico() == 0)
                throw new Exception(Textos.Geral_Mensagem_Usuario_Desativado);

            usuario.Email = emailService.RecuperarTodos(usuario.Id).SingleOrDefault();

            return usuario;
        }

        Usuario IUsuarioService.TrocarSenha(string id, string novaSenha)
        {
            var usuario = repository.RecuperarPorId(id);

            usuario.Senha = MetodosEstaticos.CriptografarSenha(usuario.Login, novaSenha);
            usuario.SenhaTemporaria = false;

            base.Atualizar(usuario);

            return usuario;
        }

        private bool VerificaDuplicado(string nome, string login, string id = null)
        {
            return repository.VerificarDuplicado(nome, login, id);
        }
    }
}
