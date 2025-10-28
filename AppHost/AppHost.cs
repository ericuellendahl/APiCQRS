var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.ApiCQRS>("apicqrs");

builder.Build().Run();
