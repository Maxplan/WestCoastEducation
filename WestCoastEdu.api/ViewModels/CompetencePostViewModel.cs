using System.ComponentModel.DataAnnotations;

namespace WestCoastEdu.api.ViewModels
{
    public class CompetencePostViewModel
    {
        [Required(ErrorMessage = "Name is missing")]
        public string Name { get; set; }
    }
}