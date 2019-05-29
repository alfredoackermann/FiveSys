using System.ComponentModel;

namespace FiveSys.Retaguarda.Core.Domain.Admin.Enum
{
    public enum TipoTelefoneEnum
    {
        [Description("Celular")]
        Celular = 1,
        [Description("Comercial")]
        Comercial = 2,
        [Description("Residencial")]
        Residencial = 3
    }
}
