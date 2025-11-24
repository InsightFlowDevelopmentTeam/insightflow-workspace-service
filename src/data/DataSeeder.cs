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
                for (int i = 0; i < 100; i++)
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