using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using To_Do_List.Database;
using To_Do_List.Middlewares;
using To_Do_List.Models;
using To_Do_List.Models.DTOs;
using To_Do_List.Models.MapProfile;
using To_Do_List.Repository;
using To_Do_List.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDefaultIdentity<User>()
    .AddEntityFrameworkStores<ToDoDbContext>()
    .AddApiEndpoints();

builder.Services.AddDbContext<ToDoDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Tests")));

builder.Services.AddAutoMapper(typeof(ToDoTaskProfile));

builder.Services.AddValidatorsFromAssemblyContaining<ToDoTaskDTOValidator>()
    .AddFluentValidationAutoValidation()
    .AddFluentValidationClientsideAdapters();

builder.Services.AddScoped<IToDoTasksRepository, ToDoTasksRepository>();
builder.Services.AddScoped<ITaskService, TaskService>();
builder.Services.AddScoped<ErrorHandlingMiddleware>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseMiddleware<ErrorHandlingMiddleware>();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Tasks/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.MapRazorPages();
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Tasks}/{action=Index}/{id?}");

app.Run();
