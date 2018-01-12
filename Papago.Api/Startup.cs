using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using Papago.Business.Services;
using Papago.Core.Logging;
using Papago.Data.DataAccess;

namespace Papago.Api
{
    public class Startup
    {
        public Startup( IConfiguration configuration )
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices( IServiceCollection services )
        {
            services.AddMvc();
            services.AddTransient<IPapagoDbContext, PapagoDbContext>();
            services.AddTransient<ILoggingService, LoggingService>();
            services.AddTransient( typeof( IEntityService<> ), typeof( EntityService<> ) );
            services.AddDbContext<PapagoDbContext>( x => x.UseSqlServer( Configuration.GetConnectionString( "Papago" ) ) );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure( IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory )
        {
            if ( env.IsDevelopment() )
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
            loggerFactory.AddNLog();
        }
    }
}