using Microsoft.AspNetCore.Mvc;

namespace StatusApi.Controllers;
public class StatusController : ControllerBase
{
    // http get localhost:5000/status
    [HttpGet("/status")]
    public ActionResult GetTheStatus()
    {
        var resp = new StatusResponse
        {
            Message = "The server is great thanks",
            LastChecked = DateTime.Now
        };
        return Ok(resp);
    }
}

public class StatusResponse
{
    public string Message { get; set; }
    public DateTime LastChecked { get; set; }  
}
