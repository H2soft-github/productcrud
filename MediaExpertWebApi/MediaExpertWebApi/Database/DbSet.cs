using System.Collections.Concurrent;

namespace MediaExpertWebApi.Database
{
    public class DbSet<T> where T : Entry
    {
        private ConcurrentDictionary<int, T> entries = new ConcurrentDictionary<int, T>();
        private int IdAutoIncrement = 0;

        public void Add(T entry)
        {
            if (!entries.TryAdd(++IdAutoIncrement, entry))
            {
                throw new Exception("Error adding entry.");
            }
            entry.Id = IdAutoIncrement;
        }

        public void Update(T entry)
        {
            CheckId(entry.Id);
            if (!entries.TryUpdate(entry.Id, entry, Get(entry.Id)))
            {
                throw new Exception("Error updating entry.");
            }
        }

        public void  Delete(int id)
        {
            CheckId(id);
            T? entry = default;
            if (!entries.TryRemove(id, out entry))
            {
                throw new Exception("Error deleting entry.");
            }
        }

        public T Get(int id)
        {
            CheckId(id);
            T? entry = default;
            if (entries.TryGetValue(id, out entry))
            {
                return entry!;
            } else
            {
                throw new KeyNotFoundException($"No entry with id {id}.");
            }
        }

        public ICollection<T> Get()
        {
            return entries.Values.ToList();
        }

        public int Count()
        {
            return entries.Count;
        }

        private void CheckId(int id)
        {
            if (!entries.Keys.Contains(id))
            {
                throw new KeyNotFoundException("Key not found.");
            }
        }
    }
}
