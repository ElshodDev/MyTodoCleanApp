using MyTodoCleanApp.Application;
using MyTodoCleanApp.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Servislar
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddControllers();

// FAQAT SHU IKKALASI SWAGGER UCHUN YETARLI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();