using SPTarkov.DI.Annotations;
using SPTarkov.Server.Core.DI;
using SPTarkov.Server.Core.Helpers;
using SPTarkov.Server.Core.Models.Spt.Mod;
using SPTarkov.Server.Core.Servers;

namespace UnderbarrelsBuffed;

public record ModMetadata : AbstractModMetadata
{
    public override string ModGuid { get; init; } = "com.hazeofdream.underbarrelsbuffed";
    public override string Name { get; init; } = "Underbarrels Buffed";
    public override string Author { get; init; } = "Haze_of_dream";

    public override List<string>? Contributors { get; init; } = null;

    public override SemanticVersioning.Version Version { get; init; } = new("1.0.0");
    public override SemanticVersioning.Range SptVersion { get; init; } = new("~4.0.0");

    public override List<string>? Incompatibilities { get; init; } = null;
    public override Dictionary<string, SemanticVersioning.Range>? ModDependencies { get; init; } = null;

    public override string? Url { get; init; } = "https://github.com/Hazeofdream/UnderBarrelsBuffed";

    public override bool? IsBundleMod { get; init; } = false;

    public override string License { get; init; } = "MIT";
}

[Injectable(TypePriority = OnLoadOrder.PostDBModLoader + 100)]
public class UnderbarrelsBuffed(
    DatabaseServer databaseServer,
    ItemHelper itemHelper)
    : IOnLoad
{
    public Task OnLoad()
    {
        var itemDatabase = databaseServer.GetTables().Templates.Items;

        foreach (var (itemId, item) in itemDatabase)
        {
            var props = item.Properties;
            if (props == null) continue;

            // Only target underbarrel grenade launchers
            if (itemHelper.IsOfBaseclass(itemId, BaseClasses.LAUNCHER))
            {
                props.Ergonomics = 0.0;
                props.Weight = 1.36;
            }
        }

        return Task.CompletedTask;
    }
}