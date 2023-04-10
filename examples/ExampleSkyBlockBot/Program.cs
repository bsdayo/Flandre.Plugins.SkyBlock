using Flandre.Adapters.OneBot;
using Flandre.Framework;
using Flandre.Plugins.SkyBlock;
using Microsoft.Extensions.Hosting;

var builder = FlandreApp.CreateBuilder(args);

// Adapter
var adapterConfig = new OneBotAdapterConfig();
adapterConfig.Bots.Add(new OneBotBotConfig
{
    Protocol = OneBotProtocol.WebSocket,
    Endpoint = Environment.GetEnvironmentVariable("ONEBOT_ENDPOINT")
});
builder.Adapters.Add(new OneBotAdapter(adapterConfig));

// Plugin
builder.Plugins.AddSkyBlock(opts =>
    opts.ApiKey = Environment.GetEnvironmentVariable("HYPIXEL_APIKEY"));

using var app = builder.Build();

// Middlewares
app.UseCommandParser();
app.UseCommandInvoker();

app.Run();