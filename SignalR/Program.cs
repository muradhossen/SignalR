using SignalR.SignalRHub;

var builder = WebApplication.CreateBuilder(args);
 

builder.Services.AddControllers(); 
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Added signalR service --01
builder.Services.AddSignalR();

var app = builder.Build();
 
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//Added signalR hub --02
app.MapHub<NotificationsHub>("notification-hub");

app.Run();
