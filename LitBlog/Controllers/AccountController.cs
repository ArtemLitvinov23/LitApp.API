using AutoMapper;
using LitBlog.API.Helpers;
using LitBlog.API.Models;
using LitBlog.BLL.ModelsDto;
using LitBlog.BLL.Services.Interfaces;
using LitChat.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LitBlog.API.Controllers
{
    [ApiController]
    [Route("api/Account")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;

        public AccountController(
            IAccountService accountService,
            IMapper mapper)
        {
            _accountService = accountService;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpPost("sign-in")]
        public async Task<ActionResult<AuthenticateResponseViewModel>> Authenticate(AuthenticateRequestViewModel request)
        {
            var requestDto = _mapper.Map<AuthenticateRequestDto>(request);
            var response = await _accountService.AuthenticateAsync(requestDto, IpAddress());
            SetTokenCookie(response.RefreshToken);
            var mapper = _mapper.Map<AuthenticateResponseViewModel>(response);
            return Ok(mapper);
        }

        [AllowAnonymous]
        [HttpPost("sign-up")]
        public async Task<ActionResult> SignUp(AccountRegisterViewModel request)
        {
            var account = _mapper.Map<AccountDto>(request);
            await _accountService.RegisterAsync(account, Request.Headers["origin"]);
            return Ok(new { message = "Registration successful, please check your email for verify instructions" });
        }

        [AllowAnonymous]
        [HttpPost("verify")]
        public async Task<ActionResult> Verify([FromQuery]VerifyRequestViewModel verifyRequest)
        {
            await _accountService.VerifyEmailAsync(verifyRequest.Token);
            return Ok(new { message = "Verification successful, you can now login" });
        }

        [AllowAnonymous]
        [HttpPost("forgot-password")]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordRequestViewModel model)
        {
            var mapModel = _mapper.Map<ForgotPasswordRequestDto>(model);
            await _accountService.ForgotPasswordAsync(mapModel, Request.Headers["origin"]);
            return Ok(new { message = "Please check your email for password reset instructions" });
        }

        [AllowAnonymous]
        [HttpPost("reset-password")]
        public async Task<ActionResult> ResetPassword(ResetPasswordRequestViewModel model)
        {
            var mapModel = _mapper.Map<ResetPasswordRequestDto>(model);
            await _accountService.ResetPasswordAsync(mapModel);
            return Ok(new { message = "Password reset successful, you can now login" });
        }

        [Authorize]
        [HttpPost("refresh-token")]
        public async Task<ActionResult<AuthenticateResponseViewModel>> RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            var response = await _accountService.RefreshTokenAsync(refreshToken, IpAddress());
            SetTokenCookie(response.RefreshToken);
            return Ok(response);
        }

        [Authorize]
        [HttpPost("revoke-token")]
        public IActionResult RevokeToken(RevokeTokenRequestViewModel model)
        {
            var token = model.Token ?? Request.Cookies["refreshToken"];
            if (string.IsNullOrEmpty(token))
                return BadRequest(new { message = "Token is required" });

            _accountService.RevokeTokenAsync(token, IpAddress());
            return Ok(new { message = "Token revoked" });
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<AccountResponseViewModel>>> GetAllAccount()
        {
            var result = await _accountService.GetAllAccountsAsync();
            var mapDto = _mapper.Map<List<AccountResponseDto>>(result);
            return Ok(mapDto);
        }

        [Authorize]
        [HttpGet("get-users")]
        public async Task<ActionResult<List<UserResponseViewModel>>> GetAllUsers()
        {
            var result = await _accountService.GetAllUsersAsync();
            return Ok(_mapper.Map<List<UserResponseViewModel>>(result));
        }

        [Authorize]
        [HttpGet("get-users/{id}")]
        public async Task<ActionResult<UserResponseViewModel>> GetUserById(int id)
        {
            var result = await _accountService.GetUserByIdAsync(id);
            return Ok(_mapper.Map<UserResponseViewModel>(result));
        }

        [Authorize]
        [HttpGet("{id:int}")]
        public async Task<ActionResult<List<AccountResponseViewModel>>> GetAccountById(int id)
        {
            var result = await _accountService.GetAccountByIdAsync(id);
            return Ok(result);
        }

        [Authorize]
        [HttpPost("create")]
        public async Task<ActionResult<AccountResponseViewModel>> CreateAccount(AccountViewModel create)
        {
            var mapModel = _mapper.Map<AccountDto>(create);
            var createdAccount = await _accountService.CreateAccountAsync(mapModel);
            var response = _mapper.Map<AccountResponseViewModel>(createdAccount);
            return Ok(response);
        }

        [Authorize]
        [HttpPut]
        public async Task<ActionResult<AccountResponseViewModel>> UpdateAccount(int CurrentUserId,UpdateAccountViewModel create)
        {
            var mapModel = _mapper.Map<UpdateAccountDto>(create);
            await _accountService.UpdateAccountAsync(CurrentUserId,mapModel);
            return Ok("Updated");
        }

        [Authorize]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAccount(int id)
        {
            await _accountService.DeleteAccountAsync(id);
            return Ok();
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
