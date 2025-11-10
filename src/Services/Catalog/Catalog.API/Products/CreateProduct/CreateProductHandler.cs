using Marten;
using MediatR;

namespace Catalog.API.Products.CreateProduct
{
    public record class CreateProductCommand(string Name, List<string> Category, string Description, string ImageFile, decimal Price)
        :IRequest<CreateProductResult>;
    public record class CreateProductResult(Guid Id);
    internal class CreateProductCommandHandler(IDocumentSession session) : IRequestHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            // create a new product
            var product = new Models.Products
            {
                Name = command.Name,
                Category = command.Category,
                Description = command.Description,
                ImageFile = command.ImageFile,
                Price = command.Price
            };

            // store the product in the database
            session.Store(product);
            await session.SaveChangesAsync(cancellationToken);

            // return the result
            return new CreateProductResult(product.Id);
        }
    }
}
