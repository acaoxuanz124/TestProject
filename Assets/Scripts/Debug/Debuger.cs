
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public static class Debuger
{
    public struct DebugData
    {
        public string condition;
        public string stackTrace;
        public UnityEngine.LogType type;

        public DebugData(string condition, string stackTrace, UnityEngine.LogType type)
        {
            this.condition = condition;
            this.stackTrace = stackTrace;
            this.type = type;
        }
    }


    private const string LogFm = "[Log]";
    private const string LogWarningFm = "[LogWarning]";
    private const string LogErrorFm = "[LogError]";

    private const short LogUtcHour = 8;

    private const string LogTimeFm = "[yyyy/MM/dd hh:mm:ss]";



    private static bool isShow = false;
    private static bool isStart = false;
    private static bool isDrag = false;


    private const int windowWidth = 800;
    private const int windowHeight = 600;

    private static Rect rectWindow = new Rect(20, 20, 120, 50);
    private static Vector2 scrollPosition;

    private static GUIStyle labelStyle = new GUIStyle()
    {
        fontSize = 30
    };
    private static List<DebugData> listDebugData = new List<DebugData>();
    public static void Init()
    {
        Application.logMessageReceived += LogMessageReceived;
        GameEvent.OnGUI.AddListener(OnGUI);
    }

    private static void LogMessageReceived(string condition, string stackTrace, UnityEngine.LogType type)
    {
        listDebugData.Add(new DebugData(condition, stackTrace, type));
    }

    public static void OnGUI()
    {
        GUILayout.Space(30);
        isShow = GUI.Toggle(new Rect(Screen.width / 2, Screen.height / 2, 300, 300), isShow, "log");
        if (isShow)
        {
            if (!isStart)
            {
                rectWindow = new Rect(Screen.width / 2 - windowWidth / 2, Screen.height / 2 - windowHeight / 2, windowWidth, windowHeight);
                isStart = true;
            }
            rectWindow = GUILayout.Window(0, rectWindow, delegate (int id)
              {
                  scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.Height(windowHeight), GUILayout.Width(windowWidth));
                  for (int i = listDebugData.Count - 1; i >= 0; i--)
                  {
                      DebugData debugData = listDebugData[i];
                      StartLabelStyle(debugData);
                      GUILayout.Label(Tool.ReturnTabText(debugData.condition, labelStyle, windowWidth), labelStyle, GUILayout.Width(windowWidth));
                  }
                  GUILayout.EndScrollView();
                  isDrag = GUILayout.Toggle(isDrag, "isDrag");
                  if (isDrag)
                      GUI.DragWindow();
              }, "Logs", GUILayout.Width(windowWidth), GUILayout.Height(windowHeight));
        }
        else
        {
            isStart = false;
        }
    }

    private static void StartLabelStyle(DebugData debugData)
    {
        switch (debugData.type)
        {
            case LogType.Log:
                labelStyle.normal.textColor = Color.white;
                break;
            case LogType.Warning:
                labelStyle.normal.textColor = Color.yellow;
                break;
            case LogType.Error:
                labelStyle.normal.textColor = Color.red;
                break;
            case LogType.Exception:
                labelStyle.normal.textColor = Color.red;
                break;
            default:
                labelStyle.normal.textColor = Color.white;
                break;
        }
    }
    public static void Log(object obj)
    {
        LogHandler(LogType.Log, obj);
    }


    public static void LogError(object obj)
    {
        LogHandler(LogType.Error, obj);
    }
    public static void LogWarning(object obj)
    {
        LogHandler(LogType.Warning, obj);
    }

    public static void Log(string content)
    {
        if (content == null)
            return;
        LogHandler(LogType.Log, content);
    }
    public static void Log(string content, params object[] args)
    {
        if (content == null)
            return;
        LogHandler(LogType.Log, content, args);
    }
    public static void LogError(string content)
    {
        if (content == null)
            return;
        LogHandler(LogType.Error, content);
    }
    public static void LogError(string content, params object[] args)
    {
        if (content == null)
            return;
        LogHandler(LogType.Error, content, args);
    }
    public static void LogWarning(string content)
    {
        if (content == null)
            return;
        LogHandler(LogType.Warning, content);
    }
    public static void LogWarning(string content, params object[] args)
    {
        if (content == null)
            return;
        LogHandler(LogType.Warning, content, args);
    }
    private static void LogHandler(LogType logType, object obj)
    {
        System.DateTime dateTime = Tool.GetUtcDateTime(LogUtcHour);


        string log = dateTime.ToString(LogTimeFm);
        log += obj == null ? "Null" : obj.ToString();
        switch (logType)
        {
            case LogType.Log:
                Debug.Log(log);
                break;
            case LogType.Warning:
                Debug.LogWarning(log);
                break;
            case LogType.Error:
                Debug.LogError(log);
                break;
        }
    }
    private static void LogHandler(LogType logType, string content, object[] args = null)
    {

        System.DateTime dateTime = Tool.GetUtcDateTime(LogUtcHour);
        string log = dateTime.ToString(LogTimeFm);
        log += args == null
            ? content
            : string.Format(content, args);
        switch (logType)
        {
            case LogType.Log:
                Debug.Log(log);
                break;
            case LogType.Warning:
                Debug.LogWarning(log);
                break;
            case LogType.Error:
                Debug.LogError(log);
                break;
        }
    }



}
