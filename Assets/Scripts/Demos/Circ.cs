#region

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#endregion

public class Circ : Tic80 {

  class Ball {
    public float x;
    public float y;
    public float dx;
    public float dy;
    public int r;
    public int c;
  }

  private Ball[] balls = new Ball[50];

  private int d;

  public override void init () {
    d = 1;
    for (var i = 0; i < balls.Length; i++) {
      var ball = new Ball () {
      x = Random.Range (10, 220),
      y = Random.Range (10, 126),
      dx = Random.Range (1, 2) * d,
      dy = Random.Range (1, 2) * d,
      r = Random.Range (6, 12),
      c = Random.Range (1, 6)
      };
      balls[i] = ball;
      d *= -1;
    }
  }

  public override void TIC () {
    cls ();
    foreach (var b in balls) {
      //move the ball
      b.x = b.x + b.dx;
      b.y = b.y + b.dy;
      //check right/left walls
      if (b.x >= 240 - b.r) {
        b.x = 240 - b.r - 1; //constraints inside the wall
        b.dx = -b.dx; //reverse direction
      } else if (b.x < b.r) {
        b.x = b.r;
        b.dx = -b.dx;
      }
      //check bottom/top walls
      if (b.y >= 136 - b.r) {
        b.y = 136 - b.r - 1;
        b.dy = -b.dy;
      } else if (b.y < b.r) {
        b.y = b.r;
        b.dy = -b.dy;
      }
      //draw balls
      circ (b.x, b.y, b.r, b.c);
      circ (b.x + b.r / 4, b.y - b.r / 4, b.r / 4, b.c + 7);
    }
  }

}