using System;
using System.Collections.Generic;
using UnityEngine;

/**
 * Texture2D extension class thath realized draw into Texture2D.
 * Start position ( 0,0) is the left top corner.
 * Used Bresenham's algorithms.
 */
public static class Texture2DExtension {

  #region Public

  public static void DrawPixel (this Texture2D texture, float x, float y, Color32 color) {
    pixel (texture, x, y, texture.width, texture.height, color);
  }

  public static Color32 TakePixel (this Texture2D texture, float x, float y) {
    int _x = (int) x;
    int _y = (int) y;
    if (_x < 0 || _x >= texture.width || _y < 0 || _y >= texture.height) return Color.clear;

    return texture.GetPixel (_x, transformY (_y, texture.height));
  }

  public static void DrawLine (this Texture2D texture, float x0, float y0, float x1, float y1, Color32 color) {
    line (texture, x0, y0, x1, y1, color);
  }

  public static void DrawCircle (this Texture2D texture, float x, float y, float radius, Color32 color) {
    circle (texture, x, y, radius, color, false);
  }

  public static void DrawFilledCircle (this Texture2D texture, float x, float y, float radius, Color32 color) {
    circle (texture, x, y, radius, color, true);
  }

  public static void FloodFill (this Texture2D texture, float x, float y, Color32 color) {
    int width = texture.width;
    int height = texture.height;
    Point start = new Point ((int) x, transformY ((int) y, texture.height));
    TwoDimArrColors colorsCopy = new TwoDimArrColors (texture.width, texture.GetPixels32 ());
    Color32 originalColor = texture.GetPixel (start.x, start.y);

    if (System.Object.Equals (originalColor, color)) return;

    colorsCopy[start.x, start.y] = color;

    Queue<Point> nodes = new Queue<Point> ();
    nodes.Enqueue (start);

    int i = 0;
    int emergency = width * height;

    while (nodes.Count > 0) {
      i++;

      if (i > emergency) return;

      Point current = nodes.Dequeue ();
      int _x = current.x;
      int _y = current.y;

      if (_x > 0) {
        if (System.Object.Equals (colorsCopy[_x - 1, _y], originalColor)) {
          colorsCopy[_x - 1, _y] = color;
          nodes.Enqueue (new Point (_x - 1, _y));
        }
      }
      if (_x < width - 1) {
        if (System.Object.Equals (colorsCopy[_x + 1, _y], originalColor)) {
          colorsCopy[_x + 1, _y] = color;
          nodes.Enqueue (new Point (_x + 1, _y));
        }
      }
      if (_y > 0) {
        if (System.Object.Equals (colorsCopy[_x, _y - 1], originalColor)) {
          colorsCopy[_x, _y - 1] = color;
          nodes.Enqueue (new Point (_x, _y - 1));
        }
      }
      if (_y < height - 1) {
        if (System.Object.Equals (colorsCopy[_x, _y + 1], originalColor)) {
          colorsCopy[_x, _y + 1] = color;
          nodes.Enqueue (new Point (_x, _y + 1));
        }
      }
    }

    texture.SetPixels32 (colorsCopy.data);
  }

  public static void DrawRectangle (this Texture2D texture, float x, float y, float width, float height, Color32 color) {
    texture.DrawLine (x, y, x, y + height, color);
    texture.DrawLine (x, y + height, x + width, y + height, color);
    texture.DrawLine (x + width, y + height, x + width, y, color);
    texture.DrawLine (x + width, y, x, y, color);
  }

  public static void DrawFilledRectangle (this Texture2D texture, float x, float y, float width, float height, Color32 color) {
    int _width = (int) width;
    int _height = (int) height;

    int _x = (int) x;
    if (_x < 0) {
      _width += _x;
      _x = 0;
    }

    if (y < 0) {
      _height += (int) y;
      y = 0;
    }
    int _y = transformY ((int) y, texture.height) - _height + 1;

    if (_x + _width < 0 || y + _height < 0) return;

    Color32[] colors = new Color32[_width * _height];
    for (int i = 0; i < colors.Length; i++) {
      colors[i] = color;
    }

    texture.SetPixels32 (_x, _y, _width, _height, colors);
  }

