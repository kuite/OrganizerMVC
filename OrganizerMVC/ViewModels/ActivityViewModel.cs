using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OrganizerMVC.ViewModels
{
    public class ActivityViewModel
    {
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Date")]
        public string Date { get; set; }

        [Required]
        [Display(Name = "Start")]
        public string Start { get; set; }

        [Required]
        [Display(Name = "End")]
        public string End { get; set; }
    }
}