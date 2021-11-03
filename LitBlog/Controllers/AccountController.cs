using AutoMapper;
using LitBlog.API.Models;
using LitBlog.BLL.ModelsDto;
using LitBlog.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using LitBlog.API.Helpers;
using LitBlog.BLL.Services;

namespace LitBlog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;

        public AccountController(IAccountService accountService, IMapper mapper, IEmailService emailService)
        {
            _accountService = accountService;
            _mapper = mapper;
            _emailService = emailService;
        }

        [HttpPost("auth")]
        public async Task<ActionResult<AuthenticateResponseViewModel>> Authenticate(AuthenticateRequestViewModel request)
        {
            var requestDto = _mapper.Map<AuthenticateRequestDto>(request);
            var response = await _accountService.Authenticate(requestDto, IpAddress());
            SetTokenCookie(response.RefreshToken);
            var mapper = _mapper.Map<AuthenticateResponseViewModel>(response);
            return Ok(mapper);
        }


        [HttpPost("register")]
        public async Task<ActionResult> SignUp(AccountRegisterViewModel request)
        {
            var accountDto = _mapper.Map<AccountDto>(request);
            if (_accountService.ExistsAccount(accountDto))
            {
                await _emailService.SendAlreadyRegisteredEmail(accountDto.Email, Request.Headers["origin"]);
                return BadRequest(new { message = $"User with this mail {accountDto.Email} already exists" });
            }

            var account = _mapper.Map<AccountDto>(request);

            await _accountService.Register(account, Request.Headers["origin"]);
            return Ok(new { message = "Registration successful" });
        }


        [HttpPost("verify")]
        public async Task<ActionResult> Verify(VerifyRequestViewModel verifyRequest)
        {
            await _accountService.VerifyEmail(verifyRequest.Token);
            return Ok(new { message = "Verification successful, you can now login" });
        }

        [HttpPost("forgot-password")]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordRequestViewModel model)
        {
            var mapModel = _mapper.Map<ForgotPasswordRequestDto>(model);
            await _accountService.ForgotPassword(mapModel, Request.Headers["origin"]);
            return Ok(new { message = "Please check your email for password reset instructions" });
        }

        [HttpPost("reset-password")]
        public async Task<ActionResult> ResetPassword(ResetPasswordRequestViewModel model)
        {
            var mapModel = _mapper.Map<ResetPasswordRequestDto>(model);
            await _accountService.ResetPassword(mapModel);
            return Ok(new { message = "Password reset successful, you can now login" });
        }

        [HttpPost("refresh-token")]
        public async Task<ActionResult<AuthenticateResponseViewModel>> RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            var response = await _accountService.RefreshToken(refreshToken, IpAddress());
            SetTokenCookie(response.RefreshToken);
            return Ok(response);
        }

        [Authorize]
        [HttpPost("revoke-token")]
        public IActionResult RevokeToken(RevokeTokenRequestViewModel model)
        {
            var id = IdContext.GetUserId(HttpContext);
            var userPermission = _accountService.GetAccountById(id);
            if (userPermission.Result.Role != "Admin")
                return Unauthorized(new { message = "Unauthorized" });

            var token = model.Token ?? Request.Cookies["refreshToken"];

            if (string.IsNullOrEmpty(token))
                return BadRequest(new { message = "Token is required" });

            _accountService.RevokeToken(token, IpAddress());
            return Ok(new { message = "Token revoked" });
        }
        [HttpGet]
        [Authorize]
        public ActionResult<IQueryable<AccountResponseViewModel>> GetAllAccount()
        {
            var id = IdContext.GetUserId(HttpContext);
            var accountsPermission = _accountService.GetAccountById(id);
            if (accountsPermission.Result.Role == "Admin")
            {
                var result = _accountService.GetAll();
                return Ok(result);
            }
            return Unauthorized(new { message = "Unauthorized" });
        }

        [HttpGet("get-users")]
        [Authorize]
        public ActionResult<IList<UserResponseViewModel>> GetAllUsers()
        {
            var id = IdContext.GetUserId(HttpContext);
            var accountsPermission = _accountService.GetAccountById(id);
            if (accountsPermission.Result.Role == "Admin")
            {
                var result = _accountService.GetUsers();
                return Ok(result);
            }
            return Unauthorized(new { message = "Unauthorized" });
        }

        [HttpGet("{id:int}")]
        [Authorize]
        public ActionResult<IList<AccountResponseViewModel>> GetAllAccountById(int id)
        {
            var idContext = IdContext.GetUserId(HttpContext);
            var accountsPermission = _accountService.GetAccountById(idContext);
            if (accountsPermission.Result.Role == "Admin")
            {
                var result = _accountService.GetAccountById(id);
                return Ok(result);
            }
            return Unauthorized(new { message = "Unauthorized" });
           
        }

        [HttpPost("create")]
        [Authorize]
        public async Task<ActionResult<AccountResponseViewModel>> CreateAccount(AccountRegisterViewModel create)
        {
            var idContext = IdContext.GetUserId(HttpContext);
            var accountsPermission = _accountService.GetAccountById(idContext);
            if (accountsPermission.Result.Role == "Admin")
            {
                var mapModel = _mapper.Map<AccountDto>(create);
                var createdAccount = await _accountService.Create(mapModel);
                var response = _mapper.Map<AccountResponseViewModel>(createdAccount);
                return Ok(response);
            }
            return Unauthorized(new { message = "Unauthorized" }); 
        }

        [HttpPut]
        [Authorize]
        public async Task<ActionResult<AccountResponseViewModel>> UpdateAccount(UpdateAccountViewModel create)
        {
            var idContext = IdContext.GetUserId(HttpContext);
            var accountsPermission = _accountService.GetAccountById(idContext);
            if (accountsPermission.Result.Role == "Admin")
            {
                var mapModel = _mapper.Map<UpdateAccountDto>(create);
                await _accountService.Update(idContext,mapModel);
             
                return Ok("Updated");
            }
            return Unauthorized(new { message = "Unauthorized" });
        }
        [HttpDelete("{id:int}")]
        [Authorize]
        public  IActionResult DeleteAccount(int id)
        {
            var idContext = IdContext.GetUserId(HttpContext);
            var accountsPermission = _accountService.GetAccountById(idContext);
            if (accountsPermission.Result.Role == "Admin")
            {
                  _accountService.Delete(id);
                return Ok();
            }
            return Unauthorized(new { message = "Unauthorized" });
        }
        private void SetTokenCookie(string token)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(7)
            };
            Response.Cookies.Append("refreshToken", token, cookieOptions);
        }

        private string IpAddress()
        {
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
                return Request.Headers["X-Forwarded-For"];
            else
                return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }
    }
}
