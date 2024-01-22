namespace Application;

public class ImplInterface : IImplInterface
{
    public Task<string> Method(int age, string name, CancellationToken token)
    {
        return Task.FromResult(nameof(Method));
    }

    public Task<string> ProductsMethod(string name, CancellationToken token)
    {
        return Task.FromResult(nameof(ProductsMethod));
    }
}