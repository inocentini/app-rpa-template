using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RPA.Infrastructure.Data;

namespace RPA.Application
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    // Configuração do DbContext
                    services.AddDbContext<RPADbContext>(options =>
                        options.UseSqlServer(hostContext.Configuration["ConnectionStrings:DefaultConnection"]));

                    // Configuração dos serviços
                    services.AddHostedService<OrchestratorService>();
                })
                .Build();

            await host.RunAsync();
        }
    }

    public class OrchestratorService : BackgroundService
    {
        private readonly ILogger<OrchestratorService> _logger;

        public OrchestratorService(ILogger<OrchestratorService> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Orchestrator rodando em: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
