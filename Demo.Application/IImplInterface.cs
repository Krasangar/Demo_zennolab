namespace Application;

public interface IImplInterface
{
    public Task<string> Method(int age, string name, CancellationToken token);
    public Task<string> ProductsMethod(string name, CancellationToken token);
}