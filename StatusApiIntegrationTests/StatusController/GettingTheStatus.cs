using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace StatusApiIntegrationTests;

public class GettingTheStatus : IClassFixture<TestingWebApiFactory<Program>>
{
    private readonly HttpClient _client;

    public GettingTheStatus(TestingWebApiFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetA200StatusResp()
    {
        var resp = await _client.GetAsync("/status");
        Assert.Equal(HttpStatusCode.OK, resp.StatusCode);
    }

    [Fact]
    public async Task HasCorrectMediaType()
    {
        var resp = await _client.GetAsync("/status");
        Assert.Equal("application/json", resp.Content?.Headers?.ContentType?.MediaType);
    }

    [Fact]
    public async Task HasCorrectEntity()
    {
        /* does it return the right data */
        var resp = await _client.GetAsync("/status");

        //var content = await resp.Content.ReadAsStringAsync();

        var respMsg = await resp.Content.ReadAsAsync<GetStatusResponse>();

        Assert.Equal("The server is great thanks", respMsg?.message);
        Assert.Equal(new DateTime(1985, 2, 11, 7, 30, 00), respMsg?.lastChecked);
    }

    //{"message":"The Server is Great.. Thanks","lastChecked":"2021-11-15T11:05:41.9320386-05:00"}
    public class GetStatusResponse
    {
        public string message { get; set; }
        public DateTime lastChecked { get; set; }
    }

}
