using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _23_practice
{
    public class EntityCache
    {
        private static EntityCache _instance;
        private Dictionary<string, List<object>> cache;
        private HashSet<string> typeRegistry;

        private EntityCache()
        {
            cache = new Dictionary<string, List<object>>();
            typeRegistry = new HashSet<string>();
        }

        public static EntityCache GetInstance()
        {
            if (_instance == null)
            {
                _instance = new EntityCache();
            }
            return _instance;
        }

        public void AddEntity(string entityType, object entity)
        {
            if (!cache.ContainsKey(entityType))
            {
                cache[entityType] = new List<object>();
            }
            cache[entityType].Add(entity);
        }

        public bool HasEntity(string entityType, object entity)
        {
            return cache.ContainsKey(entityType) && cache[entityType].Contains(entity);
        }

        public bool HasEntityType(string entityType)
        {
            return cache.ContainsKey(entityType);
        }

        public void RegisterEntityType(string entityType)
        {
            typeRegistry.Add(entityType);
        }

        public void RemoveEntity(string entityType, object entity)
        {
            if (cache.ContainsKey(entityType))
            {
                cache[entityType].Remove(entity);
            }
        }

        public void RemoveEntityType(string entityType)
        {
            if (cache.ContainsKey(entityType))
            {
                cache.Remove(entityType);
            }
            if (typeRegistry.Contains(entityType))
            {
                typeRegistry.Remove(entityType);
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            EntityCache cache = EntityCache.GetInstance();

            cache.RegisterEntityType("Person");

            cache.AddEntity("Person", new { Name = "Alice", Age = 30 });
            cache.AddEntity("Person", new { Name = "Bob", Age = 25 });

            Console.WriteLine("Person type registered: " + cache.HasEntityType("Person"));
            Console.WriteLine("Person Alice exists: " + cache.HasEntity("Person", new { Name = "Alice", Age = 30 }));

            cache.RemoveEntity("Person", new { Name = "Alice", Age = 30 });

            cache.RemoveEntityType("Person");

            Console.WriteLine("Person type registered: " + cache.HasEntityType("Person"));
        }
    }
}
