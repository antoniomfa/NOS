namespace NOS.Engineering.Challenge.Database;

public interface IMockData<TOut>
{
    IDictionary<Guid, TOut> GenerateMocks();
}