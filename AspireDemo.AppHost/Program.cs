var builder = DistributedApplication.CreateBuilder(args);

// add API project correctly
builder.AddProject("apiservice", "../ApiService/ApiService.csproj");

builder.Build().Run();

