using SolidDemo.Data;
using SolidDemo.Interfaces;

namespace SolidDemo.Services;

public class LoginService : ILoginService
{
    public bool IsCustomerIdValid(int customerId) => CustomerData.Information.Any(s => s.customerId == customerId);
    public bool IsPasswordValid(int customerId, string password) => CustomerData.Information.Any(s => s.customerId == customerId && s.Password == password);
    public UserInformation GetUserInformation(int customerId) => CustomerData.Information.First(s => s.customerId == customerId);
}