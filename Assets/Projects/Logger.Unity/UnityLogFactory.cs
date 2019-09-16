namespace Logger.Unity {
    public class UnityLogFactory  : LogFactory {
        public UnityLogFactory(LogLevel level = LogLevel.Info)
            : base(level) {
        }

        public override ILogger GetLogger(LogLevel level, string name) {
            return new UnityLogger(level, name);
        }
    }
}