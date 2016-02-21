using System.ComponentModel.DataAnnotations;

namespace OrganizerMVC.ViewModels
{
    public class EventViewModel
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Title")]
        public string Title { get; set; }

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