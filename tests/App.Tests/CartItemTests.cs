using AutoFixture;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace App.Tests;

public class CartItemTests
{
    private readonly IFixture fixture = new Fixture();

    [Fact]
    public async Task Given_WhenCallCreate_ThenItemCreated()
    {
        var product = this.fixture.Create<Product>();
        var amount = this.fixture.Create<int>();
        
        CartItem? actualResult = null;
        Func<Task> action = async () => actualResult = await CartItem.Create(product, amount);
        
        using (new AssertionScope())
        {
            await action.Should().NotThrowAsync();
            actualResult.Should().NotBeNull();
            actualResult.Id.Should().BeEquivalentTo(product.Id);
            actualResult.Product.Should().BeEquivalentTo(product);
            actualResult.Amount.Should().Be(amount);
        }
    }
    
    [Fact]
    public async Task GivenNullProduct_WhenCallCreate_ThenExceptionThrown()
    {
        Product? product = null;
        var amount = this.fixture.Create<int>();
        
        Func<Task> action = async () => await CartItem.Create(product, amount);
        
        using (new AssertionScope())
        {
            await action.Should().ThrowAsync<ArgumentNullException>();
        }
    }
    
    [Fact]
    public async Task GivenInvalidAmount_WhenCallCreate_ThenExceptionThrown()
    {
        var product = this.fixture.Create<Product>();
        var amount = 0;

        Func<Task> action = async () => await CartItem.Create(product, amount);
        
        using (new AssertionScope())
        {
            await action.Should().ThrowAsync<ArgumentException>();
        }
    }
}
