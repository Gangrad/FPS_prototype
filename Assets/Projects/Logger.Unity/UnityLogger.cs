using System;
using System.Runtime.CompilerServices;

namespace Logger.Unity {
    public class UnityLogger : ILogger {
        private readonly LogLevel _level;
        private readonly string _name;
        private readonly bool _hasName;

        public UnityLogger(LogLevel level, string name) {
            _level = level;
            _name = name;
            _hasName = !string.IsNullOrEmpty(name);
        }

        public UnityLogger(string name) :
#if DEBUG
            this(LogLevel.Debug, name) {
#else
            this(LogLevel.Info, name) {
#endif
        }

        private string CompleteMsg(string msg) {
            return _hasName ? string.Format("{0}: {1}", _name, msg) : msg;
        }

        public void Debug(string msg) {
            if (_level <= LogLevel.Debug)
                UnityEngine.Debug.Log(CompleteMsg(msg));
        }

        public void Info(string msg) {
            if (_level <= LogLevel.Info)
                UnityEngine.Debug.Log(CompleteMsg(msg));
        }

        public void Warn(string msg) {
            if (_level <= LogLevel.Warning)
                UnityEngine.Debug.LogWarning(CompleteMsg(msg));
        }

        public void Error(string msg) {
            if (_level <= LogLevel.Error)
                UnityEngine.Debug.LogError(CompleteMsg(msg));
        }

        public void Exception(Exception e) {
            if (_level <= LogLevel.Exception)
                UnityEngine.Debug.LogException(e);
        }
    }

    public static class UnityLoggerUtils {
        private static UnityLogFactory _factory;
        private static UnityLogFactory Factory { get { return _factory ?? (_factory = new UnityLogFactory()); } }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static UnityLogger GetCurrentClassLogger(LogLevel level) {
            return (UnityLogger) Factory.GetPrevClassLogger(level);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static UnityLogger GetCurrentClassLogger() {
            return (UnityLogger) Factory.GetPrevClassLogger();
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static UnityLogger GetEmptyLogger(LogLevel level) {
            return (UnityLogger) Factory.GetLogger(level, "");
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static UnityLogger GetEmptyLogger() {
            return (UnityLogger) Factory.GetLogger("");
        }
    }
}
