#region

using UnityEngine;

#endregion

public class Tri : Tic80 {

  public override void init () {

  }

  private void Pir (float x, float y, float w, float h, float cx, float cy) {
    tri (x, y, w / 2 + cx, h / 2 + cy, x + w, y, 1);
    tri (x + w, y, w / 2 + cx, h / 2 + cy, x + w, y + h, 2);
    tri (x, y, w / 2 + cx, h / 2 + cy, x, y + h, 8);
    tri (x, y + h, w / 2 + cx, h / 2 + cy, x + w, y + h, 15);
  }

  public override void TIC () {
    cls ();

    for (var x = 0; x < 240; x += 28) {
      for (var y = 0; y < 136; y += 28) {
        var cx = 12 * Mathf.Sin (time () / 30000 * (x + y + 1));
        var cy = 12 * Mathf.Cos (time () / 30000 * (x + y + 1));
        Pir (x, y, 25, 25, x + cx, y + cy);
      }
    }

  }

}