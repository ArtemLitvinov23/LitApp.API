using AutoMapper;
using LitBlog.API.Models;
using LitBlog.BLL.ModelsDto;
using LitBlog.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace LitBlog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;

        public AccountController(IAccountService accountService, IMapper mapper)
        {
            _accountService = accountService;
            _mapper = mapper;
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
            var account = _mapper.Map<AccountDto>(request);

            if (account == null)
                throw new NullReferenceException();

            await _accountService.Register(account, Request.Headers["origin"]);
            return Ok(new {message = "Registration successful" });
        }


        [HttpPost("verify")]
        public async Task<ActionResult> Verify(VerifyRequestViewModel verifyRequest)
        {
            await _accountService.VerifyEmail(verifyRequest.Token);
            return Ok(new {message = "Verification successful, you can now login"});
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
