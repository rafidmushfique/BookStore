﻿using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore.Models
{
    [NotMapped]
    public class VMLogin
    {
        public String Email { get; set; }
        public String Password { get; set; }
        public bool KeepLoggedIn { get; set; }
    }
}
