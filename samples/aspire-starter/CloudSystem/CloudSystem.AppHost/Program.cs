IDistributedApplicationBuilder builder = DistributedApplication.CreateBuilder(args);

IResourceBuilder<ProjectResource> apiService = builder.AddProject<Projects.CloudSystem_ApiService>("apiservice");

builder.AddProject<Projects.CloudSystem_Web>("webfrontend")
    .WithReference(apiService);


foreach(IResource r in builder.Resources)
{
    string json_r = System.Text.Json.JsonSerializer.Serialize(r);

    foreach(IResourceAnnotation ra in r.Annotations)
    {
        string json_ra = System.Text.Json.JsonSerializer.Serialize(ra);
        if(ra is AllocatedEndpointAnnotation)
        {
            int i;
        }
        if(ra is EndpointAnnotation)
        {
            int i;
        }
    }
}


DistributedApplication da = builder.Build();

//string json_builder = System.Text.Json.JsonSerializer.Serialize(builder);
string json_da = System.Text.Json.JsonSerializer.Serialize(da);

da.Run();

return;
