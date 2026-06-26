using System;
using System.ComponentModel.DataAnnotations;

namespace Mycolog
{
    public class MushroomCulture
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Species { get; set; } = string.Empty;

        [StringLength(100)]
        public string Strain { get; set; } = string.Empty;

        public DateTime StartDate { get; set; } = DateTime.Now;

        [StringLength(100)]
        public string Substrate { get; set; } = string.Empty;

        [StringLength(50)]
        public string Container { get; set; } = string.Empty;

        [StringLength(50)]
        public string CurrentStage { get; set; } = "Мицелий";

        public int? Temperature { get; set; }
        public int? Humidity { get; set; }

        public string Notes { get; set; } = string.Empty;

        public bool IsHarvested { get; set; } = false;
        public decimal? HarvestWeight { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}