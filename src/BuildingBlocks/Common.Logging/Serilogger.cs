using Microsoft.Extensions.Hosting;
using Serilog;

namespace Common.Logging
{
    public class Serilogger
    {
        public static Action<HostBuilderContext, LoggerConfiguration> Configure => (context, configuration) =>
        {
            var applicationName = context.HostingEnvironment.ApplicationName?.ToLower().Replace(".","-");
            var enviromentName = context.HostingEnvironment.EnvironmentName ?? "Development";

            configuration
                .WriteTo.Debug()
                .WriteTo.Console(outputTemplate:
                "[{Timestamp:HH:mm:ss}{Level}]{SourceContext}{NewLine}{Message:lj}{NewLine}{Exeption}{NewLine}")
                .Enrich.FromLogContext()
                .Enrich.WithMachineName()
                .Enrich.WithProperty("Enviroment", enviromentName)
                .Enrich.WithProperty("Application", applicationName)
                .ReadFrom.Configuration(context.Configuration);
        };
    }
}