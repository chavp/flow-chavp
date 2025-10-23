using FlowLabApi.Models;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using static FlowLabApi.Models.FlowModels;

namespace FlowLabApi.Controllers;

[ApiController]
[Route("[controller]")]
public class FlowsController : ControllerBase
{
    private readonly IWebHostEnvironment _env;
    public FlowsController(IWebHostEnvironment env)
    {
        _env = env;
    }


    [HttpPut("{flowId}")]
    public async Task<IActionResult> SaveFlow(Guid flowId, FlowData request)
    {
        var flowFilePath = Path.Combine(_env.ContentRootPath, "Flows", $"{flowId}.json");
        saveFlowToFile(flowFilePath, request);
        var flowData = loadFlowFromFile(flowFilePath);
        return Ok(flowData);
    }

    [HttpGet("{flowId}")]
    public async Task<IActionResult> GetFlow(Guid flowId)
    {
        var flowFilePath = Path.Combine(_env.ContentRootPath, "Flows", $"{flowId}.json");
        var flowData = loadFlowFromFile(flowFilePath);
        if(flowData == null)
        {
            return NotFound();
        }
        return Ok(flowData);
    }

    private void saveFlowToFile(string path, FlowData flowData)
    {
        var flowJson = System.Text.Json.JsonSerializer.Serialize(flowData);
        System.IO.File.WriteAllText(path, flowJson);
    }

    private FlowData? loadFlowFromFile(string path)
    {

        if(!System.IO.File.Exists(path)) return null;

        var json = System.IO.File.ReadAllText(path);
        if (string.IsNullOrEmpty(json)) return null;

        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            PropertyNameCaseInsensitive = true
        };
        var flowData = JsonSerializer.Deserialize<FlowData>(json, options);
        return flowData!;
    }
}
