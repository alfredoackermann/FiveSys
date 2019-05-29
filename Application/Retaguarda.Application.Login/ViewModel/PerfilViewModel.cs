using System.Collections.Generic;
using AutoMapper;
using FileSys.Shared.Application.ViewModels;
using FiveSys.Retaguarda.Core.Domain.Acesso.Entity;

namespace FileSys.Retaguarda.Application.Acesso.ViewModel
{
    public class PerfilViewModel : BaseViewModel
    {
        public string Descricao { get; set; }

        public IEnumerable<PermissaoViewModel> Permissoes {get;set;}

        #region Campos da tela

        public IDictionary<string, string> Funcionalidades { get; set; }
        public IDictionary<string, string> Acoes { get; set; }

        #endregion

        public static void Mapping(Profile Mapper)
        {
            Mapper.CreateMap<PerfilViewModel, Perfil>();
            Mapper.CreateMap<Perfil, PerfilViewModel>();
        }
    }
}
