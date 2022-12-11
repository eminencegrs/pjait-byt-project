using App.Addresses;
using App.Orders;
using AutoFixture;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace App.Tests.Orders;

public class DraftOrderTests
{
    private readonly IFixture fixture = new Fixture();

    [Fact]
    public async Task Given_WhenCallSubmit_ThenSubmittedOrderReturned()
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
        
        var cart = this.fixture.Create<Cart>();
        var draftOrder = await OrderBase.Create(cart);
        var updatedAt = DateTime.UtcNow;

        SubmittedOrder? actualResult = null;
        
        Func<Task> action = async () => actualResult = await draftOrder.Submit(address, paymentInfo); 
        
        using (new AssertionScope())
        {
            await action.Should().NotThrowAsync();
            actualResult.Should().NotBeNull();
            actualResult.Should().BeOfType<SubmittedOrder>();
            actualResult.Should().BeAssignableTo<IOrder>();
            actualResult?.Id.Should().Be(draftOrder.Id);
            actualResult?.CreatedAt.Should().Be(draftOrder.CreatedAt);
            actualResult?.UpdatedAt.Should().BeAfter(draftOrder.UpdatedAt);
            actualResult?.UpdatedAt.Should().BeCloseTo(updatedAt, new TimeSpan(1000000));
            actualResult?.Items.Should().BeEquivalentTo(draftOrder.Items);
            actualResult?.Address.Should().BeEquivalentTo(address);
            actualResult?.PaymentInfo.Should().BeEquivalentTo(paymentInfo);
        }
    }
}
