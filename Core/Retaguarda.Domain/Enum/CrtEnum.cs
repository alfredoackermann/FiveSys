using System.ComponentModel;

namespace FiveSys.Retaguarda.Core.Domain.Admin.Enum
{
    public enum CrtEnum
    {
        [Description("Simples Nacional")]
        SimplesNacional = 1,
        [Description("Simples Nacional - excesso de sublimite da receita bruta")]
        SimplesNacionalExcessoSublimite = 2,
        [Description("Regime Normal")]
        RegimeNormal = 3
    }
}
