using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace Data
{
    public interface IAccountRepository
    {
        bool ExistCheck(string email);
        Users ActiveCodeCheck(string id);
        bool LoginCheck(string email, string password);
        string GetUserNameByEmail(string email);
        Users GetUserByEmail(string email);
        IEnumerable<Addresses> GetAddressByEmail(string email);
        string GetStateById(int id);
        IEnumerable<Cities> GetCitiesByStateId(int id);
        IEnumerable<ProductGroups> GetMainProductGroups();
        IEnumerable<ProductGroups> GetAllSubGroups(int groupId);
    }
}
