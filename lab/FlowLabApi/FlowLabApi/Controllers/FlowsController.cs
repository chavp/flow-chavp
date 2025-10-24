using FlowLabApi.BackgroundServices;
using FlowLabApi.Jobs;
using FlowLabApi.Models;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Quartz;
using System.Text.Json;
using static FlowLabApi.Models.FlowModels;

namespace FlowLabApi.Controllers;

[ApiController]
[Route("[controller]")]
public class FlowsController : ControllerBase
{
    private readonly IWebHostEnvironment _env;
    private readonly ILogger<FlowWorkerBackgroundService> _logger;
    private readonly ISchedulerFactory _schedulerFactory;

    public FlowsController(IWebHostEnvironment env,
        ILogger<FlowWorkerBackgroundService> logger,
        ISchedulerFactory schedulerFactory)
    {
        _env = env;
        _logger = logger;
        _schedulerFactory = schedulerFactory;
    }

    [HttpPut("{flowId}")]
    public async Task<IActionResult> SaveFlow(Guid flowId, FlowData request)
    {
        saveFlow(flowId, request);
        var flowData = getFlow(flowId);
        return Ok(flowData);
    }

    [HttpGet("{flowId}")]
    public async Task<IActionResult> GetFlow(Guid flowId)
    {
        var flowData = getFlow(flowId);
        if(flowData == null) 
        {
            return NotFound();
        }
        return Ok(flowData);
    }

    [HttpPut("nodes/{nodeId}/start")]
    public async Task<IActionResult> StartNode(Guid nodeId)
    {
        // 2. Get the scheduler
        IScheduler scheduler = await _schedulerFactory.GetScheduler();

        // 3. Define the job
        JobKey jobKey = new JobKey(nodeId.ToString(), "MachineGroup");
        // Retrieve the JobDetail using the JobKey
        IJobDetail jobDetail = await scheduler.GetJobDetail(jobKey);
        if (jobDetail == null)
        {
            IJobDetail job = JobBuilder.Create<MachineJob>()
                .SetJobData(new JobDataMap
                {
                    { "JobId", nodeId }
                })
                .WithIdentity(jobKey)
                .Build();

            // 4. Define the trigger
            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity($"{nodeId.ToString()}-trigger", "MachineGroup")
                .WithCronSchedule("* * * ? * *")
                .Build();

            // 5. Schedule the job with the trigger
            await scheduler.Start();
            await scheduler.ScheduleJob(job, trigger);
            _logger.LogInformation($"Job for {nodeId.ToString()} scheduled successfully.");
        }
        else
        {
            await scheduler.ResumeJob(jobKey);
            _logger.LogInformation($"Job for {nodeId.ToString()} resumed successfully.");
        }

            return Ok();
    }

    [HttpPut("nodes/{nodeId}/pause")]
    public async Task<IActionResult> PauseNode(Guid nodeId)
    {
        // 2. Get the scheduler
        IScheduler scheduler = await _schedulerFactory.GetScheduler();

        // 3. Define the job
        JobKey jobKey = new JobKey(nodeId.ToString(), "MachineGroup");
        IJobDetail jobDetail = await scheduler.GetJobDetail(jobKey);
        if (jobDetail != null)
        {
            await scheduler.PauseJob(jobKey);
            _logger.LogInformation($"Job for {nodeId.ToString()} stopped successfully.");
        }
        return Ok();
    }

    private void saveFlow(Guid flowId, FlowData flowData)
    {
        var path = Path.Combine(_env.ContentRootPath, "Flows", $"{flowId}.json");
        var newflowData = flowData;
        newflowData.SetId(flowId);
        var flowJson = System.Text.Json.JsonSerializer.Serialize(newflowData);
        System.IO.File.WriteAllText(path, flowJson);
    }

    private FlowData? getFlow(Guid flowId)
    {
        var path = Path.Combine(_env.ContentRootPath, "Flows", $"{flowId}.json");
        if (!System.IO.File.Exists(path)) return null;

        var json = System.IO.File.ReadAllText(path);
        if (string.IsNullOrEmpty(json)) return null;

        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            PropertyNameCaseInsensitive = true
        };
        var flowData = JsonSerializer.Deserialize<FlowData>(json, options);
        flowData.SetId(flowId);
        return flowData!;
    }
}
