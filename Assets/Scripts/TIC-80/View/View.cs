#region
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#endregion

public class View : SingletonPrefab<View> {

  [SerializeField]
  private RawImage Border;

  [SerializeField]
  private RawImage Screen;

  [SerializeField]
  private RawImage RawScreen;

  [SerializeField]
  private Text Text;

  public Stats Stats {get; private set;}

  protected override void Awake () {
    base.Awake ();
    var cameras = FindObjectsOfType<Camera> ();
    // remove other cameras
    foreach (var camera in cameras) {
      if (camera.gameObject.transform.parent != gameObject.transform) Destroy (camera.gameObject);
    }

    Stats = gameObject.AddComponent<Stats> ();
  }

  public Texture2D GetScreenTexture () {
    if (Screen.texture == null) Screen.texture = new Texture2D (Tic80Config.WIDTH, Tic80Config.HEIGHT);

    return Screen.texture as Texture2D;
  }

  public Texture2D GetBorderTexture () {
    if (Border.texture == null) Border.texture = new Texture2D (Tic80Config.BORDER_TEXTURE_WIDTH, Tic80Config.BORDER_TEXTURE_HEIGHT);

    return Border.texture as Texture2D;
  }

  public Texture2D GetRawScreenTexture () {
    if (RawScreen.texture == null) RawScreen.texture = new Texture2D (Tic80Config.SCREEN_AND_BORDER_WIDTH, Tic80Config.SCREEN_AND_BORDER_HEIGHT);

    return RawScreen.texture as Texture2D;
  }

  public Text GetTextField () {
    return Text;
  }

}