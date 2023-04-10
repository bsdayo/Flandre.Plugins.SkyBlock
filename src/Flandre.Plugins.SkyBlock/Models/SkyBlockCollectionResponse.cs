using System.Text.Json.Serialization;

namespace Flandre.Plugins.SkyBlock.Models;

internal sealed class SkyBlockCollectionResponse : SkyBlockResponse
{
    [JsonPropertyName("collections")]
    public Dictionary<string, SkyBlockCollection> Collections { get; set; } = new();
}

public sealed class SkyBlockCollection
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = "Unknown Collection";

    [JsonPropertyName("items")]
    public Dictionary<string, SkyBlockCollectionItem> Items { get; set; } = new();
}

public sealed class SkyBlockCollectionItem
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = "";
    
    [JsonPropertyName("maxTiers")]
    public int MaxTiers { get; set; }

    [JsonPropertyName("tiers")]
    public SkyBlockCollectionItemTier[] Tiers { get; set; } = Array.Empty<SkyBlockCollectionItemTier>();
}

public sealed class SkyBlockCollectionItemTier
{
    [JsonPropertyName("tier")]
    public int Tier { get; set; }
    
    [JsonPropertyName("amountRequired")]
    public int AmountRequired { get; set; }

    [JsonPropertyName("unlocks")]
    public string[] Unlocks { get; set; } = Array.Empty<string>();
}

