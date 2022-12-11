using System.Collections;
using AutoFixture;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace App.Tests;

public class ProductTests
{
    private readonly IFixture fixture = new Fixture();

    [Fact]
    public async Task Given_WhenCallCreate_ThenProductCreated()
    {
        var name = this.fixture.Create<string>();
        var description = this.fixture.Create<string>();
        var price = this.fixture.Create<double>();
        var category = this.fixture.Create<Category>();

        var actualResult = await Product.Create(name, description, price, category);

        using (new AssertionScope())
        {
            actualResult.Should().NotBeNull();
            actualResult.Id.Should().NotBeNullOrWhiteSpace();
            actualResult.Name.Should().NotBeNullOrWhiteSpace();
            actualResult.Name.Should().Be(name);
            actualResult.Description.Should().NotBeNullOrWhiteSpace();
            actualResult.Description.Should().Be(description);
            actualResult.Price.Should().Be(price);
            actualResult.Category.Should().Be(category);
            
        }
    }
    
    [Fact]
    public async Task Given_WhenCallCreate_ThenProductWithoutCategoryCreated()
    {
        var name = this.fixture.Create<string>();
        var description = this.fixture.Create<string>();
        var price = this.fixture.Create<double>();

        var actualResult = await Product.Create(name, description, price);

        using (new AssertionScope())
        {
            actualResult.Should().NotBeNull();
            actualResult.Id.Should().NotBeNullOrWhiteSpace();
            actualResult.Name.Should().NotBeNullOrWhiteSpace();
            actualResult.Name.Should().Be(name);
            actualResult.Description.Should().NotBeNullOrWhiteSpace();
            actualResult.Description.Should().Be(description);
            actualResult.Price.Should().Be(price);
            actualResult.Category.Should().BeNull();

        }
    }
    
    [Theory]
    [ClassData(typeof(TestData))]
    public async Task Given_WhenCallUpdate_ThenProductUpdated(
        string? name,
        string? description,
        double? price,
        Category? category)
    {
        var product = await Product.Create(
            this.fixture.Create<string>(),
            this.fixture.Create<string>(),
            this.fixture.Create<double>(),
            this.fixture.Create<Category>());

        var updatedProduct = await product.Update(name, description, price, category);

        using (new AssertionScope())
        {
            updatedProduct.Should().NotBeNull();
            updatedProduct.Should().NotBeSameAs(product);
            updatedProduct.Id.Should().Be(product.Id);
            updatedProduct.Name.Should().Be(name ?? product.Name);
            updatedProduct.Description.Should().Be(description ?? product.Description);
            updatedProduct.Price.Should().Be(price ?? product.Price);
            updatedProduct.Category.Should().Be(category ?? product.Category);
        }
    }
    
    [Fact]
    public async Task Given_WhenCallDelete_ThenProductDeleted()
    {
        var product = await Product.Create(
            this.fixture.Create<string>(),
            this.fixture.Create<string>(),
            this.fixture.Create<double>(),
            this.fixture.Create<Category>());

        var actualResult = false;
        Func<Task> action = async () => actualResult = await product.Delete();

        using (new AssertionScope())
        {
            await action.Should().NotThrowAsync();
            actualResult.Should().BeTrue();
        }
    }
    
    [Fact]
    public async Task GivenCart_WhenCallAddToCart_ThenProductAddedToCart()
    {
        var product = await Product.Create(
            this.fixture.Create<string>(),
            this.fixture.Create<string>(),
            this.fixture.Create<double>(),
            this.fixture.Create<Category>());

        var cart = new Cart();

        var actualResult = false;
        Func<Task> action = async () => actualResult = await product.AddTo(cart);

        using (new AssertionScope())
        {
            await action.Should().NotThrowAsync();
            actualResult.Should().BeTrue();
        }
    }
    
    [Fact]
    public async Task GivenNullCart_WhenCallAddToCart_ThenExceptionThrown()
    {
        var product = await Product.Create(
            this.fixture.Create<string>(),
            this.fixture.Create<string>(),
            this.fixture.Create<double>(),
            this.fixture.Create<Category>());

        Cart? cart = null;

        Func<Task> action = () => product.AddTo(cart);

        using (new AssertionScope())
        {
            await action.Should().ThrowAsync<ArgumentNullException>();
        }
    }
    
    [Fact]
    public async Task GivenCart_WhenCallRemoveFromCart_ThenProductRemovedFromCart()
    {
        var product = await Product.Create(
            this.fixture.Create<string>(),
            this.fixture.Create<string>(),
            this.fixture.Create<double>(),
            this.fixture.Create<Category>());

        var cart = new Cart();

        var actualResult = false;
        Func<Task> action = async () => actualResult = await product.RemoveFrom(cart);

        using (new AssertionScope())
        {
            await action.Should().NotThrowAsync();
            actualResult.Should().BeTrue();
        }
    }
    
    [Fact]
    public async Task GivenNullCart_WhenCallRemoveFromCart_ThenExceptionThrown()
    {
        var product = await Product.Create(
            this.fixture.Create<string>(),
            this.fixture.Create<string>(),
            this.fixture.Create<double>(),
            this.fixture.Create<Category>());

        Cart? cart = null;

        Func<Task> action = () => product.RemoveFrom(cart);

        using (new AssertionScope())
        {
            await action.Should().ThrowAsync<ArgumentNullException>();
        }
    }
    
    [Fact]
    public async Task Given_WhenCallGetHashCode_ThenResultAsExpected()
    {
        var product = await Product.Create(
            this.fixture.Create<string>(),
            this.fixture.Create<string>(),
            this.fixture.Create<double>(),
            this.fixture.Create<Category>());

        var productHashCode = product.GetHashCode();

        var firstUpdated = await product.Update("new name");
        var firstHashCode = firstUpdated.GetHashCode();
        var secondUpdated = await product.Update("new name");
        var secondHashCode = secondUpdated.GetHashCode();

        using (new AssertionScope())
        {
            firstHashCode.Should().Be(secondHashCode);
            firstHashCode.Should().NotBe(productHashCode);
            secondHashCode.Should().NotBe(productHashCode);
        }
    }
    
    private class TestData : IEnumerable<object[]>
    {
        private readonly IFixture fixture = new Fixture();
        
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { "new name", null, null, null };
            yield return new object[] { null, "new description", null, null };
            yield return new object[] { null, null, 100.0, null };
            yield return new object[] { null, null, null, this.fixture.Create<Category>() };
            yield return new object[] { "new name", "new description", 200.0, this.fixture.Create<Category>() };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
