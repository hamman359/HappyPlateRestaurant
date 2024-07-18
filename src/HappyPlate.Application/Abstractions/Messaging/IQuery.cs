using HappyPlate.Domain.Shared;

using MediatR;

namespace HappyPlate.Application.Abstractions.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}