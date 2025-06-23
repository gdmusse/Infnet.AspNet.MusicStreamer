using MusicStreamer.Infrastructure.EntityFrameworkProvider.Context;
using MusicStreamer.Infrastructure.EntityFrameworkProvider.Repositories;
using MusicStreamer.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using MusicStreamer.Application.Interfaces;
using MusicStreamer.Application.Dtos;

var builder = WebApplication.CreateBuilder(args);

// DB Connection
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Services & Repositories
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IAssinaturaRepository, AssinaturaRepository>();
builder.Services.AddScoped<IAssinaturaService, AssinaturaService>();
builder.Services.AddScoped<ITransacaoRepository, TransacaoRepository>();
builder.Services.AddScoped<ITransacaoService, TransacaoService>();
builder.Services.AddScoped<IMusicaRepository, MusicaRepository>();
builder.Services.AddScoped<IMusicaService, MusicaService>();
builder.Services.AddScoped<IBandaRepository, BandaRepository>();
builder.Services.AddScoped<IBandaService, BandaService>();

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
