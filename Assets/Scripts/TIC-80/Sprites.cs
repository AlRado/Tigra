using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Sprites : SingletonPrefab<Sprites> {

  public class SpriteItem {
    public int Id;
    public byte[] Data;
    public float Width;

    public SpriteItem (Sprite sprite) {
      Id = Int32.Parse (sprite.name);
      Data = GetSprColorsIxs(sprite);
      Width = sprite.textureRect.width;
    }

    private byte[] GetSprColorsIxs (Sprite x) {
      var colors = GetSprColors (x);
      var colorsIxs = colors.Select (c => Palettes.GetColorIx (c, Tic80Config.DEFAULT_PALETTE)).ToArray ();
      return colorsIxs;
    }

    private Color[] GetSprColors (Sprite x) {
      return x.texture.GetPixels (
        (int) x.textureRect.x,
        (int) x.textureRect.y,
        (int) x.textureRect.width,
        (int) x.textureRect.height);
    }
  }

  public Sprite[] SpritesArray;

  private Dictionary<int, SpriteItem> SpritesDict = new Dictionary<int, SpriteItem> ();

  private void OnEnable () {
    SpritesDict = SpritesArray.Select (x => new SpriteItem (x)).ToDictionary (x => x.Id);
  }

  public SpriteItem GetSpriteItem (int id) {
    return SpritesDict.ContainsKey (id) ? SpritesDict[id] : null;
  }

}