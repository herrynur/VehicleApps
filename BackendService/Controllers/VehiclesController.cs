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
    /// <summary>
    /// Api to get All Vehicles
    /// </summary>
    /// <remarks>
    /// Use PaginationFilter to get paginated data with Parameters PageNumber and PageSize,
    /// example:
    /// 
    ///     {BaseUrl}Vehicles?PageNumber=1&amp;PageSize=10
    ///     
    /// Use Query to search data with Parameters query,
    /// example:
    /// 
    ///     {BaseUrl}Vehicles?PageNumber=1&amp;PageSize=10&amp;Query=mandiri
    ///     
    /// </remarks>
    /// <param name="filter"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<PaginatedList<VehicleReadDto>>> GetVehiclesAsync(
        [FromQuery] PaginationFilter filter,
        CancellationToken cancellationToken)
    {
        return await vehicleService.GetVehicleAsync(filter, cancellationToken);
    }

    /// <summary>
    /// Get Single Vehicle
    /// </summary>
    /// <remarks>
    /// Use id from Vehicles data in path to get single data using GET Method, example:
    /// 
    ///     {BaseUrl}Vehicles/cdf76df8-d15c-4a0b-9c77-ec50dead8dac
    ///     
    /// </remarks>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
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

    /// <summary>
    /// Api to Post new Vehicle
    /// </summary>
    /// <param name="vehicleWriteDto"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<VehicleReadDto>> CreateVehicleAsync(
        [FromBody] VehicleWriteDto vehicleWriteDto,
        CancellationToken cancellationToken)
    {
        var result = await vehicleService.CreateVehicleAsync(vehicleWriteDto, cancellationToken);

        return result;
    }

    /// <summary>
    /// Api to Update Vehicle
    /// </summary>
    /// <param name="id"></param>
    /// <param name="vehicleWriteDto"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
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

    /// <summary>
    /// Api to Delete Vehicle
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
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
