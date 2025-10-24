using FlowLabApi.Jobs;
using FlowLabApi.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.VisualBasic;
using Quartz;
using System.Security.Claims;

namespace FlowLabApi.BackgroundServices;

public class FlowWorkerBackgroundService : BackgroundService
{
    private readonly ILogger<FlowWorkerBackgroundService> _logger;
    private readonly ISchedulerFactory _schedulerFactory;

    public FlowWorkerBackgroundService(
        ILogger<FlowWorkerBackgroundService> logger,
        ISchedulerFactory schedulerFactory)
    {
        _logger = logger;
        _schedulerFactory = schedulerFactory;
    }

    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        
    }
}

public class ClockHub : Hub<IClock>
{
    public async Task SendMyLocalTimeToUser(string userId, string nodeId, MachineInfo machinInfo)
    {
        await Clients.User(userId).ShowTime(nodeId, machinInfo);
    }
}

public interface IClock
{
    Task ShowTime(string nodeId, MachineInfo machinInfo);
}

//public partial class ClockHubClient : IClock, IHostedService
//{
//    private readonly ILogger<ClockHubClient> _logger;
//    private HubConnection _connection;

//    public ClockHubClient(ILogger<ClockHubClient> logger)
//    {
//        _logger = logger;

//        _connection = new HubConnectionBuilder()
//            .WithUrl(Strings.HubUrl)
//            .Build();

//        _connection.On<DateTime>(Strings.Events.TimeSent, ShowTime);
//    }

//    public Task ShowTime(DateTime currentTime)
//    {
//        _logger.LogInformation("{CurrentTime}", currentTime.ToShortTimeString());

//        return Task.CompletedTask;
//    }

//    public async Task StartAsync(CancellationToken cancellationToken)
//    {
//        // Loop is here to wait until the server is running
//        while (true)
//        {
//            try
//            {
//                await _connection.StartAsync(cancellationToken);

//                break;
//            }
//            catch
//            {
//                await Task.Delay(1000, cancellationToken);
//            }
//        }
//    }

//    public async Task StopAsync(CancellationToken cancellationToken)
//    {
//        await _connection.DisposeAsync();
//    }
//}