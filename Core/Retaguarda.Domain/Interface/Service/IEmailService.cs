using System.Collections.Generic;
using FileSys.Shared.Core.Domain.Impl.Validator;
using FileSys.Shared.Core.Domain.Interface.Service;
using FiveSys.Retaguarda.Core.Domain.Admin.Entity;

namespace FiveSys.Retaguarda.Core.Domain.Admin.Interface.Service
{
    public interface IEmailService : IBaseService<Email, string>
    {
        ResultadoValidacao Processar(Email emailModel, Email emailBancoDados, string metadataId);

        ResultadoValidacao Processar(IEnumerable<Email> emailsModel, IEnumerable<Email> emailsBancoDados, string metadataId);

        IEnumerable<Email> RecuperarTodos(string metadataId);

        void RemoverPorId(string id);

        void RemoverTodos(string metadataId);
    }
}
