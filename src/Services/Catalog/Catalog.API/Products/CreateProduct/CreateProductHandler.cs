using Catalog.API.CQRS;
using FluentValidation;
using Marten;


namespace Catalog.API.Products.CreateProduct
{
    public record class CreateProductCommand(string Name, List<string> Category, string Description, string ImageFile, decimal Price)
        :ICommand<CreateProductResult>;
    public record class CreateProductResult(Guid Id);

    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(command => command.Name)
                .NotEmpty().WithMessage("Name is required")
                .Length(2, 150).WithMessage("Name must be between 2 and 150 characters");
            RuleFor(command => command.Category).NotEmpty().WithMessage("Category is required");

            RuleFor(command => command.Price)
                .GreaterThan(0).WithMessage("Price must be greater than 0");
        }
    }
    internal class CreateProductCommandHandler(IDocumentSession session, ILogger<CreateProductCommandHandler> logger  ) : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            logger.LogInformation("Creating a new product with name: {ProductName}", command.Name);
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
