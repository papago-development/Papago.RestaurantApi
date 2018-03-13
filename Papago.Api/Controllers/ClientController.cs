using Microsoft.AspNetCore.Mvc;
using Papago.Business.Services;
using Papago.Core.Logging;
using Papago.Model.Entities;

namespace Papago.Api.Controllers
{
    [Route( "api/[controller]" )]
    public class ClientController : EntityController<Client>
    {
        public ClientController( IEntityService<Client> entityService, ILoggingService loggingService ) : base( entityService, loggingService )
        { }
    }
}