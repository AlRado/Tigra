#region

using UnityEngine;

#endregion

public class Line : Tic80 {

  const float pi8 = Mathf.PI / 8;
  const float pi2 = Mathf.PI * 2;

  private float tt;

  public override void init () {
    tt = 0;
  }

  public override void TIC () {
    cls ();

    //lines
    for (var i = tt % 8; i < 135; i += 8) {
      line (i, 0, 0, 135 - i, 8);
      line (i, 135, 135, 135 - i, 6);
      tt += 0.01f;
    }

    //prism
    for (var i = tt / 16 % pi8; i < pi2; i += pi8) {
      var x = 68 + 32 * Mathf.Cos (i);
      var y = 68 + 32 * Mathf.Cos (i);
      line (135, 0, x, y, 15);
      line (0, 135, x, y, 15);
    }

    // Border
    line (0, 0, 135, 0, 8);
    line (0, 0, 0, 135, 8);
    line (135, 0, 135, 135, 6);
    line (0, 135, 135, 135, 6);
  }

}