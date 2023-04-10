using System.Collections.Immutable;
using Flandre.Plugins.SkyBlock.Models;

namespace Flandre.Plugins.SkyBlock;

public sealed class SkyBlockResourceManager
{
    private readonly SkyBlockClient _client;
    private bool _initialized;

    private ImmutableDictionary<string, SkyBlockCollectionItem> _allCollections = null!;

    public SkyBlockResourceManager(SkyBlockClient client)
    {
        _client = client;
    }

    private async Task Initialize()
    {
        _allCollections = (await _client.GetCollections())
            .SelectMany(cls => cls.Value.Items)
            .Select(item =>
                new KeyValuePair<string, SkyBlockCollectionItem>(
                    SkyBlockUtils.ConvertItemId(item.Key), item.Value))
            .ToImmutableDictionary();

        _initialized = true;
    }

    public async Task<ImmutableDictionary<string, SkyBlockCollectionItem>> GetAllCollectionsItems()
    {
        if (!_initialized)
            await Initialize();

        return _allCollections;
    }
}