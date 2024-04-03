namespace LoggingDemo.Shared;

using Microsoft.AspNetCore.Builder;
using Serilog;

public static class HostExtensions
{
    // read config from appsettings.json
    public static void AddSerilog(this ConfigureHostBuilder host)
        => host.UseSerilog((ctx,
                            cfg) => cfg
                                              .Enrich.WithProperty("Environment", ctx.HostingEnvironment.EnvironmentName)
                                              .Enrich.WithProperty("Application",
                                                  ctx.HostingEnvironment.ApplicationName)
                                                           .Enrich.WithThreadId()
                                                           .ReadFrom.Configuration(ctx.Configuration));

    // // config in code
    // public static void AddSerilog(this ConfigureHostBuilder host)
    //     => host.UseSerilog((ctx,
    //                         cfg) => cfg.Enrich.WithProperty("Environment", ctx.HostingEnvironment.EnvironmentName)
    //                                    .Enrich.WithProperty("Application",
    //                                        ctx.HostingEnvironment.ApplicationName)
    //                                    .Enrich.WithMachineName()
    //                                    .WriteTo.Seq("http://localhost:5341")
    //                                    .WriteTo.Console()
    //     );
}