  public static void DrawTriangle (this Texture2D texture, float x1, float y1, float x2, float y2, float x3, float y3, Color32 color) {
    texture.DrawLine (x1, y1, x2, y2, color);
    texture.DrawLine (x2, y2, x3, y3, color);
    texture.DrawLine (x3, y3, x1, y1, color);
  }

  public static void DrawFilledTriangle (this Texture2D texture, float x1, float y1, float x2, float y2, float x3, float y3, Color32 color) {
    Point vt1 = new Point ((int) x1, (int) y1);
    Point vt2 = new Point ((int) x2, (int) y2);
    Point vt3 = new Point ((int) x3, (int) y3);

    // sort vertices ascending by y 
    if (vt1.y > vt2.y) swap (ref vt1, ref vt2);
    if (vt1.y > vt3.y) swap (ref vt1, ref vt3);
    if (vt2.y > vt3.y) swap (ref vt2, ref vt3);

    if (vt2.y == vt3.y) {
      fillFlatSideTriangle (texture, vt1, vt2, vt3, color);
    } else if (vt1.y == vt2.y) {
      fillFlatSideTriangle (texture, vt3, vt1, vt2, color);
    } else {
      Point vTmp = new Point ((int) (vt1.x + ((float) (vt2.y - vt1.y) / (float) (vt3.y - vt1.y)) * (vt3.x - vt1.x)), vt2.y);
      fillFlatSideTriangle (texture, vt1, vt2, vTmp, color);
      fillFlatSideTriangle (texture, vt3, vt2, vTmp, color);
    }
  }

  #endregion

  #region Private

  private static void pixel (Texture2D texture, float x, float y, float width, float height, Color32 color) {
    int _x = (int) x;
    int _y = (int) y;
    int _width = (int) width;
    int _height = (int) height;

    if (_x < 0 || _x >= _width || _y < 0 || _y >= _height) return;

    texture.SetPixel (_x, transformY (_y, _height), color);
  }

  private static void circle (Texture2D texture, float x, float y, float radius, Color32 color, bool filled = false) {
    int cx = (int) radius;
    int cy = 0;
    int radiusError = 1 - cx;

    while (cx >= cy) {
      if (!filled) {
        texture.DrawPixel (cx + x, cy + y, color);
        texture.DrawPixel (cy + x, cx + y, color);
        texture.DrawPixel (-cx + x, cy + y, color);
        texture.DrawPixel (-cy + x, cx + y, color);
        texture.DrawPixel (-cx + x, -cy + y, color);
        texture.DrawPixel (-cy + x, -cx + y, color);
        texture.DrawPixel (cx + x, -cy + y, color);
        texture.DrawPixel (cy + x, -cx + y, color);
      } else {
        texture.DrawLine (cx + x, cy + y, -cx + x, cy + y, color);
        texture.DrawLine (cy + x, cx + y, -cy + x, cx + y, color);
        texture.DrawLine (-cx + x, -cy + y, cx + x, -cy + y, color);
        texture.DrawLine (-cy + x, -cx + y, cy + x, -cx + y, color);
      }

      cy++;

      if (radiusError < 0) {
        radiusError += 2 * cy + 1;
      } else {
        cx--;
        radiusError += 2 * (cy - cx + 1);
      }
    }
  }

  private class TwoDimArrColors {
    private int width;
    public Color32[] data;

    public TwoDimArrColors (int width, Color32[] data) {
      this.width = width;
      this.data = data;
    }

    public Color32 this [int x, int y] {
      get {
        return data[x + y * width];
      }
      set {
        data[x + y * width] = value;
      }
    }
  }

