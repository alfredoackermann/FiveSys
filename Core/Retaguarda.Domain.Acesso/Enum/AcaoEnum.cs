using System.ComponentModel;

namespace FiveSys.Retaguarda.Core.Domain.Acesso.Enum
{
    public enum AcaoEnum
    {
        [Description("Acessar")]
        Acessar = 1,

        [Description("Cadastrar")]
        Cadastrar = 2,

        [Description("Editar")]
        Editar = 4,

        [Description("Excluir")]
        Excluir = 8
    }
}
