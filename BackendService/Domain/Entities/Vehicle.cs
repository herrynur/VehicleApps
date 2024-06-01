using BackendService.Domain.Enums;
using BackendService.Helper.Helper;
using System.ComponentModel.DataAnnotations;

namespace BackendService.Domain.Entities;

public class Vehicle : EntityBase
{
    [StringLength(100)]
    public string? VehicleNumber { get; set; }
    public VehicleStatus Status { get; set; }
    public VehicleType Type { get; set; }
}
