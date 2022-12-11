using System.Runtime.CompilerServices;
using AutoFixture;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace App.Tests;

public class CustomerTests
{
    private readonly IFixture fixture = new Fixture();

    [Fact]
    public void Given_WhenCallCtor_ThenCustomerCreated()
    {
        var firstName = this.fixture.Create<string>();
        var lastName = this.fixture.Create<string>();
        var email = this.fixture.Create<string>();
        var passwordHash = this.fixture.Create<string>();

        Customer expectedResult = new Customer(firstName, lastName, email, passwordHash);

        Customer? actualResult = null;

        Action action = () => actualResult = new Customer(firstName, lastName, email, passwordHash);

        using (new AssertionScope())
        {
            action.Should().NotThrow();
            actualResult.Should().NotBeNull();
            actualResult.Should().BeEquivalentTo(expectedResult);
        }
    }
    
    [Fact]
    public void GivenAccounts_WhenCallCtor_ThenCustomerCreated()
    {
        var firstName = this.fixture.Create<string>();
        var lastName = this.fixture.Create<string>();
        var email = this.fixture.Create<string>();
        var passwordHash = this.fixture.Create<string>();
        var accounts = this.fixture.CreateMany<Account>().ToDictionary(k => k.Id, v => v);

        Customer expectedResult = new Customer(firstName, lastName, email, passwordHash, accounts);

        Customer? actualResult = null;

        Action action =
            () => actualResult =
                new Customer(firstName, lastName, email, passwordHash, accounts);

        using (new AssertionScope())
        {
            action.Should().NotThrow();
            actualResult.Should().NotBeNull();
            actualResult.Should().BeEquivalentTo(expectedResult);
        }
    }
    
    [Fact]
    public void Given_WhenCallLogin_ThenLoginSucceeded()
    {
        var firstName = this.fixture.Create<string>();
        var lastName = this.fixture.Create<string>();
        var email = this.fixture.Create<string>();
        var passwordHash = this.fixture.Create<string>();

        var customer = new Customer(firstName, lastName, email, passwordHash);

        var actualResult = false;

        Func<Task> action = async () => actualResult = await customer.Login(email, passwordHash);

        using (new AssertionScope())
        {
            action.Should().NotThrowAsync();
            actualResult.Should().BeTrue();
        }
    }
    
    [Fact]
    public void Given_WhenCallLogin_ThenLogoutSucceeded()
    {
        var firstName = this.fixture.Create<string>();
        var lastName = this.fixture.Create<string>();
        var email = this.fixture.Create<string>();
        var passwordHash = this.fixture.Create<string>();

        var customer = new Customer(firstName, lastName, email, passwordHash);

        var actualResult = false;

        Func<Task> action = async () => actualResult = await customer.Logout();

        using (new AssertionScope())
        {
            action.Should().NotThrowAsync();
            actualResult.Should().BeTrue();
        }
    }
}
