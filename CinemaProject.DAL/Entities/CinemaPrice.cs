﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaProject.DAL.Entities
{
    public class CinemaPrice
    {
        [Key]
        public int PriceId { get; set; }
        public int Price { get; set; }

    }
}