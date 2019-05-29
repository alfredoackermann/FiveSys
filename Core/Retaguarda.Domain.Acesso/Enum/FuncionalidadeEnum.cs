using System.ComponentModel;

namespace FiveSys.Retaguarda.Core.Domain.Acesso.Enum
{
    public enum FuncionalidadeEnum
    {
        [Description("Cliente")]
        Cliente,
        [Description("Fornecedor")]
        Fornecedor,
        [Description("Identificação")]
        Identificacao,
        [Description("Industria")]
        Industria,
        [Description("Loja")]
        Loja,
        [Description("Marca")]
        Marca,
        [Description("Parâmetro")]
        Parametro,
        [Description("Perfil")]
        Perfil,
        [Description("Ramo")]
        Ramo,
        [Description("Tara")]
        Tara,
        [Description("Tipo Cliente")]
        TipoCliente,
        [Description("Tipo Identificação")]
        TipoIdentificacao,
        [Description("Tipo Preco")]
        TipoPreco,
        [Description("Tipo Produto")]
        TipoProduto,
        [Description("Usuário")]
        Usuario,
        [Description("Unidade")]
        Unidade,
    }
}
