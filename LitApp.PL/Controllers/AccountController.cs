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
    [Route("api/Account")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;
        private readonly ICacheService<AccountResponseDto> _cacheService;

        public AccountController(
            IAccountService accountService,
            IMapper mapper,
            ICacheService<AccountResponseDto> cacheService)
        {
            _accountService = accountService;
            _mapper = mapper;
            _cacheService = cacheService;
        }


        [AllowAnonymous]
        [HttpPost("sign-in")]
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

            var result = await _accountService.RegisterAsync(account, Request.Headers["origin"]);

            if (result != StatusEnum.OK)
                return BadRequest();

            return Ok(new { message = "Registration successful, please check your email for verify instructions" });
        }

        [AllowAnonymous]
        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Verify(VerifyRequestViewModel verifyRequest)
        {
            var result = await _accountService.VerifyEmailAsync(verifyRequest.Token);

            if (result != StatusEnum.OK)
                return BadRequest();

            return Ok(new { message = "Verification successful, you can now login" });
        }

        [AllowAnonymous]
        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordRequestViewModel model)
        {
            var mapModel = _mapper.Map<ForgotPasswordRequestDto>(model);

            var result = await _accountService.ForgotPasswordAsync(mapModel, Request.Headers["origin"]);

            if (result != StatusEnum.OK)
                return BadRequest();

            return Ok(new { message = "Please check your email for password reset instructions" });
        }

        [AllowAnonymous]
        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> ResetPassword(ResetPasswordRequestViewModel model)
        {
            var mapModel = _mapper.Map<ResetPasswordRequestDto>(model);

            var result = await _accountService.ResetPasswordAsync(mapModel);

            if (result != StatusEnum.OK)
                return BadRequest();

            return Ok(new { message = "Password reset successful, you can now login" });
        }

        [Authorize]
        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AuthenticateResponseViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AuthenticateResponseViewModel>> RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];

            var response = await _accountService.RefreshTokenAsync(refreshToken, IpAddress());

            if (response == null)
                return BadRequest();

            SetTokenCookie(response.RefreshToken);

            return Ok(response);
        }

        [Authorize]
        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> RevokeToken(RevokeTokenRequestViewModel model)
        {
            var token = model.Token ?? Request.Cookies["refreshToken"];

            if (string.IsNullOrEmpty(token))
                return BadRequest(new { message = "Token is required" });

            var result = await _accountService.RevokeTokenAsync(token, IpAddress());

            if (result != StatusEnum.OK)
                return BadRequest();

            return Ok(new { message = "Token revoked" });
        }

        [Authorize]
        [HttpGet("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AccountResponseViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<AccountResponseViewModel>>> GetAllAccount()
        {
            var result = await _cacheService.GetList();

            if (result == null)
            {
                result = await _accountService.GetAllAccountsAsync();
                await _cacheService.SetList(result);
            }

            var mapDto = _mapper.Map<List<AccountResponseViewModel>>(result);

            return Ok(mapDto);
        }


        [Authorize]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AccountResponseViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AccountResponseViewModel>> GetAccountById(int id)
        {
            var result = await _cacheService.Get(id);

            if (result == null)
            {
                result = await _accountService.GetAccountByIdAsync(id);
                await _cacheService.Set(result);
            }
            var mapModel = _mapper.Map<AccountResponseViewModel>(result);

            return Ok(mapModel);
        }

        [Authorize]
        [HttpPut("{CurrentUserId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AccountResponseViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AccountResponseViewModel>> UpdateAccount(int CurrentUserId, UpdateAccountViewModel create)
        {
            var mapModel = _mapper.Map<UpdateAccountDto>(create);

            await _accountService.UpdateAccountAsync(CurrentUserId, mapModel);

            return Ok("Updated");
        }

        [Authorize]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteAccount(int id)
        {
            var result = await _accountService.DeleteAccountAsync(id);

            if (result != StatusEnum.OK)
                return BadRequest();

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
