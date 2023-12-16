
using DataProcessorService.MyDbContext;
using DataProcessorService.Repository;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProcessorService.RegisterDI
{
    public static class RegisterDI
    {
        public static void Register(IServiceCollection repository)
        {
            repository.AddScoped<SQLiteDataBase>();
            repository.AddScoped<ModuleCatIDModuleStateRepository>();
        }
    }
}
