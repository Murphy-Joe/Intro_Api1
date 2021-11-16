using Microsoft.AspNetCore.Mvc;

namespace StatusApi.Controllers;
public class StatusController : ControllerBase
{
    private readonly ISystemTime _systemTime;

    public StatusController(ISystemTime systemTime)
    {
        _systemTime = systemTime;
    }



    [HttpGet("/status")]
    public ActionResult GetTheStatus()
    {
        var resp = new StatusResponse
        {
            Message = "The server is great thanks",
            LastChecked = _systemTime.GetCurrent()
        };
        return Ok(resp);
    }
}

public class StatusResponse
{
    public string Message { get; set; }
    public DateTime LastChecked { get; set; }  
}
