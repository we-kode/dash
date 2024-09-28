using Dash.Application.Contracts;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Dash.Server.HostedServices;

public class IntegrationInitService(IDisplayRepository displayRepository, IElementRepository elementRepository, ICalendarRepository calendarRepository) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Console.WriteLine("Importing new found data...");
        await displayRepository.Init();
        await elementRepository.Init();
        await calendarRepository.Init();
        Console.WriteLine("Importing finisehd.");
    }
}
