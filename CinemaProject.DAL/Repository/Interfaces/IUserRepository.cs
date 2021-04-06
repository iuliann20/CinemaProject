using CinemaProject.TL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaProject.DAL.Repository.Interfaces
{
    public interface IUserRepository
    {
        void AddUser(CinemaUserDTO registerDTO);
        CinemaUserDTO GetUserByEmail(string email);
        CinemaUserDTO GetUserById(int id);
        List<CinemaRoleDTO> GetRoleByUserId(int id);
    }
}
