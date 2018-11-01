
public interface IPool<T> : IObject
{
    T Get();
    void Return(T obj);
}
