using AutoMapper;
using FiveSys.Retaguarda.Core.Domain.Acesso.Entity;
using FiveSys.Retaguarda.Core.Domain.Acesso.Enum;

namespace FileSys.Retaguarda.Application.Acesso.ViewModel
{
    public class PermissaoViewModel
    {
        public FuncionalidadeEnum Funcionalidade { get; set; }

        public int Acoes { get; set; }

        public static void Mapping(Profile Mapper)
        {
            Mapper.CreateMap<PermissaoViewModel, Permissao>();

            Mapper.CreateMap<Permissao, PermissaoViewModel>();
        }
    }
}
