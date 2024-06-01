using BackendService.Application.SignUp.Dtos;
using BackendService.Domain.Entities;
using BackendService.Helper.Model;

namespace BackendService.Application.SignUp.Service
{
    public interface ISignUpService
    {
        Task<ResponseBaseModel<MsUser>> SignUp(SignUpWriteDto input, CancellationToken cancellationToken);
    }
}
