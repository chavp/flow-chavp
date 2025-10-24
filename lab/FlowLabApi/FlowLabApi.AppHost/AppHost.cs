var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.FlowLabApi>("flowlabapi");

builder.Build().Run();
