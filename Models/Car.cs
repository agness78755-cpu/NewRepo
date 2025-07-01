using System.ComponentModel.DataAnnotations;

public class Car
{
    [Key]
    public int CarID { get; set; }

    [StringLength(50)]
    public string Make { get; set; }

    [StringLength(50)]
    public string Model { get; set; }

    public int Year { get; set; }

    [StringLength(50)]
    public string LicensePlate { get; set; }

    public decimal DailyRate { get; set; }

    public string Status { get; set; }
}
