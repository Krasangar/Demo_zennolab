using Application;
using Generators;
using Grpc.Core;

namespace Demo.Sample.Services;

[GrpcMapping]
internal partial class ProductsApiController : ProductsApi.ProductsApiBase
{
    [GrpcInterface(typeof(IImplInterface), nameof(IImplInterface.ProductsMethod))]
    public override partial Task<MethodResponse> ProductsMethod(MethodRequest request, ServerCallContext context);
}