using MusicStreamer.Infrastructure.EntityFrameworkProvider.Context;
using MusicStreamer.Infrastructure.EntityFrameworkProvider.Repositories;
using MusicStreamer.Domain.Interfaces;
using MusicStreamer.ApplicationLayer.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// DB Connection
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Services & Repositories
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<UsuarioService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();                             
    app.UseSwaggerUI();                         
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
