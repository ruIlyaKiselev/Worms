using System.Collections.Generic;
using System.Linq;
using ConsoleApp1.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApp1.Database
{
    public class PostgresDatabaseORM: DbContext
    {
        private DbSet<WorldBehaviorDTO> WorldBehaviors { get; set; }

        public PostgresDatabaseORM()
        {
            Database.EnsureCreated();
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql($@"Host=localhost;
                                                    Port={PostgresContract.Port};
                                                    Database={PostgresContract.DBname};
                                                    Username={PostgresContract.User};
                                                    Password={PostgresContract.Password}");
        }

        public WorldBehaviorDTO GetWorldBehaviorByName(string name)
        {
            var queryResult = WorldBehaviors
                .Where(wb => wb.Name == name)
                .ToList();

            return queryResult.Count != 0 ? queryResult.ToList().First() : null;
        }

        public List<WorldBehaviorDTO> GetAllWorldBehaviors()
        {
            return WorldBehaviors.ToList();
        }

        public void SaveWorldBehavior(WorldBehaviorDTO worldBehaviorDto)
        {
            WorldBehaviors.Add(worldBehaviorDto);
            SaveChanges();
        }

        public void EditWorldBehavior(string name, string newValue)
        {
            WorldBehaviors.Add(new WorldBehaviorDTO(name, newValue));
            SaveChanges();
        }

        public void DeleteWorldBehavior(string name)
        {
            var itemToDelete = WorldBehaviors.ToList().Find(it => it.Name == name);
            if (itemToDelete != null) WorldBehaviors.Remove(itemToDelete);
            SaveChanges();
        }

        public bool CheckWorldBehaviorExists(string name)
        {
            return WorldBehaviors.ToList().Find(it => it.Name == name) != null;
        }
    }
}