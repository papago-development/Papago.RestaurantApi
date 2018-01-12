using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.ApplicationInsights.AspNetCore.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Papago.Business.Services;
using Papago.Core.Logging;
using Papago.Model.BaseEntities;

namespace Papago.Api.Controllers
{
    [Route( "api/[controller]" )]
    public class EntityController<TEntity> : Controller
            where TEntity : BaseEntity
    {
        private readonly IEntityService<TEntity> _entityService;
        private readonly ILoggingService _loggingService;

        public EntityController( IEntityService<TEntity> entityService, ILoggingService loggingService )
        {
            _entityService = entityService;
            _loggingService = loggingService;
        }

        // GET api/Category
        [HttpGet]
        public IActionResult List()
        {
            try
            {
                var entity = _entityService.List();
                return Ok( entity );
            }
            catch ( Exception ex )
            {
                _loggingService.Error( ex );
                return StatusCode( StatusCodes.Status500InternalServerError );
            }
        }

        // GET api/Category/5
        [HttpGet( "{id}" )]
        public IActionResult Get( int id )
        {
            try
            {
                var entity = _entityService.Get( id );
                if ( entity == null )
                {
                    return NotFound();
                }
                return Ok( entity );
            }
            catch ( Exception ex )
            {
                _loggingService.Error( ex );
                return StatusCode( StatusCodes.Status500InternalServerError );
            }
        }

        // POST api/Category
        [HttpPost]
        public IActionResult Post( [FromBody] TEntity value )
        {
            try
            {
                var userEmail = User?.Identity?.Name;
                var createdEntity = _entityService.Create( value, userEmail );
                return Created( Request.GetUri() + createdEntity.Id.ToString(), createdEntity );
            }
            catch ( Exception ex )
            {
                _loggingService.Error( ex );
                return StatusCode( StatusCodes.Status500InternalServerError );
            }
        }

        // PUT api/Category/5
        [HttpPatch( "{id}" )]
        public IActionResult Patch( int id, [FromBody] TEntity value )
        {
            try
            {
                var userEmail = User?.Identity?.Name;
                string requestBody;
                using ( var stream = new StreamReader( Request.Body ) )
                {
                    stream.BaseStream.Position = 0;
                    requestBody = stream.ReadToEnd();
                }

                var objectAsDictionary = JsonConvert.DeserializeObject( requestBody, typeof( Dictionary<string, string> ) );
                var updateKeys = ( ( Dictionary<string, string> ) objectAsDictionary ).Keys;
                var pascalCaseUpdateKeys = new List<string>();
                foreach ( var updateKey in updateKeys )
                {
                    pascalCaseUpdateKeys.Add( updateKey.Substring( 0, 1 ).ToUpper() + updateKey.Substring( 1 ) );
                }
                var receivedObject = ( TEntity ) JsonConvert.DeserializeObject( requestBody, typeof( TEntity ) );

                var found = _entityService.Update( receivedObject, id, pascalCaseUpdateKeys, userEmail );
                if ( found )
                {
                    return Ok();
                }
                return NotFound();
            }
            catch ( Exception ex )
            {
                _loggingService.Error( ex );
                return StatusCode( StatusCodes.Status500InternalServerError );
            }
        }

        // DELETE api/Category/5
        [HttpDelete( "{id}" )]
        public IActionResult Delete( int id )
        {
            try
            {
                var deleted = _entityService.Delete( id );
                if ( deleted )
                {
                    return Ok();
                }
                return NotFound();
            }
            catch ( Exception ex )
            {
                _loggingService.Error( ex );
                return StatusCode( StatusCodes.Status500InternalServerError );
            }
        }
    }
}