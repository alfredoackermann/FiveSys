using System;
using System.Reflection;
using AutoMapper;
using FiveSys.Retaguarda.Infrastructure.CrossCutting.IoC;
using FiveSys.Retaguarda.Infrastructure.Data.Admin;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Retaguarda.Application.Admin.Test
{
    public class InicializacaoFixture : IDisposable
    {
        public ServiceProvider Container { get; set; }

        public InicializacaoFixture()
        {
            var serviceProvider = new ServiceCollection();

            Configuracao.InjecaoDependencia(serviceProvider);

            serviceProvider.AddAutoMapper(typeof(FileSys.Retaguarda.Application.Admin.ConfigurationProfile).GetTypeInfo().Assembly);

            serviceProvider.AddDbContext<RetaguardaAdminContext>(options =>
                options.UseInMemoryDatabase("Retaguarda"));

            Container = serviceProvider.BuildServiceProvider();

            var context = Container.GetService<RetaguardaAdminContext>();
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }

        public void Dispose()
        {
        }
    }

    [CollectionDefinition("Database collection")]
    public class DatabaseCollection : ICollectionFixture<InicializacaoFixture>
    {
        // This class has no code, and is never created. Its purpose is simply
        // to be the place to apply [CollectionDefinition] and all the
        // ICollectionFixture<> interfaces.
    }
}
