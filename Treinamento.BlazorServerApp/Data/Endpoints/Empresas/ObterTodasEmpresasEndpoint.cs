﻿using Treinamento.BlazorServerApp.Data.ApiEndpoints;
using Treinamento.Shared.Results;

namespace Treinamento.BlazorServerApp.Data.Endpoints.Empresas;

public class ObterTodasEmpresasEndpoint(IApiEndpoints<EmpresaResult> apiEndpoints)
{
    public async Task<EmpresaResult[]> ExecuteAsync()
    {
        var result = await apiEndpoints.GetAllAsync("empresas");
        return result;
    }
}