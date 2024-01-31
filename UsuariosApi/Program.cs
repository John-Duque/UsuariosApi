using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using UsuariosApi.Authorization;
using UsuariosApi.Data;
using UsuariosApi.Model;
using UsuariosApi.Service;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("UsuarioConnection");

// Add services to the container.

builder.Services.AddDbContext<UsuarioDbContext>
    (opts =>
    {
        //Todo:MySql
        opts.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        //Todo: Sql Server
        //opts.UseSqlServer(connectionString);
    });

builder.Services.AddIdentity<Usuario, IdentityRole>() // Adicionar o conceito de identidade para esse usuario
    .AddEntityFrameworkStores<UsuarioDbContext>() // Para fazer a comunicação com a base de dados e salvar meu usuario
    .AddDefaultTokenProviders();

builder.Services.AddSingleton<IAuthorizationHandler, IdadeAuthorization>();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;//Criando Autenticação
}).AddJwtBearer(options =>
{
    //Definando as configurações do JwtBearer
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("*9ASHDA98H9ah9ha9H9A89n0f*9ASHDA98H9*")),
        ValidateAudience = false, // Verificar depois se sera preciso usar
        ValidateIssuer = false, // Verificar depois se sera preciso usar
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("IdadeMinima", policy =>
    {
        policy.AddRequirements(new IdadeMinima(18));//Criando regra de autorização
    });
});

//Fazendo Injeção de Dependência  
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<UsuarioService>();
builder.Services.AddScoped<TokenService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Usuario", Version = "v1" });
    //Fazendo a criação do botão Authorization no swagger
    c.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey, // Para que usuario não digite "Bearer" no swagger usse "SecuritySchemeType.Http" no Type
        Scheme = JwtBearerDefaults.AuthenticationScheme,
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization Header - utilizado com Bearer Authentication.\r\n\r\n" +
                        "Digite 'Bearer' [espaço] e então seu token no campo abaixo.\r\n\r\n" +
                        "Exemplo (informar sem as aspas): 'Bearer 12345abcdef'"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = JwtBearerDefaults.AuthenticationScheme
                }
            },
            new List<string>()
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "UsuarioApi v1"));
}

app.UseHttpsRedirection();

app.UseAuthentication(); // Para dizer que a aplicação usa autenticação

app.UseAuthorization();

app.MapControllers();

app.Run();

