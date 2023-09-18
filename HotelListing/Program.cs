using HotelListing;
using HotelListing.Configurations;
using HotelListing.Data;
using HotelListing.IRepository;
using HotelListing.Repository;
using HotelListing.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling =Newtonsoft.Json.ReferenceLoopHandling.Ignore);

//dbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("sqlConnection")));

//service extensions

builder.Services.ConfigureIdentity();
//builder.Services.ConfigureJWT(builder.Configuration);
builder.Services.Configure<JwtConfig>(builder.Configuration.GetSection(JwtConfig));

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = jwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(jwt =>
{
    var key = Encoding.ASCII.GetBytes(builder.Configuration.GetSection("JwtConfig").Value);
    jwt.SaveToken = true;
    jwt.TokenValidationParameters = new TokenValidationParameters(){
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,//for dev purpose
        ValidateAudience = false, //for dev purpose
        RequireExpirationTime = false,
        ValidateLifetime = true
    };
});

//create a custom cors policy
builder.Services.AddCors(o =>
{
    o.AddPolicy("AllowAll", builder =>
    builder.AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());
});

builder.Services.AddAutoMapper(typeof(MapperInitializer));

//unit of work 
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
//AuthManager implementation
builder.Services.AddScoped<IAuthManager, AuthManager>();

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

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthentication();//where the problem is

app.UseAuthorization();

app.MapControllers();

app.Run();
