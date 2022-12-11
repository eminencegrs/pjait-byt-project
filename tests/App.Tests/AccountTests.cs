using System.Collections.ObjectModel;
using App.Addresses;
using App.Orders;
using AutoFixture;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace App.Tests;

public class AccountTests
{
    private readonly IFixture fixture = new Fixture();

    [Fact]
    public async Task Given_WhenCallDefaultCtor_ThenAccountCreated()
    {
        Account? actualResult = null;
        
        Action action = () => actualResult = new Account();
        
        using (new AssertionScope())
        {
            action.Should().NotThrow();
            actualResult.Should().NotBeNull();
        }
    }
    
    [Fact]
    public async Task Given_WhenCallCtor_ThenAccountCreated()
    {
        var id = this.fixture.Create<string>();
        var paymentInfos = this.fixture.Create<IReadOnlyDictionary<string, PaymentInfo>>();
        var deliveryAddresses = Enumerable.Empty<IAddress>().ToDictionary(k => k.Id, v => v);
        var orderHistory = Enumerable.Empty<IOrder>().ToDictionary(k => k.Id, v => v);
        var favorites = this.fixture.Create<IReadOnlyDictionary<string, Product>>();
        
        Account? actualResult = null;
        
        Action action = () => actualResult = new Account(
            id,
            paymentInfos,
            deliveryAddresses,
            orderHistory,
            favorites);
        
        using (new AssertionScope())
        {
            action.Should().NotThrow();
            actualResult.Should().NotBeNull();
        }
    }
    
    [Fact(Skip = "To Be Implemented")]
    public Task<bool> Given_WhenCallCreate_Then()
    {
        throw new NotImplementedException();
    }
    
    [Fact(Skip = "To Be Implemented")]
    public Task<bool> Given_WhenCallDelete_Then()
    {
        throw new NotImplementedException();
    }
    
    [Fact]
    public async Task Given_WhenCallAddPaymentMethod_ThenResultAsExpected()
    {
        var account = new Account();
        var paymentInfo = this.fixture.Create<PaymentInfo>();
        bool actualResult = false;
        
        Func<Task> action = async () => actualResult = await account.AddPaymentMethod(paymentInfo);
        
        using (new AssertionScope())
        {
            await action.Should().NotThrowAsync();
            actualResult.Should().BeTrue();
        }
    }

    [Fact]
    public async Task GivenValidPaymentInfo_WhenCallRemovePaymentMethod_ThenResultAsExpected()
    {
        var account = new Account();
        var paymentInfo = this.fixture.Create<PaymentInfo>();
        await account.AddPaymentMethod(paymentInfo);
        bool actualResult = false;
        
        Func<Task> action = async () => actualResult = await account.RemovePaymentMethod(paymentInfo);
        
        using (new AssertionScope())
        {
            await action.Should().NotThrowAsync();
            actualResult.Should().BeTrue();
        }
    }
    
    [Fact]
    public async Task GivenInvalidPaymentInfo_WhenCallRemovePaymentMethod_ThenResultAsExpected()
    {
        var account = new Account();
        var paymentInfo = this.fixture.Create<PaymentInfo>();
        bool actualResult = false;
        
        Func<Task> action = async () => actualResult = await account.RemovePaymentMethod(paymentInfo);
        
        using (new AssertionScope())
        {
            await action.Should().NotThrowAsync();
            actualResult.Should().BeFalse();
        }
    }
    
    [Fact]
    public async Task Given_WhenCallAddDeliveryAddress_ThenResultAsExpected()
    {
        var account = new Account();
        var address = this.fixture.Create<Address>();
        bool actualResult = false;
        
        Func<Task> action = async () => actualResult = await account.AddDeliveryAddress(address);
        
        using (new AssertionScope())
        {
            await action.Should().NotThrowAsync();
            actualResult.Should().BeTrue();
        }
    }
    
    [Fact]
    public async Task GivenValidAddress_WhenCallRemoveDeliveryAddress_ThenResultAsExpected()
    {
        var account = new Account();
        var address = this.fixture.Create<Address>();
        await account.AddDeliveryAddress(address);
        bool actualResult = false;
        
        Func<Task> action = async () => actualResult = await account.RemoveDeliveryAddress(address);
        
        using (new AssertionScope())
        {
            await action.Should().NotThrowAsync();
            actualResult.Should().BeTrue();
        }
    }
    
    [Fact]
    public async Task GivenInvalidAddress_WhenCallRemoveDeliveryAddress_ThenResultAsExpected()
    {
        var account = new Account();
        var address = this.fixture.Create<Address>();
        bool actualResult = false;
        
        Func<Task> action = async () => actualResult = await account.RemoveDeliveryAddress(address);
        
        using (new AssertionScope())
        {
            await action.Should().NotThrowAsync();
            actualResult.Should().BeFalse();
        }
    }
    
    [Fact]
    public async Task GivenValidAddress_WhenCallUpdateDeliveryAddress_ThenAddressUpdated()
    {
        var account = new Account();
        var address = this.fixture.Create<Address>();
        var newAddress = this.fixture
            .Build<Address>()
            .With(x => x.Id, address.Id)
            .Create();
        
        await account.AddDeliveryAddress(address);
        bool actualResult = false;
        
        Func<Task> action = async () => actualResult = await account.UpdateDeliveryAddress(newAddress);
        
        using (new AssertionScope())
        {
            await action.Should().NotThrowAsync();
            actualResult.Should().BeTrue();
        }
    }
    
    [Fact]
    public async Task GivenValidAddress_WhenCallUpdateDeliveryAddress_ThenAddressAdded()
    {
        var account = new Account();
        var address = this.fixture.Create<Address>();
        bool actualResult = false;
        
        Func<Task> action = async () => actualResult = await account.UpdateDeliveryAddress(address);
        
        using (new AssertionScope())
        {
            await action.Should().NotThrowAsync();
            actualResult.Should().BeTrue();
        }
    }
}
