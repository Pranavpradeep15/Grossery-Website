using Azure;
using VegeSite.Services.Contract;
using VegeSite.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BCrypt.Net;
using VegeSite.Models;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Authorization;
using VegeSite.Utilities;
using VegeSite.Services.Contract;

namespace VegeSite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserServices _userServices;
        public UserController(IUserServices userServices)
        {
            _userServices = userServices;
        }
    
        [HttpPost]
     
        public IActionResult postCustomerDetails(VegetableRequest vegetableRequest)
        {
            ResponApi<VegetableRequest> responce = new ResponApi<VegetableRequest>();

            try
            {
                VegetableRequest newdeatil = _userServices.AddData(vegetableRequest);
                responce = new ResponApi<VegetableRequest>
                {
                    Status = true,
                    Msg = "Added",
                    Value = newdeatil
                };
                return StatusCode(StatusCodes.Status200OK, responce);

            }
            catch (Exception ex)
            {
                responce = new ResponApi<VegetableRequest>(); responce.Msg = ex.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, responce);

            }
        }

        [HttpPost("verify")]
        public ActionResult<VegetableRequest> CheckUser([FromBody] VegetableRequest Request)
        {
            try
            {
                var responses = this._userServices.Verify(Request);
                if (string.IsNullOrWhiteSpace(Request.Email) || string.IsNullOrWhiteSpace(Request.UserPassword))
                {
                    return BadRequest(responses);
                }
                if (responses == "verification failed" || responses == "Wrong Password")
                {
                    return BadRequest(responses);
                }
                return Ok(responses);

            }
            catch (Exception ex)
            {
                var response = new ResponApi<VegetableRequest>();
                response.Msg = ex.Message;
                return BadRequest(response);
            }
        }

        [HttpGet]

        public IActionResult Get()
        {
            ResponApi<List<Vegetable>> response = new ResponApi<List<Vegetable>>();
            try
            {
                List<Vegetable> employeelist = _userServices.GetDetails();
                if (employeelist.Count == 0)
                {
                    response = new ResponApi<List<Vegetable>> { Status = false, Msg = "no data found" };
                }
                else
                {
                    response = new ResponApi<List<Vegetable>> { Status = true, Msg = "Data Found", Value = employeelist };
                }
                return StatusCode(StatusCodes.Status200OK, response);
            }
            catch (Exception ex)
            {
                response = new ResponApi<List<Vegetable>>() { Status = false, Msg = ex.Message };
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

    }


}




