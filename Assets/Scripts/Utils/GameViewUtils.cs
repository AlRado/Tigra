using System;
using UnityEngine;

#if UNITY_EDITOR
using System.Reflection;
using UnityEditor;
#endif

public class GameViewUtils {

#if UNITY_EDITOR

  private static object gameViewSizesInstance;
  private static MethodInfo getGroup;
  private static bool isInited;

  public enum GameViewSizeType {
    AspectRatio,
    FixedResolution
  }

  private static void Init () {
    if (isInited) return;

    var sizesType = typeof (Editor).Assembly.GetType ("UnityEditor.GameViewSizes");
    var singleType = typeof (ScriptableSingleton<>).MakeGenericType (sizesType);
    var instanceProp = singleType.GetProperty ("instance");
    getGroup = sizesType.GetMethod ("GetGroup");
    gameViewSizesInstance = instanceProp.GetValue (null, null);
    isInited = true;
  }

  public static void InitTIC80Size (int scale) {
    AddSize (Tic80Config.NAME, Tic80Config.SCREEN_AND_BORDER_WIDTH, Tic80Config.SCREEN_AND_BORDER_HEIGHT);
    SetSize (Tic80Config.SCREEN_AND_BORDER_WIDTH, Tic80Config.SCREEN_AND_BORDER_HEIGHT);
    SetScale (scale);
  }

  public static void AddSize (string name, int width, int height) {
    GameViewSizeGroupType[] viewTypes = (GameViewSizeGroupType[]) Enum.GetValues (typeof (GameViewSizeGroupType));
    foreach (var type in viewTypes) {
      if (SizeExists (type, Tic80Config.NAME)) continue;

      AddSize (GameViewSizeType.FixedResolution, type, width, height, name);
    }
  }

  public static void SetSize (int width, int height) {
    var gameViewSizeGroupType = GameViewSizeGroupType.Standalone;
    var targetName = EditorUserBuildSettings.activeBuildTarget.ToString ();

    GameViewSizeGroupType[] viewTypes = (GameViewSizeGroupType[]) Enum.GetValues (typeof (GameViewSizeGroupType));
    foreach (var type in viewTypes) {
      if (targetName.Contains (type.ToString ())) {
        gameViewSizeGroupType = type;
        break;
      }
    }

    int ix = FindSize (gameViewSizeGroupType, width, height);
    if (ix != -1) SetSize (ix);
  }

  public static void SetSize (int index) {
    var gameViewType = typeof (Editor).Assembly.GetType ("UnityEditor.GameView");
    var gameViewWindow = EditorWindow.GetWindow (gameViewType);

    var selectedSizeIndexProperty = gameViewType.GetProperty ("selectedSizeIndex",
      BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
    selectedSizeIndexProperty.SetValue (gameViewWindow, index, null);
  }

  public static void SetScale (int scale) {
    var gameViewType = typeof (Editor).Assembly.GetType ("UnityEditor.GameView");
    var gameViewWindow = EditorWindow.GetWindow (gameViewType);
    var areaField = gameViewType.GetField ("m_ZoomArea", BindingFlags.Instance | BindingFlags.NonPublic);
    var areaObj = areaField.GetValue (gameViewWindow);
    var scaleField = areaObj.GetType ().GetField ("m_Scale", BindingFlags.Instance | BindingFlags.NonPublic);
    scaleField.SetValue (areaObj, new Vector2 (scale, scale));
  }

  public static void AddSize (GameViewSizeType gameWiewSizeType, GameViewSizeGroupType gameViewSizeGroupType, int width, int height, string name) {
    var group = GetGroup (gameViewSizeGroupType);
    var addCustomSize = getGroup.ReturnType.GetMethod ("AddCustomSize");
    var gameViewSizeType = typeof (Editor).Assembly.GetType ("UnityEditor.GameViewSize");
    var constructor = gameViewSizeType.GetConstructor (new Type[] { typeof (int), typeof (int), typeof (int), typeof (string) });
    var newSize = constructor.Invoke (new object[] {
      (int) gameWiewSizeType, width, height, name
    });
    addCustomSize.Invoke (group, new object[] { newSize });
  }

  public static bool SizeExists (GameViewSizeGroupType sizeGroupType, string name) {
    return FindSize (sizeGroupType, name) != -1;
  }

  public static int FindSize (GameViewSizeGroupType sizeGroupType, string text) {
    var group = GetGroup (sizeGroupType);
    var getDisplayTexts = group.GetType ().GetMethod ("GetDisplayTexts");
    var displayTexts = getDisplayTexts.Invoke (group, null) as string[];
    for (int i = 0; i < displayTexts.Length; i++) {
      string display = displayTexts[i];
      int pren = display.IndexOf ('(');
      if (pren != -1) display = display.Substring (0, pren - 1);
      if (display == text) return i;
    }
    return -1;
  }

  public static bool SizeExists (GameViewSizeGroupType sizeGroupType, int width, int height) {
    return FindSize (sizeGroupType, width, height) != -1;
  }

  public static int FindSize (GameViewSizeGroupType sizeGroupType, int width, int height) {
    var group = GetGroup (sizeGroupType);
    var groupType = group.GetType ();
    var getBuiltinCount = groupType.GetMethod ("GetBuiltinCount");
    var getCustomCount = groupType.GetMethod ("GetCustomCount");
    int sizesCount = (int) getBuiltinCount.Invoke (group, null) + (int) getCustomCount.Invoke (group, null);
    var getGameViewSize = groupType.GetMethod ("GetGameViewSize");
    var gvsType = getGameViewSize.ReturnType;
    var widthProp = gvsType.GetProperty ("width");
    var heightProp = gvsType.GetProperty ("height");
    var indexValue = new object[1];
    for (int i = 0; i < sizesCount; i++) {
      indexValue[0] = i;
      var size = getGameViewSize.Invoke (group, indexValue);
      int sizeWidth = (int) widthProp.GetValue (size, null);
      int sizeHeight = (int) heightProp.GetValue (size, null);
      if (sizeWidth == width && sizeHeight == height) return i;
    }
    return -1;
  }

  static object GetGroup (GameViewSizeGroupType type) {
    Init ();
    return getGroup.Invoke (gameViewSizesInstance, new object[] {
      (int) type
    });
  }

  public static GameViewSizeGroupType GetCurrentGroupType () {
    var getCurrentGroupTypeProp = gameViewSizesInstance.GetType ().GetProperty ("currentGroupType");
    return (GameViewSizeGroupType) (int) getCurrentGroupTypeProp.GetValue (gameViewSizesInstance, null);
  }

#endif
}