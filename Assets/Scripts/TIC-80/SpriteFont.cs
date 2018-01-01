using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpriteFont : SingletonPrefab<SpriteFont>  {

    public class SprChar {
      public char Char;
      public int[] Data;
      public float Width;
    }

    public List<Sprite> Chars;

    private Dictionary<char, SprChar> FontChars = new Dictionary<char, SprChar> ();

    private void OnEnable () {
      FontChars = Chars.Select (x => new SprChar () {
        Char = Convert.ToChar (Int32.Parse (x.name)),
          Data = GetSprColorsIxs (x),
          Width = x.textureRect.width
      }).
      ToDictionary (x => x.Char);
    }

    public SprChar GetCharData (char @char) {
      return FontChars.ContainsKey ( @char) ? FontChars[@char] : null;
    }

    private int[] GetSprColorsIxs (Sprite x){
      var colors = GetSprColors (x);
      var colorsIxs = colors.Select (c => Palettes.GetColorIx (c, Tic80Config.DEFAULT_PALETTE));
      return colorsIxs.ToArray ();
    }

    private Color[] GetSprColors (Sprite x){
     return x.texture.GetPixels (
        (int) x.textureRect.x,
        (int) x.textureRect.y,
        (int) x.textureRect.width,
        (int) x.textureRect.height);
    }

}