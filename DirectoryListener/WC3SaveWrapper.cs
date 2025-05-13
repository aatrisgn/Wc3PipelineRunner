using Source.Triggers;

namespace DirectoryListener;

internal class WC3SaveWrapper<T> where T: class
{
    public string SaveString { get; set; }
    public int Version { get; set; }
    public int HashCode { get; set; }
}

internal class PipelineWrapper
{
    public Dictionary<string, PipelineCommand> PipelineCommands { get; set; }
}
