namespace HW.Aspire.MAUI;

public static partial class 
                                        ResourceBuilderExtensions
{
    public static
        IResourceBuilder<ProjectResource>
                                        GenerateSettings
                                        (
                                            this IResourceBuilder<ProjectResource> rb
                                        )
    {
        IEnumerable<EndpointAnnotation>? endpoints;

        rb.Resource.TryGetEndpoints(out endpoints);

        ProjectResource pr = rb.Resource;

        List<string> endpoint_reference_names = new List<string>();

        foreach(IResourceAnnotation ra in pr.Annotations)
        {          
            Type t = ra.GetType();


            switch(t.Name)
            {
                case "EndpointReferenceAnnotation":
                    endpoint_reference_names.Add(ra.Resource.Name);
                    break;
                default:
                    break;
            }
        }

        return rb;
    }
}
