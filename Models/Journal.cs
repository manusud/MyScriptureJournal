using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyScriptureJournal.Models
{
    public class Journal
    {
        public int ID { get; set; }

        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string FavoriteScripture { get; set; }
        
        [Display(Name = "Edition Date")]
        [DataType(DataType.Date)]
        [Required]
        public DateTime EditionDate { get; set; }

        [Required]
        public string Book { get; set; }

        [Required] 
        public string Notes { get; set; }
    }
}
