using Xunit;
using To_Do_List.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using To_Do_List.Database;
using Microsoft.Extensions.DependencyInjection;

namespace To_Do_List.Services.Tests
{
    public class TaskServiceTests
    {
        private HttpClient _client;

        public TaskServiceTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.WithWebHostBuilder(builder =>
            {

                builder.ConfigureServices(services =>
                {
                    var dbContextOptions = services.SingleOrDefault(service => service.ServiceType == typeof(DbContextOptions<ToDoDbContext>));

                    services.Remove(dbContextOptions);

                    services.AddDbContext<ToDoDbContext>(opt => opt.UseSqlServer("Data Source=DESKTOP-UTN9VVA\\SQLEXPRESS;Initial Catalog=To-DoDBTests;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False"));
                });
            }).CreateClient();
        }


        [Fact]
        public void AddTaskByUserTest()
        {
            //arrange
            
        }

        [Fact]
        public void GetTasksListByUserTest()
        {

        }
    }
}