using System.Collections.Generic;

namespace FileSys.Shared.Core.Domain.Interface.Entity
{
    public interface IValidacaoEntity
    {
        bool EhValido { get; }

        void AdicionarMensagens(IList<string> mensagens);

        IList<string> RecuperarMensagens();
    }
}
