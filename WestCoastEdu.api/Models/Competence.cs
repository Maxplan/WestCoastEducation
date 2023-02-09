using System.ComponentModel.DataAnnotations.Schema;

namespace WestCoastEdu.api.Models
{
    public class Competence
    {
        public int Id { get; init; }
        public string Name { get; set; }
        public ICollection<Teacher> Teachers { get; set; }
    }
}