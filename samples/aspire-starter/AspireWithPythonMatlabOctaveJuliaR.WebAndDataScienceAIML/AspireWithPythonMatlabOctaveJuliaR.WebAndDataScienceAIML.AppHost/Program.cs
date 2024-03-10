using HolisticWare.Tools.Aspire.Hosting.Clients.Python;
using HolisticWare.Tools.Aspire.Hosting.Clients.MatlabOctave;
using HolisticWare.Tools.Aspire.Hosting.Clients.Julia;
using HolisticWare.Tools.Aspire.Hosting.Clients.R;

var builder = DistributedApplication.CreateBuilder(args);

var apiService = builder.AddProject<Projects.AspireWithPythonMatlabOctaveJuliaR_WebAndDataScienceAIML_ApiService>("apiservice");

builder.AddProject<Projects.AspireWithPythonMatlabOctaveJuliaR_WebAndDataScienceAIML_Web>("webfrontend")
    .WithReference(apiService);


builder.AddScriptPython
                (
                    "clients-python-machine-learning",
                    $"{Environment.GetEnvironmentVariable("HOME")}/moljac-python/venv/bin/python3",
                    "../Clients/Python/simple-machine-learning/",
                    new string[] { "test1.py"}
                )
                .WithReference(apiService);

builder.AddScriptPythonDjango
                (
                    "webapp-python-django-web-frontent",
                    $"{Environment.GetEnvironmentVariable("HOME")}/moljac-python/venv/bin/python3",
                    "../Clients/Python/django/AspireTest/",
                    new string[] { "manage.py", "runserver" }
                )
                .WithReference(apiService);

builder.AddScriptPythonFlask
                (
                    "webapp-python-flask-web-frontent",
                    $".venv/bin/flask",
                    //$"flask",
                    "../Clients/Python/flask/",
                    new string[] { "--app", "app-minimal.py", "run" }
                )
                .WithReference(apiService);

builder.AddScriptMatlabOctave
                (
                    "clients-matlab-octave-machine-learning-anomaly-detection",
                    "octave",
                    "../Clients/MatLabOctave/machine-learning-octave-master/anomaly-detection/",
                    new string[] { "demo.m" }
                )
                .WithReference(apiService);

builder.AddScriptMatlabOctave
                (
                    "clients-matlab-octave-machine-learning-k-means",
                    "octave",
                    "../Clients/MatLabOctave/machine-learning-octave-master/k-means",
                    new string[] { "demo.m" }
                )
                .WithReference(apiService);

builder.AddScriptR
                (
                    "clients-r-machine-learning",
                    $"Rscript",
                    "../Clients/R/",
                    new string[] { "binary-classification-iris.r"}
                )
                .WithReference(apiService);

builder.AddScriptJulia
                (
                    "clients-julia-machine-learning",
                    "julia",
                    "../Clients/Julia/",
                    new string[] { "flux-linear-regression.jl"}
                )
                .WithReference(apiService);


builder.Build().Run();
