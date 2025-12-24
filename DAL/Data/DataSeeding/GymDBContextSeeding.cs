using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using DAL.Data.Contexts;
using DAL.Entities.Category;
using DAL.Entities.Plan;

namespace DAL.Data.DataSeeding
{
    public static class GymDBContextSeeding
    {
        public static bool SeedData(GymDbContext context)
        {
            try
            {
                var HasPlan = context.Plans.Any();
                var HasCategory = context.Categories.Any();
                if (HasPlan && HasCategory) return false;
                if (!HasPlan)
                {
                    var Plans = LoadDataFromJsonFiles<Plan>("plans.json");
                    if (Plans.Any()) context.AddRange(Plans);

                }
                if (!HasCategory)
                {
                    var Categories = LoadDataFromJsonFiles<Category>("categories.json");
                    if (Categories.Any()) context.AddRange(Categories);

                }

                return context.SaveChanges() > 0;
            }catch(Exception ex)
            {
                Console.WriteLine($"Seeding Failed : {ex}");
                return false;
            }
        }

        private static List<T> LoadDataFromJsonFiles<T>(string FileName)
        {
            var FilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", FileName);
            if (!File.Exists(FilePath)) throw new FileNotFoundException();
            var data = File.ReadAllText(FilePath);
            var Options = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            };
            return JsonSerializer.Deserialize<List<T>>(data, Options) ?? new List<T>();
        }
    }
}
