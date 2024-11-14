
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class AreaChange
    {
        [Required(ErrorMessage = "Id is required.")]
        public string Id { get; set; }

        [Required(ErrorMessage = "GeoJson is required.")]
        public string GeoJson { get; set; }  // GeoJSON format for points, lines, or polygons

        [Required(ErrorMessage = "Description is required.")]
        [StringLength(100, ErrorMessage = "Description must be between {2} and {1} characters long.", MinimumLength = 5)]
        public string Description { get; set; }
    }
}
