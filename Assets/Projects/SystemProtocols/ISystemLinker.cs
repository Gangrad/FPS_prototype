using Logger;

namespace SystemProtocols {
    public interface ISystemLinker {
        ILogger Log { get; set; }
        bool CreateSymlink(string sourceFolder, string link, bool overrideIfExists);
        bool Delete(string link);
        bool IsLink(string folder);
    }
}