namespace Trail_A.Repositry.Iterface
{
    public interface IGenericRepo<T> where T : class
    {
        void AddObject(T obj);
        void DeleteObject(T obj);
        void UpdateObject(T obj);
        List<T> GetAllObjects();
        T GetObject(int id);
        List<T> Search(Func<T,bool> func);
    }
}
