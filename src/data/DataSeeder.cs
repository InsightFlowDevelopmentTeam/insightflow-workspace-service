using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using insightflow_workspace_service.src.models;

namespace insightflow_workspace_service.src.data
{
    public class DataSeeder
    {
        public static async Task InitializeAsync(IServiceProvider ServiceProvider)
        {
            using var scope = ServiceProvider.CreateScope();
            List<Workspace> workspaces = DataContext.Workspaces;
            var faker = new Faker("es");
            if (!workspaces.Any())
            {
                var userTest = new User
                {
                    Id = Guid.Parse("b3850a65-61d9-4417-8b03-de3a700d7064"),
                    Name = "Juan Pérez",
                    Role = "Owner"
                };
                var userTest2 = new User
                {
                    Id = Guid.Parse("afd352d5-aaae-4829-ae37-a2de588f97ab"),
                    Name = "Manuel Herrera",
                    Role = "Editor"
                };
                var testWorkspace = new Workspace
                {
                    Id = Guid.Parse("d3a1d94c-1f3e-445f-8412-7209c7c2af1b"),
                    Name = "Trabajos de taller",
                    Description = "Espacio para compartir y colaborar en los trabajos del taller de arquitectura de sistemas.",
                    Topic = "Arquitectura de sistemas",
                    ImageUrl = "https://picsum.photos/640/480/?image=726",
                    Users = new List<User> { userTest, userTest2 },
                    CreatedAt = DateTime.Now,
                    IsActive = true
                };
                workspaces.Add(testWorkspace);
                for (int i = 0; i < 20; i++)
                {
                    var workspace = new Workspace
                    {
                        Id = Guid.NewGuid(),
                        Name = faker.Company.CompanyName(),
                        Description = faker.Lorem.Sentence(),
                        Topic = faker.Hacker.Noun(),
                        ImageUrl = faker.Image.PicsumUrl(),
                        CreatedAt = faker.Date.Past(),
                        IsActive = true
                    };

                    int userCount = faker.Random.Int(1, 5);
                    var roles = new[] { "Owner", "Editor" };
                    for (int j = 0; j < userCount; j++)
                    {
                        var user = new User
                        {
                            Id = Guid.NewGuid(),
                            Name = faker.Name.FullName(),
                            Role = faker.PickRandom(roles)
                        };
                        workspace.Users.Add(user);
                    }
                    workspaces.Add(workspace);
                }
            }
        }
    }
}