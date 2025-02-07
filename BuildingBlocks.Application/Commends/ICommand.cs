using MediatR;

namespace BuildingBlocks.Application.Commends;

public interface ICommand<out TResult> : IRequest<TResult>
{
}
