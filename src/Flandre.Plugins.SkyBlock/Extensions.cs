using Flandre.Framework;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Flandre.Plugins.SkyBlock;

public static class PluginCollectionExtensions
{
    private static void AddSkyBlockServices(IPluginCollection plugins)
    {
        plugins.Services.AddSingleton<SkyBlockClient>();
        plugins.Services.AddSingleton<SkyBlockResourceManager>();
        plugins.Services.AddSingleton<SkyBlockService>();
    }

    public static void AddSkyBlock(this IPluginCollection plugins, IConfiguration configuration)
    {
        AddSkyBlockServices(plugins);
        plugins.Add<SkyBlockPlugin, SkyBlockPluginOptions>(configuration);
    }

    public static void AddSkyBlock(this IPluginCollection plugins, Action<SkyBlockPluginOptions> action)
    {
        AddSkyBlockServices(plugins);
        plugins.Add<SkyBlockPlugin, SkyBlockPluginOptions>(action);
    }
}