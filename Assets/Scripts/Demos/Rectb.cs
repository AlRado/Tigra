#region

using UnityEngine;

#endregion

public class Rectb : Tic80 {

  private const float HALF_SCR_W = 240 / 2;
  private const float HALF_SCR_H = 136 / 2;
  private const float DEVIATION = 150;
  private const float SPEED = 1000;
  private const float RECT_COUNT = 70;
  private const float RECT_STEP = 4;
  private const int RECT_COLOR = 8;

  public override void init () {

  }

  public override void TIC () {
    cls ();
    for (var i = 1; i < RECT_COUNT; i++) {
      var width = i * RECT_STEP;
      var height = width / 2;
      float slowedTime = time () / SPEED;
      float x = Mathf.Sin (slowedTime) * DEVIATION / i - width / 2;
      float y = Mathf.Cos (slowedTime) * DEVIATION / i - height / 2;
      rectb (HALF_SCR_W + x, HALF_SCR_H + y, width, height, RECT_COLOR);
    }
  }

}