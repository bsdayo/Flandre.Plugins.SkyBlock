using Flandre.Core.Messaging;
using Flandre.Framework.Attributes;
using Flandre.Framework.Common;
using Microsoft.Extensions.Logging;

namespace Flandre.Plugins.SkyBlock;

public sealed class SkyBlockPlugin : Plugin
{
    private readonly SkyBlockService _service;
    private readonly ILogger<SkyBlockPlugin> _logger;

    public SkyBlockPlugin(SkyBlockService service, ILogger<SkyBlockPlugin> logger)
    {
        _service = service;
        _logger = logger;
    }

    [Command("sky.collection")]
    public async Task<MessageContent> Collection(params string[] itemName)
    {
        var query = string.Join(string.Empty, itemName);
        _logger.LogInformation("Searching {ItemName}...", query);
        var item = await _service.SearchFromAllCollection(query);

        if (item is null) return "Item not found.";

        var mb = new MessageBuilder().Text($"{item.Name} Collection");

        foreach (var tier in item.Tiers)
        {
            mb.Text($"\n\n[Tier {tier.Tier}] Require {tier.AmountRequired} items");
            foreach (var unlock in tier.Unlocks)
                mb.Text($"\n  {unlock}");
        }

        return mb.Build();
    }
}