using System.Text;
using API.Context;
using API.Interface;
using API.Middlewares;
using API.Models;
using API.Repository;
using API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Sieve.Services;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddXmlDataContractSerializerFormatters();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(option =>{
    option.UseMySQL(builder.Configuration.GetConnectionString("default"));
});


builder.Services.AddScoped<JWTService>();
builder.Services.AddScoped<ContextSeedService>();
builder.Services.AddScoped<ISkills,SkillsRepository>();
builder.Services.AddScoped<IModule,ModuleRepository>();
builder.Services.AddScoped<IBatch,BatchRepository>();
builder.Services.AddScoped<ICourse,CourseService>();
builder.Services.AddSingleton<IHttpContextAccessor,HttpContextAccessor>();
builder.Services.AddSingleton<SieveProcessor>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//identity core service
builder.Services.AddIdentityCore<User>(option=>{
    option.Password.RequiredLength=6;
    option.Password.RequireDigit=true;
    option.Password.RequireUppercase=true;
    option.Password.RequireNonAlphanumeric=false;
    option.Lockout.AllowedForNewUsers = false;
})
.AddRoles<IdentityRole>() //be able to add role
.AddRoleManager<RoleManager<IdentityRole>>() //able to make use of roleManager
.AddEntityFrameworkStores<AppDbContext>() //providing our context
.AddSignInManager<SignInManager<User>>() //make use of sign in manager
.AddUserManager<UserManager<User>>() //make use of usermanager to create user
.AddDefaultTokenProviders(); //able to create tokens

//able to authenticate user using JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(option =>
    {
        option.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"])),
            ValidIssuer = builder.Configuration["JWT:Issuer"],
            ValidateIssuer = true,
            ValidateAudience = false
        };
    }
);

builder.Services.Configure<ApiBehaviorOptions>(option=>{
    option.InvalidModelStateResponseFactory = actionContext =>
    {
        var errors = actionContext.ModelState
        .Where(x => x.Value.Errors.Count > 0)
        .SelectMany(x => x.Value.Errors)
        .Select(x => x.ErrorMessage).ToArray();
        var toreturn = new{
            Errors =errors
        };
        return new BadRequestObjectResult(toreturn);
    };
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseMiddleware<EncryptionMiddleware>();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCors(option=>option
.AllowAnyOrigin()
.AllowAnyMethod()
.AllowAnyHeader());

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();


using var scope = app.Services.CreateScope();
try{
    var contextseedService = scope.ServiceProvider.GetService<ContextSeedService>();
    await contextseedService.InitalizeContextAsync();
}
catch(Exception e)
{
    var logger = scope.ServiceProvider.GetService<ILogger<Program>>();
    logger.LogError(e.Message,"Failed to initalize and seed database");
}

app.Run();
