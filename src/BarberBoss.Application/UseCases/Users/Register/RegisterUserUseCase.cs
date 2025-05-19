using AutoMapper;
using BarberBoss.Communication.Requests;
using BarberBoss.Communication.Responses;
using BarberBoss.Domain.Repositories;
using BarberBoss.Domain.Repositories.Users;
using BarberBoss.Domain.Security.Cryptography;
using BarberBoss.Domain.Security.Tokens;
using BarberBoss.Exception;
using BarberBoss.Exception.ExceptionsBase;
using FluentValidation.Results;

namespace BarberBoss.Application.UseCases.Users.Register;

public class RegisterUserUseCase : IRegisterUserUseCase
{
    private readonly IMapper _mapper;
    private readonly IPasswordEncrypter _passwordEncrypter;
    private readonly IUserReadOnlyRepository _userReadOnlyRepository;
    private readonly IUserWriteOnlyRepository _userWriteOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAccessTokenGenerator _tokenGenerator;

    public RegisterUserUseCase(
        IMapper mapper, 
        IPasswordEncrypter passwordEncrypter,
        IUserReadOnlyRepository userReadOnlyRepository,
        IUserWriteOnlyRepository userWriteOnlyRepository,
        IUnitOfWork unitOfWork,
        IAccessTokenGenerator tokenGenerator)
    {
        _mapper = mapper;
        _passwordEncrypter = passwordEncrypter;
        _userReadOnlyRepository = userReadOnlyRepository;
        _userWriteOnlyRepository = userWriteOnlyRepository;
        _unitOfWork = unitOfWork;
        _tokenGenerator = tokenGenerator;
    }

    public async Task<ResponseRegisteredUserJson> Execute(RequestRegisterUserJson request)
    {
        await Validate(request);

        var user = _mapper.Map<Domain.Entities.User>(request);
        user.Password = _passwordEncrypter.Encrypt(request.Password);
        user.UserIdentifier = Guid.NewGuid();

        await _userWriteOnlyRepository.Add(user);

        await _unitOfWork.Commit();

        return new ResponseRegisteredUserJson
        {
            UserId = user.Id,
            Token = _tokenGenerator.Generate(user)
        };
    }
    
    private async Task Validate(RequestRegisterUserJson request)
    {
        var result = new RegisterUserValidator().Validate(request);

        var emailExists = await _userReadOnlyRepository.ExistsActiveUserWithEmail(request.Email);

        if (emailExists)
        {
            result.Errors.Add(new ValidationFailure(string.Empty, ResourceErrorMessages.EMAIL_ALREADY_REGISTERED));
        }

        if (result.IsValid == false)
        {
            var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorMessages);
        }
    }
}
