using Application;
using Demo.Sample.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient<IImplInterface, ImplInterface>();


builder.Services.AddGrpc();

var app = builder.Build();


app.UseRouting();

app.MapGrpcService<MyApiController>().EnableGrpcWeb();



app.MapGet("/",
    () =>
        "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();