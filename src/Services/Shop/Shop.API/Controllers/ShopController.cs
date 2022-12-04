using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shopping.API.Application.Commands.CreateShop;
using System.Net;

namespace Shopping.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShopController : Controller
    {
        private readonly IMediator _mediator;


        public ShopController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        #region - Get Requests -




        #endregion

        #region -Post, Put, Delete Reqeusts -

        /// <summary>
        /// Create Shop
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost()]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> CreateGShop([FromBody] CreateShopCommand model)
        {
            Result result = await _mediator.Send(model);

            return result.IsSuccess ? Ok() : NotFound(result.Error);
        }

        #endregion
    }
}
