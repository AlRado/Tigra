using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Tic80Config : MonoBehaviour {

  public const string NAME = "TIC-80";

  public const string TIC80_API_VERSION = "0.50.1";

  public const int WIDTH = 240;
  public const int HEIGHT = 136;

  public const int BORDER_WIDTH = 8;
  public const int BORDER_HEIGHT = 4;

  public const int SCREEN_AND_BORDER_WIDTH = WIDTH + BORDER_WIDTH * 2;
  public const int SCREEN_AND_BORDER_HEIGHT = HEIGHT + BORDER_HEIGHT * 2;

  public const int FRAMERATE = 60;

  public const int BORDER_TEXTURE_WIDTH = 8;
  public const int BORDER_TEXTURE_HEIGHT = 244;

  public const int FONT_HEIGHT = 6;
  public const int FONT_WIDTH = 6;

  public const int SPRITE_SIZE = 8;
  public const int SPRITE_COLUMN_COUNT = 16;

  public const Palettes.Palette DEFAULT_PALETTE = Palettes.Palette.DB16;

  public const string COVER_PATH = "Assets/Resources/Covers/";

  public bool Stats;

  public Action OnPaletteChange;
  [SerializeField]
  private Palettes.Palette _palette;
  public Palettes.Palette Palette {
    get { return _palette; }
    set {
      _palette = value;
      if (OnPaletteChange != null) OnPaletteChange ();
    }
  }

  public Action OnFontChange;
  [SerializeField]
  private Fonts.FontType _font;
  public Fonts.FontType Font {
    get { return _font; }
    set {
      _font = value;
      if (OnFontChange != null) OnFontChange ();
    }
  }

  [SerializeField]
  private ViewSize.Scale _viewScale = ViewSize.Scale.x2;
  public ViewSize.Scale ViewScale {
    get { return _viewScale; }
    set {
      _viewScale = value;
    }
  }

  public static Tic80Config Instance { get; private set; }

  private void Awake () {
    if (Instance) {
      enabled = false;
      return;
    }
    Instance = this;
  }

  private void OnValidate () {
    Palette = _palette;
    Font = _font;
    ViewScale = _viewScale;
#if UNITY_EDITOR
    if (Instance) {
      GameViewUtils.InitTIC80Size ((int) ViewScale);
    }
#endif

  }

}