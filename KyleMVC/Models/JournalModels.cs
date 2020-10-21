using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KyleMVC.Models
{
    public class JournalModels
    {
        //Composite key with that contains a unique date
        public virtual UserModel User { get; set; }
        
        [Key][Column(Order = 0)]
        public int UserID { get; set; }
        
        [Key][Column(Order = 1)]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Required]
        [MinLength(10)]
        [MaxLength(50)]
        public string Title { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [MinLength(75)]
        public string UsrText { get; set; }

    }
}