using CSharpFunctionalExtensions;
using Discounting.API.Application.Commands.CreateDiscount;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Discounting.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DiscountController : Controller
    {
        private readonly IMediator _mediator;

       


        public DiscountController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        #region - Get Requests -




        #endregion

        #region -Post, Put, Delete Reqeusts -

        /// <summary>
        /// Create Discount
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost()]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> CreateDiscount([FromBody] CreateDiscountCommand model)
        {
            Result result = await _mediator.Send(model);

            return result.IsSuccess ? Ok() : NotFound(result.Error);
        }

        #endregion
    }
   
}

