﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaProject.DAL.Entities
{
    public class CinemaRole
    {
        [Key]
        public int RoleId { get; set; }
        public string Role { get; set; }

    }
}
