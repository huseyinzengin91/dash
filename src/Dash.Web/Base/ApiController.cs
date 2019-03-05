using System.Collections.Generic;
using Dash.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Dash.Web
{
    [Route("api/v1/[controller]")]
    public class ApiController : SecureController
    {


        [NonAction]
        public IActionResult Success(string message = default(string), object data = default(object), int code = 200)
        {
            return Json(
                new DSReturn()
                {
                    Success = true,
                    Message = message,
                    Data = data,
                    Code = code
                }
            );
        }

         [NonAction]
        public IActionResult Error(string message = default(string), string internalMessage = default(string), object data = default(object), int code = 400, List<DSReturnError> errorMessages = null)
        {
            var rv = new DSReturn()
            {
                Success = false,
                Message = message,
                InternalMessage = internalMessage,
                Data = data,
                Code = code
            };

            if (rv.Code == 500)
                return StatusCode(500, rv);
            if (rv.Code == 401)
                return Unauthorized();
            if (rv.Code == 403)
                return Forbid();
            if (rv.Code == 404)
                return NotFound(rv);

            return BadRequest(rv);
        }

    }
}