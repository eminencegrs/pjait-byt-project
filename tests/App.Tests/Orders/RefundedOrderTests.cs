using System.Collections.Immutable;
using App.Addresses;
using App.Orders;
using AutoFixture;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace App.Tests.Orders;

public class RefundedOrderTests
{
    private readonly IFixture fixture = new Fixture();
    
    [Fact]
    public async Task Given_WhenCallArchive_ThenArchivedOrderReturned()
    {
        var address = new Address
        {
            Id = this.fixture.Create<string>(),
            Building = this.fixture.Create<string>(),
            Street = this.fixture.Create<string>(),
            City = this.fixture.Create<string>(),
            Country = this.fixture.Create<string>(),
            Index = this.fixture.Create<string>()
        };
        
        var paymentInfo = new PaymentInfo
        {
            Id = this.fixture.Create<string>(),
            CardNumber = this.fixture.Create<string>(),
            CvvHash = this.fixture.Create<string>(),
            ExpirationDate = this.fixture.Create<DateTime>()
        };
        
        var dateTime = DateTime.UtcNow;
        var order = new RefundedOrder
        {
            Id = this.fixture.Create<string>(),
            CreatedAt = dateTime,
            UpdatedAt = dateTime + TimeSpan.FromTicks(1),
            Address = address,
            PaymentInfo = paymentInfo,
            Items = this.fixture.CreateMany<Product>().ToImmutableDictionary(
                k => k.Id,
                v => new OrderItem { Product = v, Amount = 1 })
        };
        
        var updatedAt = DateTime.UtcNow;

        ArchivedOrder? actualResult = null;
        
        Func<Task> action = async () => actualResult = await order.Archive(); 
        
        using (new AssertionScope())
        {
            await action.Should().NotThrowAsync();
            actualResult.Should().NotBeNull();
            actualResult.Should().BeOfType<ArchivedOrder>();
            actualResult.Should().BeAssignableTo<IOrder>();
            actualResult?.Id.Should().Be(order.Id);
            actualResult?.CreatedAt.Should().Be(order.CreatedAt);
            actualResult?.UpdatedAt.Should().BeAfter(order.UpdatedAt);
            actualResult?.UpdatedAt.Should().BeCloseTo(updatedAt, new TimeSpan(1000000));
            actualResult?.Items.Should().BeEquivalentTo(order.Items);
            actualResult?.Address.Should().BeEquivalentTo(address);
            actualResult?.PaymentInfo.Should().BeEquivalentTo(paymentInfo);
        }
    }
}
