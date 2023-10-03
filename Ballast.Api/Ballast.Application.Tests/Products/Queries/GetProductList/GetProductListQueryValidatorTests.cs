using Ballast.Application.Products.Queries.GetProductList;
using FluentValidation.TestHelper;

namespace Ballast.Application.Tests.Products.Queries.GetProductList;

[TestFixture]
public class GetProductListQueryValidatorTests
{
    private GetProductListQueryValidator _validator;

    [SetUp]
    public void Setup()
    {
        _validator = new GetProductListQueryValidator();
    }
    
    [Test]
    public void Validate_ValidModel_ShouldPass()
    {
        var query = new GetProductListQuery
        {
            FilterName = "Test"
        };
        
        var result = _validator.TestValidate(query);
        result.ShouldNotHaveValidationErrorFor(x => x.FilterName);
    }
    
    [Test]
    public void Validate_EmptyName_ShouldPass()
    {
        var query = new GetProductListQuery
        {
            FilterName = null
        };
        
        var result = _validator.TestValidate(query);
        result.ShouldNotHaveValidationErrorFor(x => x.FilterName);
    }
    
    [Test]
    public void Validate_LessThanTwoCharacters_ShouldFail()
    {
        var query = new GetProductListQuery
        {
            FilterName = "a"
        };
        
        var result = _validator.TestValidate(query);
        result.ShouldHaveValidationErrorFor(x => x.FilterName);
    }
}