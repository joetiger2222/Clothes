using Clothes.Data;
using Clothes.MappingProfiles;
using Clothes.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using static System.Net.WebRequestMethods;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataDbContext>(options=>options.UseSqlServer(builder.Configuration.GetConnectionString("ClothesConnectionString")));
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddScoped<IClothItemRespository, SqlClothItemRepository>();
builder.Services.AddScoped<ICateogriesRepository,SqlCategoriesRepository>();
builder.Services.AddScoped<ICartRepository,SqlCartRepository>();
builder.Services.AddScoped<IImageRepository,SqlImageRepository>();

builder.Services.AddHttpContextAccessor();//image


builder.Services.AddIdentityCore<IdentityUser>()//login and register
    .AddRoles<IdentityRole>()
    .AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>("Auth")
    .AddEntityFrameworkStores<DataDbContext>()
    .AddDefaultTokenProviders();


builder.Services.Configure<IdentityOptions>(options =>//login and register
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric= false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;
});


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme);//token
    //.AddJwtBearer(options => options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    //{
    //    ValidateIssuer = true,
    //    ValidateAudience = true,
    //    ValidateLifetime = true,
    //    ValidateIssuerSigningKey = true,
    //    ValidIssuer = "http://clothes.somee.com",
    //    ValidAudience = "http://clothes.somee.com",
    //    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("eyJhbGciOiJIUzI1NiJ9.eyJSb2xlIjoiQWRtaW4iLCJJc3N1ZXIiOiJJc3N1ZXIiLCJVc2VybmF"))
    //});



builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});





var app = builder.Build();

// Configure the HTTP request pipeline.


    app.UseSwagger();
    app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseAuthentication();//login and register

app.UseAuthorization();


app.UseCors();//cors

if (!Directory.Exists("Images"))
{
    Directory.CreateDirectory("Images");
}

app.UseStaticFiles(new StaticFileOptions
{
//    if (!Directory.Exists("Images"))
//{
//    Directory.CreateDirectory("Images");
//}
FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Images")),
    RequestPath = "/Images"
});


app.MapControllers();

app.Run();
