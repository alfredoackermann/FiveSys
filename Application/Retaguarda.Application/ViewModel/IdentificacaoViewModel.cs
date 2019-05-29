using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using FileSys.Shared.Application.ViewModels;
using FiveSys.Retaguarda.Core.Domain.Admin.Entity;

namespace FileSys.Retaguarda.Application.Admin.ViewModel
{
    public class IdentificacaoTelaViewModel
    {
        public IdentificacaoTelaViewModel()
        {

        }

        public IdentificacaoTelaViewModel(IdentificacaoViewModel tipoIdentificacao)
        {
            ApenasUmTipoIdentificacao = true;
            Identificacoes = new IdentificacaoViewModel[1] { tipoIdentificacao };
        }

        public IdentificacaoTelaViewModel(IEnumerable<IdentificacaoViewModel> tiposIdentificacao)
        {
            ApenasUmTipoIdentificacao = false;
            Identificacoes = (tiposIdentificacao.Count() > 0 ? tiposIdentificacao.ToArray() : null);
        }

        public bool ApenasUmTipoIdentificacao { get; set; }

        public IdentificacaoViewModel[] Identificacoes { get; set; }

        #region Campos da tela

        public IDictionary<string, string> TiposIdentificacao { get; set; }

        #endregion
    }

    public class IdentificacaoViewModel : BaseViewModel
    {
        public string MetadataId { get; set; }

        public string TipoIdentificacaoId { get; set; }

        public string Descricao { get; set; }

        public bool Status { get; set; }

        public static void Mapping(Profile mapper)
        {
            mapper.CreateMap<IdentificacaoViewModel, Identificacao>()
                ;

            mapper.CreateMap<Identificacao, IdentificacaoViewModel>()
                ;
        }
    }
}
