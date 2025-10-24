using FlowLabApi.BackgroundServices;
using FlowLabApi.Models;
using Microsoft.AspNetCore.SignalR;
using Quartz;
using System.Diagnostics;
namespace FlowLabApi.Jobs;

public sealed class MachineJob : IJob
{
    private readonly IHubContext<ClockHub, IClock> _clockHub;
    private readonly ILogger<MachineJob> _logger;

    public MachineJob(
        ILogger<MachineJob> logger,
        IHubContext<ClockHub, IClock> clockHub)
    {
        _logger = logger;
        _clockHub = clockHub;
    }

    public Guid? JobId { get; set; }
    public async Task Execute(IJobExecutionContext context)
    {
        if (JobId == null) return;

        var me = Process.GetCurrentProcess();
        _logger.LogInformation("Working set {WorkingSet64} bytes", me.WorkingSet64);
        _logger.LogInformation("Total CPU time {TotalSeconds} sec", me.TotalProcessorTime.TotalSeconds);

        var machine = new MachineInfo(
            Environment.MachineName,
            me.TotalProcessorTime.TotalSeconds,
            DateTime.Now
        );
        _logger.LogInformation("Worker running at: {Time}", machine);
        await _clockHub.Clients.All.ShowTime(JobId.ToString(), machine);
    }
}
