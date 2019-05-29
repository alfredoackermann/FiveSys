using AutoMapper;
using FileSys.Retaguarda.Application.Admin.ViewModel;

namespace FileSys.Retaguarda.Application.Admin
{
    public class ConfigurationProfile : Profile
    {
        public ConfigurationProfile()
        {
            Shared.Application.Conversores.Configuracao.Registrar(this);
            this.AllowNullCollections = true;

            ClienteViewModel.Mapping(this);
            ClienteListaViewModel.Mapping(this);
            EmailViewModel.Mapping(this);
            EnderecoViewModel.Mapping(this);
            FornecedorViewModel.Mapping(this);
            FornecedorListaViewModel.Mapping(this);
            IdentificacaoViewModel.Mapping(this);
            IndustriaViewModel.Mapping(this);
            LojaViewModel.Mapping(this);
            LojaListaViewModel.Mapping(this);
            ParametroViewModel.Mapping(this);
            RamoViewModel.Mapping(this);
            TaraViewModel.Mapping(this);
            TelefoneViewModel.Mapping(this);
            TipoClienteViewModel.Mapping(this);
            TipoIdentificacaoViewModel.Mapping(this);
            TipoProdutoViewModel.Mapping(this);
            TipoPrecoViewModel.Mapping(this);
            UsuarioViewModel.Mapping(this);
            UnidadeViewModel.Mapping(this);
            MarcaViewModel.Mapping(this);

        }
    }
}
