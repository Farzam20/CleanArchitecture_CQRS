using MediatR;

namespace CleanArchitecture.Application.CQRS.ProductFiles.Commands
{
    public class DeleteProductCommand : IRequest<HandlerResponse<bool>>
    {
        public int Id { get; }
        public string CurrentUserId { get; }

        public DeleteProductCommand(int id, string currentUserId)
        {
            Id = id;
            CurrentUserId = currentUserId;
        }
    }
}
