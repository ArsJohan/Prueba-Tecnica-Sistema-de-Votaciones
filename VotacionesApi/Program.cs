using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using VotacionesApi.Models;
using VotacionesApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Configurar conexi�n a PostgreSQL
builder.Services.AddDbContext<ElectoralContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Inyecci�n de dependencias para los servicios
builder.Services.AddScoped<IVoterServices, VoterServices>();
builder.Services.AddScoped<ICandidateServices, CandidateServices>();
builder.Services.AddScoped<IVoteServices, VoteServices>();

// Controladores y Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Votaciones API", Version = "v1" });
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath); // Para la documentaci�n de resumen (///)
});

var app = builder.Build();

// Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
