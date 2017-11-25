namespace StarCraft.Web.Models.AccountViewModels
{
    using System.ComponentModel.DataAnnotations;
    using StarCraft.Data.Models.Enums;

    public class ExternalLoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Race:")]
        public Race Race { get; set; }
    }
}