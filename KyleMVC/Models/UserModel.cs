using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace KyleMVC.Models
{
    public class UserModel
    {
        [Key]
        public int UserID { get; set; }

        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string User_Name { get; set; }
        public virtual ICollection<JournalModels> Journals { get; set; }

        public virtual ApplicationUser User { get; set; }
        public string ApplicationUserId { get; set; }
    }
}