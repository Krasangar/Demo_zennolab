using Application;
using Generators;
using Grpc.Core;

namespace Demo.Sample.Services;

[GrpcMapping]
internal partial class MyApiController : MyGrpcApi.MyGrpcApiBase
{
    [GrpcInterface(typeof(IImplInterface), nameof(IImplInterface.Method))]
    public override partial Task<MethodResponse> Method(MethodRequest request, ServerCallContext context);
}