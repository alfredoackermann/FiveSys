using System.ComponentModel;

namespace FiveSys.Retaguarda.Core.Domain.Admin.Enum
{
    public enum ParametroTipoEnum
    {
        [Description("Data")]
        Data = 1,
        [Description("Número")]
        Numero = 2,
        [Description("Texto")]
        Texto = 3
    }
}
