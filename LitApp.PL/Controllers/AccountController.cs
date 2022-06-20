using AutoMapper;
using LitApp.BLL.ModelsDto;
using LitApp.BLL.Services.Interfaces;
using LitApp.PL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LitChat.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/Account")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;
        //private readonly ICacheService<AccountResponseDto> _cacheService;

        public AccountController(
            IAccountService accountService,
            IMapper mapper)
            //ICacheService<AccountResponseDto> cacheService)
        {
            _accountService = accountService;
            _mapper = mapper;
            //_cacheService = cacheService;
        }


        [AllowAnonymous]
        [HttpPost("SignIn")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AuthenticateResponseViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AuthenticateResponseViewModel>> Authenticate(AuthenticateRequestViewModel request)
        {
            var requestDto = _mapper.Map<AuthenticateRequestDto>(request);

            var response = await _accountService.AuthenticateAsync(requestDto, IpAddress());

            if (response == null)
                return BadRequest();

            SetTokenCookie(response.RefreshToken);

            var mapper = _mapper.Map<AuthenticateResponseViewModel>(response);

            return Ok(mapper);
        }

        [AllowAnonymous]
        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> SignUp(AccountRegisterViewModel request)
        {
            var account = _mapper.Map<AccountDto>(request);

            await _accountService.RegisterAsync(account, Request.Headers["origin"]);

            return Ok(new { message = "Registration successful, please check your email for verify instructions" });
        }

        [AllowAnonymous]
        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Verify(VerifyRequestViewModel verifyRequest)
        {
            await _accountService.VerifyEmailAsync(verifyRequest.Token);

            return Ok(new { message = "Verification successful, you can now login" });
        }

        [AllowAnonymous]
        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordRequestViewModel model)
        {
            var mapModel = _mapper.Map<ForgotPasswordRequestDto>(model);

            await _accountService.ForgotPasswordAsync(mapModel, Request.Headers["origin"]);

            return Ok(new { message = "Please check your email for password reset instructions" });
        }

        [AllowAnonymous]
        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> ResetPassword(ResetPasswordRequestViewModel model)
        {
            var mapModel = _mapper.Map<ResetPasswordRequestDto>(model);

            await _accountService.ResetPasswordAsync(mapModel);

            return Ok(new { message = "Password reset successful, you can now login" });
        }

        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AuthenticateResponseViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AuthenticateResponseViewModel>> RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];

            var response = await _accountService.RefreshTokenAsync(refreshToken, IpAddress());

            if (response is null)
                return BadRequest();

            SetTokenCookie(response.RefreshToken);

            return Ok(response);
        }

        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RevokeToken(RevokeTokenRequestViewModel model)
        {
            var token = model.Token ?? Request.Cookies["refreshToken"];

            if (string.IsNullOrEmpty(token))
                return BadRequest(new { message = "Token is required" });

            await _accountService.RevokeTokenAsync(token, IpAddress());

            return Ok(new { message = "Token revoked" });
        }

        [Authorize]
        [HttpGet("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<AccountResponseViewModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<AccountResponseViewModel>>> GetAllAccount()
        {
            //var result = await _cacheService.GetList();  ~~~REDIS~~~~

            //if (result is null)
            //{
            //    result = await _accountService.GetAllAccountsAsync();
            //    await _cacheService.SetList(result);
            //}

            var result = await _accountService.GetAllAccountsAsync();

            var mapDto = _mapper.Map<List<AccountResponseViewModel>>(result);

            return Ok(mapDto);
        }


        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AccountResponseViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AccountResponseViewModel>> GetAccountById(int id)
        {
            //var result = await _cacheService.Get(id); ~~~Redis~~~

            //if (result == null)
            //{
            //    result = await _accountService.GetAccountByIdAsync(id);
            //    await _cacheService.Set(result);
            //}

            var result = await _accountService.GetAccountByIdAsync(id);
            var mapModel = _mapper.Map<AccountResponseViewModel>(result);

            return Ok(mapModel);
        }

        [HttpPut("{CurrentUserId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AccountResponseViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AccountResponseViewModel>> UpdateAccount(UpdateAccountViewModel create)
        {
            var mapModel = _mapper.Map<UpdateAccountDto>(create);

            await _accountService.UpdateAccountAsync(mapModel);

            return Ok("Updated");
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
