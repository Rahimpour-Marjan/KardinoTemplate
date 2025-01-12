using Api.Model.Employee;
using Application.Account.Commands;
using Application.Account.Queries.FindAll;
using Application.Account.Queries.FindById;
using Application.Helpers;
using Application.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Api.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IUriService _uriService;
        public AccountController(IMediator mediator, IUriService uriService)
        {
            _mediator = mediator;
            _uriService = uriService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] ApiQuery apiQuery)
        {
            try
            {
                var model = await _mediator.Send(new FindAllAccountQuery
                {
                    Query = apiQuery.Query,
                });

                if (apiQuery.Query == null)
                    return StatusCode((int)HttpStatusCode.OK, new ApiResponse
                    {
                        Data = model.Result,
                    });

                var route = Request.Path.Value;
                var pagedReponse = PaginationHelper.CreatePagedResponse(model.Result, model.PageNumber, model.PageSize, model.ResultCount, _uriService, route, null);

                return StatusCode((int)HttpStatusCode.OK, pagedReponse);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, new ApiResponse
                {
                    Errors = new string[] { (ex.Message) },
                });
            }
        }

        //GET api/<UserController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var model = await _mediator.Send(new FindAccountByIdQuery { Id = id });
            return StatusCode((int)HttpStatusCode.OK, new ApiResponse
            {
                Data = model,
            });
        }

        // POST api/<UserController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AccountCreateModel model)
        {
            if (ModelState.IsValid)
            {
                var currentUserId = (int)HttpContext.Items["UserId"];

                var result = await _mediator.Send(new AccountCreate.Command
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Gender = model.Gender,
                    BirthDate = model.BirthDate,
                    NationalCode = model.NationalCode,
                    Phone = model.Phone,
                    ExtraPhone1 = model.ExtraPhone1,
                    ExtraPhone2 = model.ExtraPhone2,
                    ExtraPhone3 = model.ExtraPhone3,
                    Email = model.Email,
                    ExtraEmail = model.ExtraEmail,
                    Fax = model.Fax,
                    Website = model.Website,
                    Instagram = model.Instagram,
                    Telegram = model.Telegram,
                    WhatsApp = model.WhatsApp,
                    Linkedin = model.Linkedin,
                    Facebook = model.Facebook,
                    Address = model.Address,
                    LocationLong = model.LocationLong,
                    LocationLat = model.LocationLat,
                    Job = model.Job,
                    Company = model.Company,
                    CompanyNo = model.CompanyNo,
                    FatherName = model.FatherName,
                    AccountalNumber = model.AccountalNumber,
                    IsActive = model.IsActive,
                    WorkingHoursRate = model.WorkingHoursRate,
                    ReagentName = model.ReagentName,
                    ReagentCode = model.ReagentCode,
                    ImageUrl = model.ImageUrl,
                    DigitalSignatureUrl = model.DigitalSignatureUrl,
                    ResumeUrl = model.ResumeUrl,
                    SpacialAccount = model.SpacialAccount,
                    IsPublic = model.IsPublic,
                    EmployeementDate = model.EmployeementDate,
                    CreatorId=currentUserId,
                });

                if (!result.Success)
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, new ApiResponse
                    {
                        Errors = new string[] { (result.Exception != null ? result.Exception.Message : result.ErrorMessage) },
                    });
                }
                else
                {
                    return StatusCode((int)HttpStatusCode.OK, new ApiResponse
                    {
                        Data = result.Result.AccountId,
                    });
                }
            }
            else
            {
                return StatusCode((int)HttpStatusCode.BadRequest, new ApiResponse
                {
                    Errors = ModelState.GetModelErrors(),
                });
            }
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] AccountUpdateModel model)
        {
            if (ModelState.IsValid)
            {
                var currentUserId = (int)HttpContext.Items["UserId"];

                var result = await _mediator.Send(new AccountUpdate.Command
                {
                    Id = id,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Gender = model.Gender,
                    BirthDate = model.BirthDate,
                    NationalCode = model.NationalCode,
                    Phone = model.Phone,
                    ExtraPhone1 = model.ExtraPhone1,
                    ExtraPhone2 = model.ExtraPhone2,
                    ExtraPhone3 = model.ExtraPhone3,
                    Email = model.Email,
                    ExtraEmail = model.ExtraEmail,
                    Fax = model.Fax,
                    Website = model.Website,
                    Instagram = model.Instagram,
                    Telegram = model.Telegram,
                    WhatsApp = model.WhatsApp,
                    Linkedin = model.Linkedin,
                    Facebook = model.Facebook,
                    Address = model.Address,
                    LocationLong = model.LocationLong,
                    LocationLat = model.LocationLat,
                    Job = model.Job,
                    Company = model.Company,
                    CompanyNo = model.CompanyNo,
                    FatherName = model.FatherName,
                    AccountalNumber = model.AccountalNumber,
                    IsActive = model.IsActive,
                    WorkingHoursRate = model.WorkingHoursRate,
                    ReagentName = model.ReagentName,
                    ReagentCode = model.ReagentCode,
                    ImageUrl = model.ImageUrl,
                    DigitalSignatureUrl = model.DigitalSignatureUrl,
                    ResumeUrl = model.ResumeUrl,
                    SpacialAccount = model.SpacialAccount,
                    IsPublic = model.IsPublic,
                    EmployeementDate = model.EmployeementDate,
                    ModifireId=currentUserId,
                });

                if (!result.Success)
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, new ApiResponse
                    {
                        Errors = new string[] { (result.Exception != null ? result.Exception.Message : result.ErrorMessage) },
                    });
                }
                else
                {
                    return StatusCode((int)HttpStatusCode.OK, new ApiResponse
                    {
                        Data = result.Result.AccountId,
                    });
                }
            }
            else
            {
                return StatusCode((int)HttpStatusCode.BadRequest, new ApiResponse
                {
                    Errors = ModelState.GetModelErrors(),
                });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new AccountDelete.Command
            {
                EmpId = id
            }); ;

            if (!result.Success)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, new ApiResponse
                {
                    Errors = new string[] { (result.Exception != null ? result.Exception.Message : result.ErrorMessage) },
                });
            }
            else
            {
                return StatusCode((int)HttpStatusCode.OK, new ApiResponse
                {
                    Data = result.Result.AccountId,
                });
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int[] ids)
        {
            var result = await _mediator.Send(new AccountDeleteAll.Command
            {
                Ids = ids
            }); ;

            if (!result.Success)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, new ApiResponse
                {
                    Errors = new string[] { (result.Exception != null ? result.Exception.Message : result.ErrorMessage) },
                });
            }
            else
            {
                return StatusCode((int)HttpStatusCode.OK, new ApiResponse
                {
                    Data = result.Result.Result,
                });
            }
        }
    }
}