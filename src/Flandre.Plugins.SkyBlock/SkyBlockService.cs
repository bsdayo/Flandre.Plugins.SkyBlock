using Flandre.Plugins.SkyBlock.Models;

namespace Flandre.Plugins.SkyBlock;

public sealed class SkyBlockService
{
    private readonly SkyBlockClient _client;
    private readonly SkyBlockResourceManager _resources;

    public SkyBlockService(SkyBlockClient client, SkyBlockResourceManager resources)
    {
        _client = client;
        _resources = resources;
    }

    public async Task<SkyBlockCollectionItem?> SearchFromAllCollection(string search)
    {
        var items = await _resources.GetAllCollectionsItems();
        var query = SkyBlockUtils.ConvertItemId(search);

        var names = items.Select(kv => kv.Key).ToArray();

        var itemName = names.FirstOrDefault(name => name == query);
        
        if (itemName is null)
            if (query.Length >= 4)
                itemName = names.FirstOrDefault(name => name.Contains(query));

        return itemName is null ? null : items[itemName];
    }
}