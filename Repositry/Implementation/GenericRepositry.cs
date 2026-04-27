using Trail_A.Database_Context;
using Trail_A.Repositry.Iterface;

namespace Trail_A.Repositry.Implementation
{
    public class GenericRepositry<T> : IGenericRepo<T> where T : class
    {
        private readonly AppDbContext _context;
        public GenericRepositry(AppDbContext context)
        {
            _context = context;
        }
        public void AddObject(T obj)
        {
            _context.Set<T>().Add(obj);
             _context.SaveChanges();
        }

        public void DeleteObject(T obj)
        {
            _context.Set<T>().Remove(obj);
            _context.SaveChanges();
        }

        public List<T> GetAllObjects()
        {
            var data = _context.Set<T>().ToList();
            return data;
        }

        public T GetObject(int id)
        {
            var data = _context.Set<T>().Find(id);
            return data;
        }

        public List<T> Search(Func<T, bool> func)
        {
            var Data = _context.Set<T>().Where(func).ToList();
            return Data;
        }

        public void UpdateObject(T obj)
        {
            _context.Set<T>().Update(obj);
            _context.SaveChanges();
        }
    }
}
