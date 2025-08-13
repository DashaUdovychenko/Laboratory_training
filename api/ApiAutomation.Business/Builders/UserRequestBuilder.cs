using ApiAutomation.Business.Models;

namespace ApiAutomation.Business.Builders;

public class UserRequestBuilder
{
    private readonly UserModel _user;

    public UserRequestBuilder()
    {
        _user = new UserModel();
    }

    public UserRequestBuilder WithName(string name)
    {
        _user.Name = name;
        return this;
    }

    public UserRequestBuilder WithUsername(string username)
    {
        _user.Username = username;
        return this;
    }

    public UserRequestBuilder WithEmail(string email)
    {
        _user.Email = email;
        return this;
    }

    public UserRequestBuilder WithPhone(string phone)
    {
        _user.Phone = phone;
        return this;
    }

    public UserRequestBuilder WithWebsite(string website)
    {
        _user.Website = website;
        return this;
    }

    public UserRequestBuilder WithAddress(AddressModel address)
    {
        _user.Address = address;
        return this;
    }

    public UserRequestBuilder WithCompany(CompanyModel company)
    {
        _user.Company = company;
        return this;
    }

    public UserModel Build()
    {
        return _user;
    }
}