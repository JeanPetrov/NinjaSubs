﻿namespace NinjaSubs.Services.Admin.Models
{
    using Common.AutoMapper;
    using Data.Models;

    public class AdminUserListingServiceModel : IMapFrom<User>
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }
    }
}
