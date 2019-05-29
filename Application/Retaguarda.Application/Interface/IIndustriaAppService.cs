using FileSys.Retaguarda.Application.Admin.ViewModel;
using FileSys.Shared.Application.Interface;
using FiveSys.Retaguarda.Core.Domain.Admin.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace FileSys.Retaguarda.Application.Admin.Interface
{
    public interface IIndustriaAppService : IBaseCrudAppService<IndustriaViewModel, Industria>
    {
    }
}
