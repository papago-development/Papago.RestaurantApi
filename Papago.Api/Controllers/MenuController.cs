using Microsoft.AspNetCore.Mvc;
using Papago.Business.Services;
using Papago.Core.Logging;
using Papago.Model.Entities;

namespace Papago.Api.Controllers
{
    [Route( "api/[controller]" )]
    public class MenuController : EntityController<Menu>
    {
        public MenuController( IEntityService<Menu> entityService, ILoggingService loggingService ) : base( entityService, loggingService )
        { }
    }
}