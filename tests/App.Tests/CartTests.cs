using AutoFixture;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace App.Tests;

public class CartTests
{
    private readonly IFixture fixture = new Fixture();

    [Fact]
    public async Task Given_WhenCallAddItem_ThenItemAdded()
    {
        var product = this.fixture.Create<Product>();
        var cart = new Cart();
        var actualResult = false;
        Func<Task> action = async () => actualResult = await cart.AddItem(product);
        
        using (new AssertionScope())
        {
            await action.Should().NotThrowAsync();
            actualResult.Should().BeTrue();
            cart.Items.Should().NotBeEmpty();
            cart.Items.Count.Should().Be(1);
            cart.Items.Single(x => x.Key == product.Id).Should().NotBeNull();
        }
    }
    
    [Fact]
    public async Task Given_WhenCallRemoveItem_ThenItemRemoved()
    {
        var product = this.fixture.Create<Product>();
        var cart = new Cart();
        await cart.AddItem(product);
        var actualResult = false;
        Func<Task> action = async () => actualResult = await cart.RemoveItem(product.Id);
        
        using (new AssertionScope())
        {
            await action.Should().NotThrowAsync();
            actualResult.Should().BeTrue();
            cart.Items.Should().BeEmpty();
        }
    }

    [Fact]
    public async Task Given_WhenCallChangeAmount_ThenAmountChanged()
    {
        var product = this.fixture.Create<Product>();
        var cart = new Cart();
        await cart.AddItem(product);
        var actualResult = false;
        Func<Task> action = async () => actualResult = await cart.ChangeAmount(product.Id, 10);
        
        using (new AssertionScope())
        {
            await action.Should().NotThrowAsync();
            actualResult.Should().BeTrue();
            cart.Items.Should().NotBeEmpty();
            cart.Items.Count.Should().Be(1);
            var item = cart.Items.Single(x => x.Key == product.Id);
            item.Should().NotBeNull();
            item.Value.Product.Should().BeEquivalentTo(product);
            item.Value.Amount.Should().Be(11);
        }
    }
    
    [Fact]
    public async Task GivenEmptyCart_WhenCallChangeAmount_ThenResultAsExpected()
    {
        var product = this.fixture.Create<Product>();
        var cart = new Cart();
        var actualResult = false;
        Func<Task> action = async () => actualResult = await cart.ChangeAmount(product.Id, 10);
        
        using (new AssertionScope())
        {
            await action.Should().NotThrowAsync();
            actualResult.Should().BeFalse();
            cart.Items.Should().BeEmpty();
        }
    }
}
