using FluentValidation;
using MediaExpertWebApi.Database;

namespace MediaExpertWebApi.Models;

public class Product : Entry
{
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
}

public class ProductValidator : AbstractValidator<Product>
{
    public ProductValidator()
    {
        RuleFor(product => product.Name).NotEmpty().MaximumLength(255);
        RuleFor(product => product.Code).NotEmpty().MaximumLength(255);
        RuleFor(product => product.Price).GreaterThan(0);
    }
}

public class ProductAddValidator : ProductValidator
{
    public ProductAddValidator(DatabaseInMemory databaseInMemory): base()
    {

        RuleFor(product => product).Must(z => databaseInMemory.Products.Get().All(x => x.Code != z.Code))
            .WithMessage("Kod musi być unikalny.");
    }
}

public class ProductUpdateValidator : ProductValidator
{
    public ProductUpdateValidator(DatabaseInMemory databaseInMemory, int id): base()
    {

        RuleFor(product => product).Must(z => ProductsExcept(databaseInMemory, id).All(x => x.Code != z.Code))
            .WithMessage("Kod musi być unikalny.");

        IEnumerable<Product> ProductsExcept(DatabaseInMemory databaseInMemory, int id)
        {
            var list = databaseInMemory.Products.Get();
            list.Remove(databaseInMemory.Products.Get(id));
            return list;
        }
    }
}
