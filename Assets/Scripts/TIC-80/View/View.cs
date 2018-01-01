#region
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Moments;

#endregion

public class View : SingletonPrefab<View> {

  [SerializeField]
  private RawImage Border;

  [SerializeField]
  private RawImage Screen;

  [SerializeField]
  private Recorder GifRecorder;

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

  public Recorder GetGifRecorder () {
    return GifRecorder;
  }

}