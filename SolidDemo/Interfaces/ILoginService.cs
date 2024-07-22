using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SolidDemo.Data;

namespace SolidDemo.Interfaces;

public interface ILoginService
{
    bool IsCustomerIdValid(int customerId);
    bool IsPasswordValid(int customerId, string password);
    UserInformation GetUserInformation(int customerId);
}