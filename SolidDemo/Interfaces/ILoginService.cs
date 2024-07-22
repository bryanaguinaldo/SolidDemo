using SolidDemo.Data;

namespace SolidDemo.Interfaces;

public interface ILoginService
{
    bool IsCustomerIdValid(int customerId);
    bool IsPasswordValid(int customerId, string password);
    UserInformation GetUserInformation(int customerId);
}