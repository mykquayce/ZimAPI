using Helpers.Zim.Services;
using Microsoft.AspNetCore.Mvc;

namespace ZimApi.WebApplication.Controllers;

[Route("[controller]")]
[ApiController]
public class MetaDataController(IZimService service) : ControllerBase
{
	[HttpGet]
	public IAsyncEnumerable<Uri> Get([FromQuery] Uri uri) => service.GetUrisAsync(uri);
}
