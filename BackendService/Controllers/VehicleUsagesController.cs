using BackendService.Application.VehicleUsages.Dtos;
using BackendService.Application.VehicleUsages.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackendService.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class VehicleUsagesController(IVehicleUsageService vehicleUsageService) : ControllerBase
{
    /// <summary>
    /// Post Vehicle Usage
    /// </summary>
    /// <param name="input"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<VehicleUsageReadDto>> PostVehicleUsagesAsync(
        [FromBody] VehicleUsageWriteDto input,
        CancellationToken cancellationToken)
    {
        var result = await vehicleUsageService.PostVehicleUsageAsync(input, cancellationToken);

        return result;
    }
}
