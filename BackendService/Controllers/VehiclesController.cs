using BackendService.Application.Vehicles.Dtos;
using BackendService.Application.Vehicles.Service;
using BackendService.Helper.Api;
using BackendService.Helper.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackendService.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class VehiclesController(IVehicleService vehicleService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<PaginatedList<VehicleReadDto>>> GetVehiclesAsync(
        [FromQuery] PaginationFilter filter,
        CancellationToken cancellationToken)
    {
        return await vehicleService.GetVehicleAsync(filter, cancellationToken);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<VehicleReadDto>> GetVehicleByIdAsync(
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var result =  await vehicleService.GetVehicleByIdAsync(id, cancellationToken);

        if (result is null)
        {
            return Problem(statusCode: 404, detail: "Vehicle not found");
        }

        return result;
    }

    [HttpPost]
    public async Task<ActionResult<VehicleReadDto>> CreateVehicleAsync(
        [FromBody] VehicleWriteDto vehicleWriteDto,
        CancellationToken cancellationToken)
    {
        var result = await vehicleService.CreateVehicleAsync(vehicleWriteDto, cancellationToken);

        return result;
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<VehicleReadDto>> UpdateVehicleAsync(
        [FromRoute] Guid id,
        [FromBody] VehicleWriteDto vehicleWriteDto,
        CancellationToken cancellationToken)
    {
        var result = await vehicleService.UpdateVehicleAsync(id, vehicleWriteDto, cancellationToken);

        if (result is null)
        {
            return Problem(statusCode: 404, detail: "Vehicle not found");
        }

        return result;
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<VehicleReadDto>> DeleteVehicleAsync(
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var result = await vehicleService.DeleteVehicleAsync(id, cancellationToken);

        if (result is null)
        {
            return Problem(statusCode: 404, detail: "Vehicle not found");
        }

        return result;
    }
}
