﻿namespace qlkho.Models
{
    public class User
    {
        public int UserID { get; set; }

        public string? Username { get; set; }

        public string? Password { get; set; }

        public int RoleID { get; set; }
        public Role? Role { get; set; }
    }
}
