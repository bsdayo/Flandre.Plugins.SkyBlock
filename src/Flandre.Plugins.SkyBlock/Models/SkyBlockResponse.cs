using System.Text.Json.Serialization;

namespace Flandre.Plugins.SkyBlock.Models;

internal abstract class SkyBlockResponse
{
    [JsonPropertyName("success")]
    public bool Success { get; set; }
    
    /// <summary>
    /// Only if error
    /// </summary>
    [JsonPropertyName("cause")]
    public string? Cause { get; set; }
}