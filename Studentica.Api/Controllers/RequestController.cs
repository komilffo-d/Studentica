using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Studentica.Api.Services;
using Studentica.Common.DTOs.Project;
using Studentica.Common.DTOs.Request;
using Studentica.Common.DTOs.Requests.Request;
using Studentica.Common.Enums;

namespace Studentica.Api.Controllers
{
    [ApiController]
    [Route("api/requests")]
    public class RequestController : ControllerBase
    {
        private readonly IRequestService<Guid> _requestService;
        public RequestController(IRequestService<Guid> requestService)
        {
            _requestService = requestService;
        }

        [HttpGet("{requestId:guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<RequestDto<Guid>>> GetByIdAsync(Guid requestId)
        {
            return await _requestService.Get(requestId);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IReadOnlyCollection<RequestDto<Guid>>> GetAllAsync([FromQuery] int count = int.MaxValue)
        {
            return await _requestService.GetAllAsync(count, r => r.StatusRequest == StatusRequest.INPROCESS);
        }

        [HttpPost]
        public async Task<ActionResult<RequestDto<Guid>>> PostAsync(RequestCreateRequest request)
        {
            var requestDto = await _requestService.Create(request, HttpContext);

            var actionName = nameof(GetByIdAsync);

            return CreatedAtAction(actionName, new { requestId = requestDto.Id }, requestDto);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("status/update")]
        public async Task<ActionResult<ProjectDto<Guid>>> PostAsync(RequestUpdateStatusRequest<Guid> request)
        {
            var project = await _requestService.UpdateStatus(request, HttpContext);

            return Ok(project);
        }
    }
}
