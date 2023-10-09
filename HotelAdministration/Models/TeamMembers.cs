﻿using HotelAdministration.Migrations;

namespace HotelAdministration.Models
{
    public class TeamMembers
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName {  get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Role {  get; set; }
    }
}
