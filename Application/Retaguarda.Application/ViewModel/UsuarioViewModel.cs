using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using FileSys.Shared.Application.ViewModels;
using FileSys.Shared.CrossCutting.Tools;
using FiveSys.Retaguarda.Core.Domain.Admin.Entity;
using FiveSys.Retaguarda.Core.Domain.Admin.Enum;

namespace FileSys.Retaguarda.Application.Admin.ViewModel
{
    public class UsuarioViewModel : BaseViewModel
    {
        public string Admissao { get; set; }

        public string Login { get; set; }

        public string Nome { get; set; }

        public string Rg { get; set; }

        public string Cpf { get; set; }

        public string Nascimento { get; set; }

        public string Comissao { get; set; }

        public string Observacao { get; set; }

        public string ExpirarSenha { get; set; }

        public string StatusCodigo { get; set; }

        public string PerfilId { get; set; }

        public bool SenhaTemporaria { get; set; }

        public EmailTelaViewModel TelaEmails { get; set; } = new EmailTelaViewModel();

        #region Campos da tela
        public IDictionary<string, string> Situacao { get; set; }
        public IDictionary<string, string> Perfis { get; set; }
        #endregion

        public static void Mapping(Profile mapper)
        {
            mapper.CreateMap<UsuarioViewModel, Usuario>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(orig => orig.StatusCodigo.ToEnum<StatusEnum>()))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(orig => orig.TelaEmails.Emails.FirstOrDefault()))
                .ForMember(dest => dest.Cpf, opt => opt.MapFrom(orig => orig.Cpf.RemoverCaracteres()))
                .ForMember(dest => dest.Rg, opt => opt.MapFrom(orig => orig.Rg.RemoverCaracteres()));

            mapper.CreateMap<Usuario, UsuarioViewModel>()
                .ForMember(dest => dest.StatusCodigo, opt => opt.MapFrom(orig => orig.Status.Valor()))
                .ForMember(dest => dest.Admissao, opt => opt.MapFrom(orig => orig.Admissao.DataFormatada()))
                .ForMember(dest => dest.Nascimento, opt => opt.MapFrom(orig => orig.Nascimento.DataFormatada()))
                .ForMember(dest => dest.TelaEmails, opt => opt.MapFrom(orig => new EmailTelaViewModel(Mapper.Map<Email, EmailViewModel>(orig.Email))))
                .ForMember(dest => dest.Cpf, opt => opt.MapFrom(orig => orig.Cpf.FormataCPF()))
                .ForMember(dest => dest.Rg, opt => opt.MapFrom(orig => orig.Rg.FormataRG()));
        }
    }
}
