using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PharmacyWebApp.Data.Models;

public partial class MedicineInformation
{
    [Required(ErrorMessage = "Medicine ID is required")]
    public string MedicineId { get; set; } = null!;

    [Required(ErrorMessage = "Medicine Name is required")]
    public string MedicineName { get; set; } = null!;

    [Required(ErrorMessage = "Active Ingredients is required")]
    public string ActiveIngredients { get; set; } = null!;

    [Required(ErrorMessage = "Expiration Date is required")]
    public string? ExpirationDate { get; set; }

    [Required(ErrorMessage = "Dosage Form is required")]
    public string DosageForm { get; set; } = null!;

    [Required(ErrorMessage = "Warnings and Precautions is required")]
    public string WarningsAndPrecautions { get; set; } = null!;

    [Required(ErrorMessage = "Please select a Manufacturer")]
    public string? ManufacturerId { get; set; }

    public virtual Manufacturer? Manufacturer { get; set; }
}