  private static void line (Texture2D texture, float x0, float y0, float x1, float y1, Color32 color) {
    int width = texture.width;
    int height = texture.height;
    int _x0 = (int) x0;
    int _y0 = (int) y0;
    int _x1 = (int) x1;
    int _y1 = (int) y1;

    bool isSteep = Math.Abs (_y1 - _y0) > Math.Abs (_x1 - _x0);
    if (isSteep) {
      swap (ref _x0, ref _y0);
      swap (ref _x1, ref _y1);
    }
    if (_x0 > _x1) {
      swap (ref _x0, ref _x1);
      swap (ref _y0, ref _y1);
    }

    int deltaX = _x1 - _x0;
    int deltaY = Math.Abs (_y1 - _y0);

    int correction = deltaX / 2;
    int y = _y0;
    int yStep = _y0 < _y1 ? 1 : -1;

    for (int x = _x0; x <= _x1; x++) {
      if (isSteep) {
        pixel (texture, y, x, width, height, color);
      } else {
        pixel (texture, x, y, width, height, color);
      }
      correction = correction - deltaY;
      if (correction < 0) {
        y = y + yStep;
        correction = correction + deltaX;
      }
    }
  }

  private static void fillFlatSideTriangle (Texture2D texture, Point v1, Point v2, Point v3, Color32 color) {
    Point vTmp1 = new Point (v1.x, v1.y);
    Point vTmp2 = new Point (v1.x, v1.y);

    bool changed1 = false;
    bool changed2 = false;

    int dx1 = Mathf.Abs (v2.x - v1.x);
    int dy1 = Mathf.Abs (v2.y - v1.y);

    int dx2 = Mathf.Abs (v3.x - v1.x);
    int dy2 = Mathf.Abs (v3.y - v1.y);

    int xSign1 = Math.Sign (v2.x - v1.x);
    int xSign2 = Math.Sign (v3.x - v1.x);

    int ySign1 = Math.Sign (v2.y - v1.y);
    int ySign2 = Math.Sign (v3.y - v1.y);

    if (dy1 > dx1) {
      swap (ref dx1, ref dy1);
      changed1 = true;
    }

    if (dy2 > dx2) {
      swap (ref dx2, ref dy2);
      changed2 = true;
    }

    int e1 = 2 * dy1 - dx1;
    int e2 = 2 * dy2 - dx2;

    for (int i = 0; i <= dx1; i++) {
      DrawLine (texture, vTmp1.x, vTmp1.y, vTmp2.x, vTmp2.y, color);

      while (e1 >= 0) {
        if (changed1)
          vTmp1.x += xSign1;
        else
          vTmp1.y += ySign1;
        e1 = e1 - 2 * dx1;
      }

      if (changed1)
        vTmp1.y += ySign1;
      else
        vTmp1.x += xSign1;

      e1 = e1 + 2 * dy1;

      while (vTmp2.y != vTmp1.y) {
        while (e2 >= 0) {
          if (changed2)
            vTmp2.x += xSign2;
          else
            vTmp2.y += ySign2;
          e2 = e2 - 2 * dx2;
        }

        if (changed2)
          vTmp2.y += ySign2;
        else
          vTmp2.x += xSign2;

        e2 = e2 + 2 * dy2;
      }
    }
  }

  private static void swap (ref int a, ref int b) {
    var temp = a;
    a = b;
    b = temp;
  }

  private static void swap (ref Point a, ref Point b) {
    var temp = a;
    a = b;
    b = temp;
  }

  private static int transformY (int y, int height) {
    return height - 1 - y;
  }

  public static int TransformY (this Texture2D texture, float y) {
    return texture.height - 1 - (int)y;
  }

  private struct Point {
    public int x;
    public int y;

    public Point (int x, int y) {
      this.x = x;
      this.y = y;
    }
  }

  #endregion

}