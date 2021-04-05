using CinemaProject.Models;
using CinemaProject.TL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaProject.Helpers.Interfaces
{
    public interface IAccountControllerHelper
    {
        CinemaUserDTO BuildDTO(RegisterViewModel registerViewModel);
    }
}
