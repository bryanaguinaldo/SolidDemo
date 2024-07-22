using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SolidDemo.Data;
using SolidDemo.Interfaces;

namespace SolidDemo.Services;

public class LoginService : ILoginService
{
    public bool IsCustomerIdValid(int customerId) => CustomerData.Information.ContainsKey(customerId);
    public bool IsPasswordValid(int customerId, string password) => CustomerData.Information[customerId].Password == password;
    public UserInformation GetUserInformation(int customerId) => CustomerData.Information[customerId];
}