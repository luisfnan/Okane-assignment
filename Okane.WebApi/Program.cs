using Okane.Contracts;
using Okane.Core.Repositories;
using Okane.Core.Security;
using Okane.Core.Services;
using Okane.Core.Validations;
using Okane.Storage.EntityFramework;
using Okane.WebApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// App dependencies
builder.Services.AddTransient<IExpensesRepository, ExpensesRepository>();
builder.Services.AddTransient<IExpensesService, ExpensesService>();
builder.Services.AddTransient<IAuthService, AuthService>();
builder.Services.AddTransient<IPasswordHasher, BCryptPasswordHasher>();
builder.Services.AddTransient<IValidator<CreateExpenseRequest>, DataAnnotationsValidator<CreateExpenseRequest>>();
builder.Services.AddTransient<ICategoriesRepository, CategoriesRepository>();
builder.Services.AddTransient<ICategoriesService, CategoriesService>();
builder.Services.AddTransient<IUsersRepository, UsersRepository>();
builder.Services.AddTransient<Func<DateTime>>(provider => () => DateTime.Now);
builder.Services.AddDbContext<OkaneDbContext>();

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