namespace NinjaSubs.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using System.ComponentModel.DataAnnotations;
    using System;
    using System.Collections.Generic;

    using static DataConstants;

    public class User : IdentityUser
    {
        [Required]
        [MinLength(UserFullNameMinLength)]
        [MaxLength(UserFullNameMaxLength)]
        public string FullName { get; set; }

        public DateTime Birthdate { get; set; }

        public IEnumerable<Subtitles> Subtitles { get; set; } = new List<Subtitles>();

        public IEnumerable<Article> Articles { get; set; } = new List<Article>();
    }
}
