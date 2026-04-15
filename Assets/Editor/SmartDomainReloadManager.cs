using UnityEngine;
using UnityEditor;
using UnityEditor.Compilation;

[InitializeOnLoad]
public class SmartDomainReloadManager : EditorWindow
{
    public static bool SmartReloadActive;
    public static bool CodeHasChanged;
    public const string PrefKeyActive = "SmartDomainReload_Active";

    static SmartDomainReloadManager()
    {
        CompilationPipeline.compilationFinished += OnCompilationFinished;
        EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
        EditorApplication.delayCall += LoadPreferencesSafe;
    }

    public static void LoadPreferencesSafe()
    {
        SmartReloadActive = EditorPrefs.GetBool(PrefKeyActive, true);
        UpdateUnitySettings();
    }

    [MenuItem("Code Explode Studios Tools/Smart Domain Reload")]
    public static void ShowWindow()
    {
        SmartDomainReloadManager WindowInstance = GetWindow<SmartDomainReloadManager>("Smart Reload");
        WindowInstance.Show();
    }

    public void OnGUI()
    {    
    GUIStyle TitleStyle = new GUIStyle(EditorStyles.boldLabel)
    {
        fontSize = 16,
        alignment = TextAnchor.MiddleCenter,
        fixedHeight = 25
    };
    GUILayout.Space(5);
    GUILayout.Label("CODE EXPLODE STUDIOS", TitleStyle);

    GUIStyle CenteredLabelStyle = new GUIStyle(EditorStyles.label)
    {
        alignment = TextAnchor.MiddleCenter
    };

    string StatusMessage = CodeHasChanged ? "Code Changed (Requires Reload)" : "Cached (Instant Play)";
    GUILayout.Label("Current Cache Status: " + StatusMessage, CenteredLabelStyle);

    GUILayout.Space(15);

    Color DefaultGUIColor = GUI.backgroundColor;

    if (SmartReloadActive == true)
    {
        GUI.backgroundColor = Color.green;
        if (GUILayout.Button("Disable Smart Cache", GUILayout.Height(30)))
        {
            SmartReloadActive = false;
            EditorPrefs.SetBool(PrefKeyActive, SmartReloadActive);
            UpdateUnitySettings();
        }
    }
    else
    {
        GUI.backgroundColor = Color.red;
        if (GUILayout.Button("Enable Smart Cache", GUILayout.Height(30)))
        {
            SmartReloadActive = true;
            EditorPrefs.SetBool(PrefKeyActive, SmartReloadActive);
            UpdateUnitySettings();
        }
    }

    GUI.backgroundColor = DefaultGUIColor;
    GUILayout.Space(10);

    GUI.backgroundColor = Color.gray;
    if (GUILayout.Button("Force Clear Cache (Force Reload)", GUILayout.Height(30)))
    {
        CodeHasChanged = true;
        UpdateUnitySettings();
    }

    GUI.backgroundColor = DefaultGUIColor;
    }

    public static void OnCompilationFinished(object CompilerData)
    {
        CodeHasChanged = true;
        UpdateUnitySettings();
    }

    public static void OnPlayModeStateChanged(PlayModeStateChange StateChange)
    {
        if (StateChange == PlayModeStateChange.EnteredPlayMode)
        {
            CodeHasChanged = false;
            UpdateUnitySettings();
        }
    }

    public static void UpdateUnitySettings()
    {
        if (SmartReloadActive == false)
        {
            EditorSettings.enterPlayModeOptionsEnabled = false;
            return;
        }

        EditorSettings.enterPlayModeOptionsEnabled = true;

        if (CodeHasChanged == true)
        {
            EditorSettings.enterPlayModeOptions &= ~EnterPlayModeOptions.DisableDomainReload;
        }
        else
        {
            EditorSettings.enterPlayModeOptions |= EnterPlayModeOptions.DisableDomainReload;
        }
    }
}