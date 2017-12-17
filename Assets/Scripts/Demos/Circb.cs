#region

using UnityEngine;

#endregion

public class Circb : Tic80 {

  private float a;
  private int space;

  public override void init () {
    space = 10;
  }

  public override void TIC () {
    cls ();
    for (var i = 0; i < 200; i += space) {
      circb (120 + 80 * Mathf.Sin (a),
        68 + 40 * Mathf.Cos (a),
        i + time () / 40 % space,
        8);
      circb (120 + 80 * Mathf.Sin (a / 2),
        68 + 40 * Mathf.Cos (a / 2),
        i + time () / 40 % space,
        8);
    }
    a += Mathf.PI / 240;
  }

}