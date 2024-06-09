using Microsoft.AspNetCore.Mvc;
using Studentica.Api.Services;
using Studentica.Identity.Common.Models;

namespace Studentica.Api.Controllers
{
    [Route("api/identity")]
    [ApiController]
    public class IdentityController : ControllerBase
    {

        private readonly IIdentityService<Guid> _identityService;

        public IdentityController(IIdentityService<Guid> identityService)
        {
            _identityService = identityService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<ResponseModel>> Login([FromBody] LoginModel model)
        {
            var result = await _identityService.Login(model);

            if (result == null)
                return Unauthorized(new ResponseModel
                {
                    Status = "Ошибка",
                    Message = "Указаны неверные авторизационные данные!",
                });

            return Ok(new ResponseModel
            {
                Status = "Успешно",
                Message = "Предоставлен новый авторизационный токен",
                Token = result.Value.Item1,
                Expiration = result.Value.Item2
            });

        }

        [HttpPost]
        [Route("register")]
        public async Task<ActionResult<ResponseModel>> Register([FromBody] RegisterModel model)
        {
            var result = await _identityService.Register(model);

            if (result.Item1 == false)
                return StatusCode(StatusCodes.Status400BadRequest, new ResponseModel { Status = "Ошибка", Message = result.Item2 });

            return StatusCode(StatusCodes.Status200OK, new ResponseModel { Status = "Успешно", Message = result.Item2 });
        }

        [HttpGet]
        [Route("validate")]
        public async Task<ActionResult<string>> Validate([FromQuery] string token)
        {
            var result = await _identityService.Validate(token);

            if (result == false)
                return StatusCode(StatusCodes.Status403Forbidden, new ResponseModel { Status = "Ошибка", Message = "Токен не является достоверным!" });

            return StatusCode(StatusCodes.Status200OK, new ResponseModel
            {
                Status = "Успешно",
                Message = "Токен прошёл валидацию!",
                Token = token,
            });

        }

    }
}
