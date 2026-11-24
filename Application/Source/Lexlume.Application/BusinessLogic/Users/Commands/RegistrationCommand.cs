using Lexlume.Application.Dtos.Users;
using Lexlume.Application.Identity.Interfaces;
using Lexlume.Common.Extensions;
using Lexlume.Common.Resources;
using Lexlume.Domain.Common;
using Lexlume.Domain.Entities.Application;
using Lexlume.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Lexlume.Application.BusinessLogic.Users.Commands;

public class RegistrationCommand(RegistrationDto user) : IRequest<ResponseWrapper<AuthenticatedUserDto>>
{
    public RegistrationDto User { get; set; } = user;

    public class RegistrationCommandHandler(IDatabaseContext db, IUserManager userManager)
        : IRequestHandler<RegistrationCommand, ResponseWrapper<AuthenticatedUserDto>>
    {
        public async Task<ResponseWrapper<AuthenticatedUserDto>> Handle(RegistrationCommand request, CancellationToken cancellationToken)
        {
            if (
                await db.Users
                .IgnoreQueryFilters()
                .AnyAsync(_ => _.Username == request.User.Username || _.Email == request.User.Email, cancellationToken)
            )
                return new(new Error(nameof(User), ResourceValidation.In_Use.FormatWith("Email or Username")));

            var model = new User();

            request.User.ToModel(model, userManager);

            db.Users.Add(model);

            await db.SaveChangesAsync(cancellationToken);

            return new(new AuthenticatedUserDto
            {
                Id = model.Id,
                Username = model.Username,
                Email = model.Email,
                Role = model.SystemRoleId.GetDescription(),
            });
        }
    }
}