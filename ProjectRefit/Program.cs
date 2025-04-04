using ProjectRefit.Handler;
using ProjectRefit.Interface.Refit;
using ProjectRefit.Interface.Service;
using ProjectRefit.Service;
using Refit;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMemoryCache();
builder.Services.AddSingleton<IUserService, UserService>();
builder.Services.AddSingleton<ITokenService, TokenService>();
builder.Services.AddTransient<AuthTokenHandler>();

builder.Services.AddRefitClient<IUserRefit>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://dummyjson.com"))
    .AddHttpMessageHandler<AuthTokenHandler>();

builder.Services.AddControllers();
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

app.UseAuthorization();

app.MapControllers();

app.Run();
