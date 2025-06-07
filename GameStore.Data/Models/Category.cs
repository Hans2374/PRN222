using System.ComponentModel.DataAnnotations;

namespace GameStore.Data.Models
{
    public partial class Category
    {
        public Category()
        {
            Games = new HashSet<Game>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = null!;

        public virtual ICollection<Game> Games { get; set; }
    }
}