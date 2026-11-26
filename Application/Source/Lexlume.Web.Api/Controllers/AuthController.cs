using Lexlume.Application.BusinessLogic.Users.Commands;
using Lexlume.Application.Dtos.Users;
using Lexlume.Domain.Common;
using Lexlume.Web.Api.Controllers._Base;
using LexLume.Api.ReinforcedTypings.Generator;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Lexlume.Web.Api.Controllers;

public class AuthController(IMediator mediator) : BaseController(mediator)
{
    [HttpPost]
    [AngularMethod(typeof(BaseResponseWrapper))]
    public async Task<IActionResult> Registration([FromBody] RegistrationDto data)
    {
        var result = await Mediator.Send(new RegistrationCommand(data));

        // Check whether registration succeeded.
        // Use the appropriate success/error property from your wrapper.

        var user = result.Data;

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Name, user.Username),
            new(ClaimTypes.Email, user.Email)
        };

        claims.AddRange(new Claim(ClaimTypes.Role, user.Role));

        var identity = new ClaimsIdentity(
            claims,
            CookieAuthenticationDefaults.AuthenticationScheme);

        var principal = new ClaimsPrincipal(identity);

        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            principal,
            new AuthenticationProperties
            {
                IsPersistent = true,
                AllowRefresh = true
            });

        return Result(new());
    }
}