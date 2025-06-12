using System.Net;
using System.Net.Http.Headers;
using System.Text;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using Treinamento.BlazorServerApp.Data.Services;
using Treinamento.Shared.Results;

namespace Treinamento.BlazorServerApp.Data.ApiEndpoints;

public interface IApiEndpoints<T>
{
    Task<ResultData> PostAsync(string endpoint, T data);
    Task<ResultData> PutAsync(string endpoint, Guid id, T data);
    Task<ResultData> DeleteAsync(string endpoint, Guid id);
    Task<IEnumerable<T>> GetAllAsync(string endpoint);
    Task<T?> GetByIdAsync(Guid id, string endpoint);
}

public class ApiEndpoints<T>(NavigationManager navManager, IHttpClientFactory httpClientFactory, JwtTokenFactory jwtTokenFactory) : IApiEndpoints<T>
{
    public async Task<ResultData> PostAsync(string endpoint, T data)
    {
        var uri = $"https://localhost:7061/api/{endpoint}";

        var httpClient = httpClientFactory.CreateClient();
        var token = jwtTokenFactory.GenerateJwtFromCurrentUser();

        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var sendObject = JsonConvert.SerializeObject(data);

        var content = new StringContent(sendObject, Encoding.UTF8, "application/json");

        var response = await httpClient.PostAsync(uri, content);

        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadAsStringAsync();
            var resultData = JsonConvert.DeserializeObject<ResultData>(result);

            return resultData!;
        }

        switch (response.StatusCode)
        {
            case HttpStatusCode.NotFound:
                navManager.NavigateTo("/404");
                break;
            case HttpStatusCode.Unauthorized:
                navManager.NavigateTo("/401");
                break;
            case HttpStatusCode.InternalServerError:
                navManager.NavigateTo("/500");
                break;
            case HttpStatusCode.BadRequest:
                var result = await response.Content.ReadAsStringAsync();
                var resultData = JsonConvert.DeserializeObject<ResultData>(result);

                return resultData!;
            default:
                throw new InvalidOperationException("não foi possível consultar a API.");
        }

        return new ResultData
        {
            Success = false,
            Message = $"Erro : {response.StatusCode}"
        };
    }

    public async Task<ResultData> PutAsync(string endpoint, Guid id, T data)
    {
        var uri = $"https://localhost:7061/api/{endpoint}";

        var httpClient = httpClientFactory.CreateClient();

        var token = jwtTokenFactory.GenerateJwtFromCurrentUser();

        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var sendObject = JsonConvert.SerializeObject(data);

        var content = new StringContent(sendObject, Encoding.UTF8, "application/json");

        var response = await httpClient.PutAsync(uri, content);

        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadAsStringAsync();
            var resultData = JsonConvert.DeserializeObject<ResultData>(result);

            return resultData!;
        }

        switch (response.StatusCode)
        {
            case HttpStatusCode.NotFound:
                navManager.NavigateTo("/404");
                break;
            case HttpStatusCode.Unauthorized:
                navManager.NavigateTo("/401");
                break;
            case HttpStatusCode.InternalServerError:
                navManager.NavigateTo("/500");
                break;
            case HttpStatusCode.BadRequest:
                var result = await response.Content.ReadAsStringAsync();
                var resultData = JsonConvert.DeserializeObject<ResultData>(result);

                return resultData!;
            default:
                throw new InvalidOperationException("não foi possível consultar a API.");
        }

        return new ResultData
        {
            Success = false,
            Message = $"Erro : {response.StatusCode}"
        };
    }

    public async Task<ResultData> DeleteAsync(string endpoint, Guid id)
    {
        var uri = $"https://localhost:7061/api/{endpoint}";

        var httpClient = httpClientFactory.CreateClient();

        var token = jwtTokenFactory.GenerateJwtFromCurrentUser();

        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await httpClient.DeleteAsync(uri);

        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadAsStringAsync();
            var resultData = JsonConvert.DeserializeObject<ResultData>(result);

            return resultData!;
        }

        switch (response.StatusCode)
        {
            case HttpStatusCode.NotFound:
                navManager.NavigateTo("/404");
                break;
            case HttpStatusCode.Unauthorized:
                navManager.NavigateTo("/401");
                break;
            case HttpStatusCode.InternalServerError:
                navManager.NavigateTo("/500");
                break;
            case HttpStatusCode.BadRequest:
                var result = await response.Content.ReadAsStringAsync();
                var resultData = JsonConvert.DeserializeObject<ResultData>(result);

                return resultData!;
            default:
                throw new InvalidOperationException("não foi possível consultar a API.");
        }

        return new ResultData
        {
            Success = false,
            Message = $"Erro : {response.StatusCode}"
        };
    }

    public async Task<IEnumerable<T>> GetAllAsync(string endpoint)
    {
        var uri = $"https://localhost:7061/api/{endpoint}";

        var httpClient = httpClientFactory.CreateClient();
        var token = jwtTokenFactory.GenerateJwtFromCurrentUser();

        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await httpClient.GetAsync(uri);

        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadAsStringAsync();
            var resultData = JsonConvert.DeserializeObject<ResultData<IEnumerable<T>>>(result);
            return resultData?.Data?.ToArray() ?? [];
        }

        switch (response.StatusCode)
        {
            case HttpStatusCode.NotFound:
                navManager.NavigateTo("/404");
                break;
            case HttpStatusCode.Unauthorized:
                navManager.NavigateTo("/401");
                break;
            case HttpStatusCode.InternalServerError:
                navManager.NavigateTo("/500");
                break;
            case HttpStatusCode.BadRequest:
                return Array.Empty<T>();
            default:
                throw new InvalidOperationException("não foi possível consultar a API.");
        }

        return Array.Empty<T>();
    }

    public async Task<T?> GetByIdAsync(Guid id, string endpoint)
    {
        var uri = $"https://localhost:7061/api/{endpoint}/{id}";

        var httpClient = httpClientFactory.CreateClient();

        var token = jwtTokenFactory.GenerateJwtFromCurrentUser();

        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await httpClient.GetAsync(uri);

        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadAsStringAsync();
            var resultData = JsonConvert.DeserializeObject<ResultData<T>>(result);

            return resultData!.Data!;
        }

        switch (response.StatusCode)
        {
            case HttpStatusCode.NotFound:
                navManager.NavigateTo("/404");
                break;
            case HttpStatusCode.Unauthorized:
                navManager.NavigateTo("/401");
                break;
            case HttpStatusCode.InternalServerError:
                navManager.NavigateTo("/500");
                break;
            case HttpStatusCode.BadRequest:
                throw new InvalidOperationException("Item procurado não existe.");
            default:
                throw new InvalidOperationException("não foi possível consultar a API.");
        }

        throw new InvalidOperationException("Item procurado não existe.");
    }
}