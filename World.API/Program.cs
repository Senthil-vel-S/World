using Microsoft.EntityFrameworkCore;
using World.API.Common;
using World.API.Datas;
using World.API.Repository;
using World.API.Repository.IRepository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

#region Cors
builder.Services.AddCors(options =>
{
    options.AddPolicy("Policy", x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});
#endregion

#region Database
builder.Services.AddDbContext<ApplicationDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
#endregion

#region Mapping
builder.Services.AddAutoMapper(typeof(MappingProfile));
#endregion

#region Repository
builder.Services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddTransient<ICountryRepository, CountryRepository>();
#endregion

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("Policy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
