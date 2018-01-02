using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[ExecuteInEditMode]
public class SpriteFont : SingletonPrefab<SpriteFont> {

    [Serializable]
    public class FontItem {
      [HideInInspector]
      public string Name;
      [HideInInspector]
      public int[] Data;
      [HideInInspector]
      public float Width;
      [HideInInspector]
      public char Char;

      public Sprite Sprite;

      public FontItem (string name) {
        Name = name;
        Char = Convert.ToChar (Name);
      }
    }

    [Serializable][ExecuteInEditMode]
    public class Font {
      [HideInInspector]
      public string Name;

      [SerializeField]
      public Fonts.FontType Type;

      [SerializeField]
      public List<FontItem> Chars = new List<FontItem> ();

      private Dictionary<char, FontItem> charsDict = new Dictionary<char, FontItem>();

      public Font () {
        Name = Type.ToString ();
        if (Chars.Count == 0) {
          Chars.Add (new FontItem (" "));
          Chars.Add (new FontItem ("!"));
          Chars.Add (new FontItem ("\""));
          Chars.Add (new FontItem ("#"));
          Chars.Add (new FontItem ("$"));
          Chars.Add (new FontItem ("%"));
          Chars.Add (new FontItem ("&"));
          Chars.Add (new FontItem ("'"));
          Chars.Add (new FontItem ("("));
          Chars.Add (new FontItem (")"));
          Chars.Add (new FontItem ("*"));
          Chars.Add (new FontItem ("+"));
          Chars.Add (new FontItem (","));
          Chars.Add (new FontItem ("-"));
          Chars.Add (new FontItem ("."));
          Chars.Add (new FontItem ("/"));

          Chars.Add (new FontItem ("0"));
          Chars.Add (new FontItem ("1"));
          Chars.Add (new FontItem ("2"));
          Chars.Add (new FontItem ("3"));
          Chars.Add (new FontItem ("4"));
          Chars.Add (new FontItem ("5"));
          Chars.Add (new FontItem ("6"));
          Chars.Add (new FontItem ("7"));
          Chars.Add (new FontItem ("8"));
          Chars.Add (new FontItem ("9"));
          Chars.Add (new FontItem (":"));
          Chars.Add (new FontItem (";"));
          Chars.Add (new FontItem ("<"));
          Chars.Add (new FontItem ("="));
          Chars.Add (new FontItem (">"));
          Chars.Add (new FontItem ("?"));

          Chars.Add (new FontItem ("@"));
          Chars.Add (new FontItem ("A"));
          Chars.Add (new FontItem ("B"));
          Chars.Add (new FontItem ("C"));
          Chars.Add (new FontItem ("D"));
          Chars.Add (new FontItem ("E"));
          Chars.Add (new FontItem ("F"));
          Chars.Add (new FontItem ("G"));
          Chars.Add (new FontItem ("H"));
          Chars.Add (new FontItem ("I"));
          Chars.Add (new FontItem ("J"));
          Chars.Add (new FontItem ("K"));
          Chars.Add (new FontItem ("L"));
          Chars.Add (new FontItem ("M"));
          Chars.Add (new FontItem ("N"));
          Chars.Add (new FontItem ("O"));

          Chars.Add (new FontItem ("P"));
          Chars.Add (new FontItem ("Q"));
          Chars.Add (new FontItem ("R"));
          Chars.Add (new FontItem ("S"));
          Chars.Add (new FontItem ("T"));
          Chars.Add (new FontItem ("U"));
          Chars.Add (new FontItem ("V"));
          Chars.Add (new FontItem ("W"));
          Chars.Add (new FontItem ("X"));
          Chars.Add (new FontItem ("Y"));
          Chars.Add (new FontItem ("Z"));
          Chars.Add (new FontItem ("["));
          Chars.Add (new FontItem ("\\"));
          Chars.Add (new FontItem ("]"));
          Chars.Add (new FontItem ("^"));
          Chars.Add (new FontItem ("_"));

          Chars.Add (new FontItem ("`"));
          Chars.Add (new FontItem ("a"));
          Chars.Add (new FontItem ("b"));
          Chars.Add (new FontItem ("c"));
          Chars.Add (new FontItem ("d"));
          Chars.Add (new FontItem ("e"));
          Chars.Add (new FontItem ("f"));
          Chars.Add (new FontItem ("g"));
          Chars.Add (new FontItem ("h"));
          Chars.Add (new FontItem ("i"));
          Chars.Add (new FontItem ("j"));
          Chars.Add (new FontItem ("k"));
          Chars.Add (new FontItem ("l"));
          Chars.Add (new FontItem ("m"));
          Chars.Add (new FontItem ("n"));
          Chars.Add (new FontItem ("o"));

          Chars.Add (new FontItem ("p"));
          Chars.Add (new FontItem ("q"));
          Chars.Add (new FontItem ("r"));
          Chars.Add (new FontItem ("s"));
          Chars.Add (new FontItem ("t"));
          Chars.Add (new FontItem ("u"));
          Chars.Add (new FontItem ("v"));
          Chars.Add (new FontItem ("w"));
          Chars.Add (new FontItem ("x"));
          Chars.Add (new FontItem ("y"));
          Chars.Add (new FontItem ("z"));
          Chars.Add (new FontItem ("{"));
          Chars.Add (new FontItem ("|"));
          Chars.Add (new FontItem ("}"));
          Chars.Add (new FontItem ("~"));
        }
        charsDict = Chars.ToDictionary(x => x.Char);
      }

      public FontItem GetFontItem(char @char){
        return charsDict.ContainsKey(@char) ? charsDict[@char]: null;
      }

    }

    public List<Font> Fonts = new List<Font> ();

    private Dictionary<Fonts.FontType, Font> fontsDict = new Dictionary<Fonts.FontType, Font> ();

    private void OnEnable () {
      foreach (var font in Fonts) {
          foreach (var fontItem in font.Chars) {
            fontItem.Data = GetSprColorsIxs (fontItem.Sprite);
            fontItem.Width = fontItem.Sprite.textureRect.width;
          }
      }
      fontsDict = Fonts.ToDictionary(x=>x.Type);
    }

    public void AddFont () {
      Fonts.Add (new Font ());
    }

    public FontItem GetFontItem (char @char, Fonts.FontType type) {
      return fontsDict[type].GetFontItem (@char);
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