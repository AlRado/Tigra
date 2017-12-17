using UnityEngine;

/**
  Autoload prefab by <Type>. Resources/Path = <Type>.
 */
public class SingletonPrefab<Type> : MonoBehaviour where Type : MonoBehaviour {
  private static Type _instance;
  public static Type Instance {
    get {
      Instantiate ();
      return _instance;
    }
  }

  public static void Instantiate () {
    if (Object.Equals (_instance, null)) {
      var prefab = Resources.Load<GameObject>(typeof (Type).Name);
      var go = Instantiate(prefab);
      DontDestroyOnLoad (go);
      _instance = go.GetComponent<Type> ();
    }
  }

  protected virtual void Awake () {
    if (_instance != null) {
      Destroy (gameObject);
      return;
    }
  }
}