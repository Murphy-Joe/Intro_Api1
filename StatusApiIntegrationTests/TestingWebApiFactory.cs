using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using StatusApi;
using System;
using System.Linq;

namespace StatusApiIntegrationTests;

public class TestingWebApiFactory<TEntryPoint> : WebApplicationFactory<TEntryPoint> where TEntryPoint : Program
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        base.ConfigureWebHost(builder);

        builder.ConfigureServices(services =>
        {
            var descriptor = services.SingleOrDefault(x =>
                x.ServiceType == typeof(ISystemTime));

            if (descriptor != null)
            {
                services.Remove(descriptor);
            }

            var mockSysTime = new Mock<ISystemTime>();
            mockSysTime.Setup(x => x.GetCurrent()).Returns(
                new DateTime(1985, 2, 11, 7, 30, 00));

            services.AddTransient<ISystemTime>(sp => mockSysTime.Object);
        });
    }
}
