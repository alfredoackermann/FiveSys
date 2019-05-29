using AutoMapper;
using FileSys.Retaguarda.Application.Acesso.ViewModel;

namespace FileSys.Retaguarda.Application.Acesso
{
    public class ConfigurationProfile : Profile
    {
        public ConfigurationProfile()
        {
            PerfilViewModel.Mapping(this);
            PermissaoViewModel.Mapping(this);
        }
    }
}
