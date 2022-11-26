using OpenTelemetry;
using OpenTelemetry.Exporter;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var serviceName = "ServiceB";
var serviceVersion = "1.0";
builder.Services.AddOpenTelemetryTracing((b) =>
{
    b.AddSource(serviceName)
    .SetResourceBuilder(
        ResourceBuilder.CreateDefault()
            .AddService(serviceName: serviceName, serviceVersion: serviceVersion));
    b.AddAspNetCoreInstrumentation();
    b.AddHttpClientInstrumentation();
    b.AddConsoleExporter();
    b.AddOtlpExporter(opt =>
    {
        opt.Protocol = OtlpExportProtocol.Grpc;
        opt.Endpoint = new Uri("http://localhost:4317");
        //Varsay�lan de�er ExportProcessorType.Batch
        //Di�er se�enek ExportProcessorType.Simple
        opt.ExportProcessorType = ExportProcessorType.Batch;
        opt.BatchExportProcessorOptions = new BatchExportProcessorOptions<Activity>()
        {
            //Maksimum kuyruk boyutu, s�n�ra ula�t���nda veri b�rak�l�r
            MaxQueueSize = 2048,
            //�ki veri aras�ndaki ge�ikme s�resi
            ScheduledDelayMilliseconds = 5000,
            //Export i�leminin zaman a��m� s�resi
            ExporterTimeoutMilliseconds = 30000,
            //Her export i�leminin maksimum boyutu
            MaxExportBatchSize = 512,
        };
    });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
