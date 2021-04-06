using CinemaProject.TL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaProject.BL.Interfaces
{
    public interface IAccountLogic
    {
        bool IsSignedIn();
        string GetCurentUserFullName();
        string EncryptPassword(string password);
        void AddToken(TokenDTO tokenDTO);
        void RemoveAllTokensByUserId(int userId);
        void Logout();
        int GetCurentUserId();
        bool IsAdmin();
    }
}
