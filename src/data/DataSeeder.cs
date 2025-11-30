using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using insightflow_workspace_service.src.models;

namespace insightflow_workspace_service.src.data
{
    /// <summary>
    /// Clase para inicializar datos de prueba en el contexto de datos.
    /// </summary>
    public class DataSeeder
    {
        /// <summary>
        /// Inicializa los datos de prueba en el contexto de datos.
        /// </summary>
        /// <param name="ServiceProvider"></param>
        /// <returns></returns>
        public static async Task InitializeAsync(IServiceProvider ServiceProvider)
        {
            // Crear un scope para obtener servicios si es necesario
            using var scope = ServiceProvider.CreateScope();
            // Obtener el contexto de datos
            List<Workspace> workspaces = DataContext.Workspaces;
            // Configurar Bogus para generar datos falsos en español
            var faker = new Faker("es");
            // Verificar si ya existen workspaces
            if (!workspaces.Any())
            {
                // Crear algunos workspaces de prueba fijos
                var userTest1Owner = new User
                {
                    Id = Guid.Parse("a08799f8-746f-46b4-8134-2ef211fe705a"),
                    Name = "Carlos Arauco Colque",
                    Role = "Owner"
                };
                var userTest1Editor = new User
                {
                    Id = Guid.Parse("a08799f8-746f-46b4-8134-2ef211fe705a"),
                    Name = "Carlos Arauco Colque",
                    Role = "Editor"
                };
                var userTest2 = new User
                {
                    Id = Guid.Parse("9fd8ec52-3aa4-4097-86fa-2c576bc06e01"),
                    Name = "Jhon Vallecilla",
                    Role = "Editor"
                };
                var userTest3Owner = new User
                {
                    Id = Guid.Parse("2db519ca-4836-4e01-977f-ec518a081d54"),
                    Name = "Raul Hidalgo",
                    Role = "Owner"
                };
                var userTest3Editor = new User
                {
                    Id = Guid.Parse("2db519ca-4836-4e01-977f-ec518a081d54"),
                    Name = "Raul Hidalgo",
                    Role = "Editor"
                };
                var testWorkspace = new Workspace
                {
                    Id = Guid.Parse("d3a1d94c-1f3e-445f-8412-7209c7c2af1b"),
                    Name = "Trabajos de taller",
                    Description = "Espacio para compartir y colaborar en los trabajos del taller de arquitectura de sistemas.",
                    Topic = "Arquitectura de sistemas",
                    ImageUrl = "https://picsum.photos/640/480/?image=726",
                    Users = new List<User> { userTest1Owner, userTest2, userTest3Editor },
                    CreatedAt = DateTime.Now,
                    IsActive = true
                };
                var testWorkspace2 = new Workspace
                {
                    Id = Guid.Parse("d3a1d94c-1f3e-445f-8412-7209c7c2af1a"),
                    Name = "Librería de recursos",
                    Description = "Espacio para almacenar y compartir recursos relacionados con la arquitectura de sistemas.",
                    Topic = "Arquitectura de sistemas",
                    ImageUrl = "https://picsum.photos/640/480/?image=726",
                    Users = new List<User> { userTest3Owner, userTest1Editor, userTest2 },
                    CreatedAt = DateTime.Now,
                    IsActive = true
                };
                var testWorkspace3 = new Workspace
                {
                    Id = Guid.Parse("d3a1d94c-1f3e-445f-8412-7209c7c2af1c"),
                    Name = "Proyectos colaborativos",
                    Description = "Espacio para gestionar proyectos colaborativos en el ámbito de la arquitectura de sistemas.",
                    Topic = "Arquitectura de sistemas",
                    ImageUrl = "https://picsum.photos/640/480/?image=726",
                    Users = new List<User> { userTest3Owner, userTest2 },
                    CreatedAt = DateTime.Now,
                    IsActive = true
                };
                // Agregar los workspaces de prueba a la lista
                workspaces.Add(testWorkspace);
                workspaces.Add(testWorkspace2);
                workspaces.Add(testWorkspace3);
                // Crear workspaces adicionales de prueba generados aleatoriamente
                for (int i = 0; i < 10; i++)
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
                    // Generar entre 1 y 5 usuarios por workspace
                    int userCount = faker.Random.Int(1, 5);
                    // Agregar roles de usuarios al workspace
                    var roles = new[] { "Owner", "Editor" };
                    // Crear y agregar usuarios al workspace
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
                    // Agregar el workspace generado a la lista
                    workspaces.Add(workspace);
                }
            }
        }
    }
}