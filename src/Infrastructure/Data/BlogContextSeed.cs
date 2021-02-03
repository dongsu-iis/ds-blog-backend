using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Type = Core.Entities.Type;

namespace Infrastructure.Data
{
    public class BlogContextSeed
    {
        public static async Task SeedAsync(BlogContext context,ILoggerFactory loggerFactory)
        {
            try
            {
                var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

                /*
                if (!context.Type.Any())
                {
                    var types =
                        File.ReadAllText(path + @"/Data/SeedData/type.json");

                    var data = JsonSerializer.Deserialize<List<Type>>(types);

                    foreach (var item in data)
                    {
                        context.Type.Add(item);
                    }

                    await context.SaveChangesAsync();
                }
                */
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<BlogContextSeed>();
                logger.LogError(ex.Message);
            }
        }
    }
}
