using CinemaProject.TL.DTO;
using CinemaProject.TL.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaProject.BL.Interfaces
{
    public interface IUserLogic
    {
        Response AddUser(CinemaUserDTO registerDTO, string rePassword);
        CinemaUserDTO GetUserByEmail(string email);
        string GetFullName(int id);
        List<CinemaRoleDTO> GetRoleByUserId(int id);
    }
}
