using DatabaseService.Services;

var builder = WebApplication.CreateBuilder(args);

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

// Add services to the container.
builder.Services.AddGrpc();

builder.Services.AddCors(o => o.AddPolicy("AllowAll", policy =>
{
    policy.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader()
        .WithExposedHeaders("Grpc-Status", "Grpc-Message", "Grpc-Encoding", "Grpc-Accept-Encoding");
}));

//builder.Services.AddAuthorization();


var app = builder.Build();

app.UseRouting();
app.UseCors("AllowAll");
app.UseGrpcWeb();

app.UseEndpoints(endpoints =>
{
    endpoints.MapGrpcService<DatabaseService.Services.DatabaseService>().EnableGrpcWeb();
});

// Configure the HTTP request pipeline.
//app.MapGrpcService<DatabaseService.Services.DatabaseService>().EnableGrpcWeb();
app.MapGet("/",
    () =>
        "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");


app.Run();
