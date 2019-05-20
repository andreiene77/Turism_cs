namespace ServerTurism.Model
{
    public interface IHasId<T>
    {
        T Id { get; }
    }
}