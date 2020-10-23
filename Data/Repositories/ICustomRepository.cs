using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace Data
{
    public interface ICustomRepository
    {
        char ExistCheck(string username,string email);
        Users ActiveCodeCheck(string id);
        bool LoginCheck(string username, string password);
    }
}
