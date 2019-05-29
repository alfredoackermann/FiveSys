using FileSys.Shared.Core.Domain.Interface.Service;
using FiveSys.Retaguarda.Core.Domain.Admin.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace FiveSys.Retaguarda.Core.Domain.Admin.Interface.Service
{
    public interface IIndustriaService : IBaseCrudService<Industria>
    {
        bool VerificaDuplicado(string descricao);
    }
}
