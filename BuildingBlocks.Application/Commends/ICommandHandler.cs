using MediatR;

namespace BuildingBlocks.Application.Commends;

public interface ICommandHandler<in TCommand, TResult> :
    IRequestHandler<TCommand, TResult>
    where TCommand : ICommand<TResult>
{
}