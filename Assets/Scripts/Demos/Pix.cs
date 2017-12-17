public class Pix : Tic80 {

  private float t = 0;

  public override void init () {
    //Draw some background
    cls (0);
    for (var i = 0; i < 15; i++) {
      rect (9 * i, 6 * i, 6 * i, 3 * i, i);
    }
  }

  public override void TIC () {
    if (t > 12) { //wait some time
      t = 0;
      for (var x = 0; x < 240; x += 2) { //every 2 pixel in width
        for (var y = 0; y < 136; y += 2) { //every 2 pixel in height
          var c = pix (x, y); //take color
          c = (c + 1) % 15; //change it
          pix (x, y, c); //put it back
        }
      }
    }
    t++;

  }

}