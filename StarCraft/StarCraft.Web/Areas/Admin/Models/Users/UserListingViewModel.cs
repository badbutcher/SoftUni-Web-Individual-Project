namespace StarCraft.Web.Areas.Admin.Models.Users
{
    using System;
    using System.Collections.Generic;
    using StarCraft.Services.Admin.Models;

    public class UserListingViewModel
    {
        public IEnumerable<AdminUserListingServiceModel> Users { get; set; }

        public int TotalUsers { get; set; }

        public int TotalPages => (int)Math.Ceiling((double)this.TotalUsers / 25);

        public int CurrentPage { get; set; }

        public int PreviousPage => this.CurrentPage <= 1 ? 1 : this.CurrentPage - 1;

        public int NextPage => this.CurrentPage == this.TotalPages
            ? this.TotalPages : this.CurrentPage + 1;

        public int Minerals { get; set; }

        public int Gas { get; set; }
    }
}