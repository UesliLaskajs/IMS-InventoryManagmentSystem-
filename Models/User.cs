﻿namespace IMS_InventoryManagmentSystem_.Models
{
    public class User
    {
        public int Id { get; set; }

        public  string UserName { get; set; }=string.Empty;
        public required string Email { get; set; }=string.Empty;


        public string PasswordHash { get; set; }=string.Empty ;

        public string Role { get; set; }=string.Empty;


    }
}
