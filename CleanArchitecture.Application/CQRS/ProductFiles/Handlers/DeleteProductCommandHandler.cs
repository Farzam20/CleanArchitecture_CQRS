using CleanArchitecture.Application.CQRS.ProductFiles.Commands;
using CleanArchitecture.Application.Services.Interfaces;
using MediatR;

namespace CleanArchitecture.Application.CQRS.ProductFiles.Handlers
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, HandlerResponse<bool>>
    {
        private readonly IProductService _productService;

        public DeleteProductCommandHandler(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<HandlerResponse<bool>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _productService.GetByIdAsync(cancellationToken, request.Id);

            if (product == null)
                return new(false, "محصول موردنظر یافت نشد", false);

            if (product.CreatedByUserId != request.CurrentUserId)
                return new(false, "امکان حذف این محصول توسط شما وجود ندارد", false);

            await _productService.DeleteAsync(product, cancellationToken);
            return true;
        }
    }
}
