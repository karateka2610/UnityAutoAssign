using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// [NEW v3.0] Advanced debugging and logging system for AutoAssign.
/// Provides detailed information about assignment operations.
/// </summary>
public static class AutoAssignDebugger
{
    public enum LogLevel
    {
        Error,
        Warning,
        Info,
        Debug,
        Verbose
    }

    private static LogLevel currentLogLevel = LogLevel.Info;
    private static bool enableStackTrace = false;
    private static List<string> logHistory = new List<string>();
    private static int maxHistorySize = 1000;

    /// <summary>
    /// Set the current log level for filtering output
    /// </summary>
    public static void SetLogLevel(LogLevel level)
    {
        currentLogLevel = level;
    }

    /// <summary>
    /// Enable or disable stack traces in logs
    /// </summary>
    public static void SetStackTraceEnabled(bool enabled)
    {
        enableStackTrace = enabled;
    }

    /// <summary>
    /// Log an error message
    /// </summary>
    public static void LogError(string message, MonoBehaviour context = null)
    {
        if (currentLogLevel < LogLevel.Error) return;

        string fullMessage = FormatMessage("[AutoAssign ERROR]", message);
        Debug.LogError(fullMessage, context);
        AddToHistory(fullMessage);
    }

    /// <summary>
    /// Log a warning message
    /// </summary>
    public static void LogWarning(string message, MonoBehaviour context = null)
    {
        if (currentLogLevel < LogLevel.Warning) return;

        string fullMessage = FormatMessage("[AutoAssign WARNING]", message);
        Debug.LogWarning(fullMessage, context);
        AddToHistory(fullMessage);
    }

    /// <summary>
    /// Log an info message
    /// </summary>
    public static void LogInfo(string message, MonoBehaviour context = null)
    {
        if (currentLogLevel < LogLevel.Info) return;

        string fullMessage = FormatMessage("[AutoAssign INFO]", message);
        Debug.Log(fullMessage, context);
        AddToHistory(fullMessage);
    }

    /// <summary>
    /// Log a debug message
    /// </summary>
    public static void LogDebug(string message, MonoBehaviour context = null)
    {
        if (currentLogLevel < LogLevel.Debug) return;

        string fullMessage = FormatMessage("[AutoAssign DEBUG]", message);
        Debug.Log(fullMessage, context);
        AddToHistory(fullMessage);
    }

    /// <summary>
    /// Log a verbose message
    /// </summary>
    public static void LogVerbose(string message, MonoBehaviour context = null)
    {
        if (currentLogLevel < LogLevel.Verbose) return;

        string fullMessage = FormatMessage("[AutoAssign VERBOSE]", message);
        Debug.Log(fullMessage, context);
        AddToHistory(fullMessage);
    }

    /// <summary>
    /// Get the log history
    /// </summary>
    public static List<string> GetLogHistory()
    {
        return new List<string>(logHistory);
    }

    /// <summary>
    /// Clear the log history
    /// </summary>
    public static void ClearLogHistory()
    {
        logHistory.Clear();
    }

    /// <summary>
    /// Export log history to a string
    /// </summary>
    public static string ExportLogHistory()
    {
        return string.Join("\n", logHistory);
    }

    private static string FormatMessage(string prefix, string message)
    {
        string formatted = $"{prefix} {message}";
        if (enableStackTrace)
        {
            formatted += $"\n{System.Environment.StackTrace}";
        }
        return formatted;
    }

    private static void AddToHistory(string message)
    {
        logHistory.Add($"[{System.DateTime.Now:HH:mm:ss.fff}] {message}");
        
        if (logHistory.Count > maxHistorySize)
        {
            logHistory.RemoveRange(0, logHistory.Count - maxHistorySize);
        }
    }
}
