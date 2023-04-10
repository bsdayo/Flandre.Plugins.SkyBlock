using Flandre.Plugins.SkyBlock.Models;
using Microsoft.Extensions.Options;
using RestSharp;
using RestSharp.Authenticators;

namespace Flandre.Plugins.SkyBlock;

internal class HypixelApiAuthenticator : IAuthenticator
{
    private readonly string? _apiKey;

    public HypixelApiAuthenticator(string? apiKey)
    {
        _apiKey = apiKey;
    }

    public ValueTask Authenticate(IRestClient client, RestRequest request)
    {
        if (_apiKey is not null)
            request.AddHeader("API-Key", _apiKey);
        return ValueTask.CompletedTask;
    }
}

public sealed class SkyBlockClient
{
    private readonly RestClient _http;

    public SkyBlockClient(IOptions<SkyBlockPluginOptions> options)
    {
        var clientOptions = new RestClientOptions("https://api.hypixel.net")
        {
            Authenticator = new HypixelApiAuthenticator(options.Value.ApiKey)
        };
        _http = new RestClient(clientOptions);
    }

    private static TResponse EnsureSuccess<TResponse>(TResponse? resp) where TResponse : SkyBlockResponse
    {
        if (resp is null)
            throw new SkyBlockException("SkyBlock response is null.");

        if (!resp.Success)
            throw new SkyBlockException($"SkyBlock request failed. Cause: {resp.Cause}");

        return resp;
    }

    public async Task<Dictionary<string, SkyBlockCollection>> GetCollections()
    {
        var req = new RestRequest("/resources/skyblock/collections");
        var res = await _http.GetAsync<SkyBlockCollectionResponse>(req);
        return EnsureSuccess(res).Collections;
    }
}