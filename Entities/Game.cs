using System.ComponentModel.DataAnnotations;

namespace GameStore.api.Entities;

public class Game
{
    public int Id { get; set; }
    [Required]
    [StringLength(50)]
    public required string Name { get; set; }
    [Required]
    [StringLength(20)]
    public required string Genre { get; set; }
    [Required]
    [Length(0,1000)] 
    public decimal Price { get; set; }
    public DateTime ReleaseDate { get; set; }
    [Url]
    [StringLength(100)]
    public required string ImageUri { get; set; }
}