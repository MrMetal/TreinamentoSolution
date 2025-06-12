using Treinamento.BlazorServerApp.Data.ApiEndpoints;
using Treinamento.Shared.Results;

namespace Treinamento.BlazorServerApp.Data.Endpoints.Empresas;

public class ObterEmpresaPorIdEndpoint(IApiEndpoints<EmpresaResult> apiEndpoints)
{
    public async Task<EmpresaResult> ExecuteAsync(Guid id)
    {
        var result = await apiEndpoints.GetByIdAsync(id, $"empresas");
        return result!;
    }
}