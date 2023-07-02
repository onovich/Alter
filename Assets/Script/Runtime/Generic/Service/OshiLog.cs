using System;
using System.Collections.Generic;
using UnityEngine;

namespace Oshi.Generic {

    public static class OshiLog {

        [Flags]
        public enum LogLevel {
            None = 0,
            Log = 1,
            Warning = 2,
            Error = 4,
            All = Log | Warning | Error
        }

        // EVENT
        public static Action<string> OnLog;
        public static Action<string> OnWarning;
        public static Action<string> OnError;
        public static Action<bool, string> OnAssert;
        public static Action<bool> OnAssertWithoutMessage;
        public static Action OnTearDown;

        // CACHE
        public static bool AllowCache = false;
        static List<string> cacheList = new List<string>();
        public static List<string> CacheList => cacheList;

        // LEVEL
        public static LogLevel Level { get; set; } = LogLevel.All;

        public static void Log(string message) {

            if ((Level & LogLevel.Log) == 0) {
                return;
            }

            if (OnLog != null) {
                OnLog.Invoke(message);
            } else {
                Debug.Log(message);
            }

            if (AllowCache) {
                AddToCache(LogLevel.Log, message);
            }

        }

        public static void Warning(string message) {

            if ((Level & LogLevel.Warning) == 0) {
                return;
            }

            if (OnWarning != null) {
                OnWarning.Invoke(message);
            } else {
                Debug.LogWarning(message);
            }

            if (AllowCache) {
                AddToCache(LogLevel.Warning, message);
            }

        }

        public static void Error(string message) {

            if ((Level & LogLevel.Error) == 0) {
                return;
            }

            if (OnError != null) {
                OnError.Invoke(message);
            } else {
                Debug.LogError(message);
            }

            if (AllowCache) {
                AddToCache(LogLevel.Error, message);
            }

        }

        public static void Assert(bool isCondition) {
            if (OnAssertWithoutMessage != null) {
                OnAssertWithoutMessage.Invoke(isCondition);
            } else {
                Debug.Assert(isCondition);
            }
        }

        public static void Assert(bool isCondition, string message) {
            if (OnAssert != null) {
                OnAssert.Invoke(isCondition, message);
            } else {
                Debug.Assert(isCondition, message);
            }
        }

        public static void AddToCache(LogLevel level, string log) {
            string prefix;
            switch (level) {
                case LogLevel.Log:
                    prefix = "[log]";
                    break;
                case LogLevel.Warning:
                    prefix = "[warn]";
                    break;
                case LogLevel.Error:
                    prefix = "[err]";
                    break;
                default:
                    prefix = "";
                    break;
            }

            string str = DateTime.Now.ToLocalTime().ToString() + ": " + log;
            cacheList.Add(prefix + str);
        }

        public static string PackLogWithDeviceInfo(LogLevel logLevel, string src) {
            var os = System.Environment.OSVersion;
            return "<" + os.VersionString + "> [" + logLevel.ToString() + "]" + src;
        }

        public static void TearDown() {
            if (OnTearDown != null) {
                OnTearDown.Invoke();
            }
        }

    }

}