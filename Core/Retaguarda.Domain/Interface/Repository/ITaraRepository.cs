using FileSys.Shared.Core.Domain.Interface.Repository;
using FiveSys.Retaguarda.Core.Domain.Admin.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace FiveSys.Retaguarda.Core.Domain.Admin.Interface.Repository
{
    public interface ITaraRepository : IBaseCrudRepository<Tara>
    {
        bool VerificaDuplicado(string descricao);
    }
}
