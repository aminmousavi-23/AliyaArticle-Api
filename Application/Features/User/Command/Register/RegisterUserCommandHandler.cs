using Application.Abstractions.Persistence;
using Application.Common.Helpers;
using Application.Common.Resources;
using Application.Common.Validation;
using Application.Models.Responses;
using AutoMapper;
using MediatR;

namespace Application.Features.User.Command.Register;

public class RegisterUserCommandHandler(
    IMapper mapper,
    IRequestValidator requestValidator,
    IUserRepository userRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<RegisterUserCommand, BaseResponse<RegisterUserCommandResponse>>
{
    public async Task<BaseResponse<RegisterUserCommandResponse>> Handle(RegisterUserCommand request,
        CancellationToken cancellationToken)
    {
        await requestValidator.ValidateAsync(request);
        
        var exists = 
            await userRepository.IsExistsAsync(request.PhoneNumber, request.Email, cancellationToken);
        if (exists)
            return ResponseFactory.Conflict<RegisterUserCommandResponse>(Messages.User.AlreadyRegistered);
        
        var newUser = mapper.Map<Domain.Entities.User>(request);
        newUser.HashedPassword = PasswordHelper.HashPassword(request.Password);

        await userRepository.AddAsync(newUser, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        var response = mapper.Map<RegisterUserCommandResponse>(newUser);
        
        return ResponseFactory.Created(response);
    }
}