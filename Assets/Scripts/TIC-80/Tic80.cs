#region

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Profiling;
using UnityEngine.UI;

#endregion

[RequireComponent (typeof (Tic80Config))]
public abstract class Tic80 : MonoBehaviour {

  private Dictionary<int, Color[]> clsColors = new Dictionary<int, Color[]> ();
  private Dictionary<int, Color[]> borderColors = new Dictionary<int, Color[]> ();

  private Texture2D screenTexture;

  private Texture2D rawScreenTexture;

  private Texture2D borderTexture;
  private Text textField;

  private int ticCounter;
  private bool isInited;

  private Tic80Config tic80Config;

  private RectTransform cachedTextRectTransform;
  private int[] mouseStates = new int[3];
  private int btnpStartTic;
  private int btnpDuration = 1;
  
  public void OnEnable () {
    if (this.enabled) {
      var scripts = GetComponents<Tic80> ();
      foreach (var item in scripts) {
        if (item != this) item.enabled = false;
      }
    }

    Application.targetFrameRate = Tic80Config.FRAMERATE;
    QualitySettings.vSyncCount = 0;
    Screen.SetResolution(Tic80Config.SCREEN_AND_BORDER_WIDTH, Tic80Config.SCREEN_AND_BORDER_HEIGHT, false);

    screenTexture = View.Instance.GetScreenTexture ();
    borderTexture = View.Instance.GetBorderTexture ();
    textField = View.Instance.GetTextField ();
    textField.text = "";
    rawScreenTexture = View.Instance.GetRawScreenTexture ();

    tic80Config = GetComponent<Tic80Config> ();
    if (!isInited) tic80Config.OnFontChange += OnFontChange;
    if (!isInited) tic80Config.OnPaletteChange += OnPaletteChange;

#if UNITY_EDITOR
      GameViewUtils.InitTIC80Size ((int) tic80Config.ViewScale);
#endif

    init ();
    border ();

    isInited = true;
  }

  private void OnDestroy () {
    if (tic80Config != null) tic80Config.OnFontChange -= OnFontChange;
    if (tic80Config != null) tic80Config.OnPaletteChange -= OnPaletteChange;
  }

  private void OnFontChange () {
    textField.font = Fonts.GetFont (Tic80Config.Instance.Font);
  }

  private void OnPaletteChange () {
    clsColors.Clear ();
    borderColors.Clear ();
    border ();
    cls ();
  }

  private void FixedUpdate () {
    if (!enabled || !isInited) return;

    if (tic80Config.DebugEnabled) {
      print ("FPS: " + View.Instance.Stats.Fps + "\nMemory: " + View.Instance.Stats.UsedMemory, 0, 0, 6);
    } else {
      print ("");
    }

    TIC ();
    screenTexture.Apply ();
    ticCounter++;

    StartCoroutine (CopyToRawScreenCoroutine ());
  }

  private IEnumerator CopyToRawScreenCoroutine () {
    yield return new WaitForEndOfFrame ();

    rawScreenTexture.ReadPixels (new Rect (Tic80Config.BORDER_WIDTH, Tic80Config.BORDER_HEIGHT, Tic80Config.WIDTH, Tic80Config.HEIGHT), Tic80Config.BORDER_WIDTH, Tic80Config.BORDER_HEIGHT);       
    rawScreenTexture.Apply ();
  }

  protected Color[] getClsColors (int colorIx) {
    Color[] colors;
    if (clsColors.TryGetValue (colorIx, out colors)) return colors;

    colors = getColors (Tic80Config.WIDTH * Tic80Config.HEIGHT, colorIx);
    clsColors.Add (colorIx, colors);

    return colors;
  }

  protected Color[] getBorderColors (int colorIx) {
    Color[] colors;
    if (borderColors.TryGetValue (colorIx, out colors)) return colors;

    colors = getColors (Tic80Config.BORDER_TEXTURE_WIDTH * Tic80Config.BORDER_TEXTURE_HEIGHT, colorIx);
    borderColors.Add (colorIx, colors);

    return colors;
  }

  private Color[] getColors (int len, int colorIx) {
    var color = GetColor (colorIx);
    var colors = new Color[len];
    for (int i = 0; i < colors.Length; i++) {
      colors[i] = color;
    }
    return colors;
  }

  private Color GetColor (int colorIx) {
    return Palettes.GetColor (colorIx, tic80Config.Palette);
  }

  private bool IsOffScreen (float x, float y) {
    return x < 0 || x >= Tic80Config.WIDTH || y < 0 || y >= Tic80Config.HEIGHT;
  }

  #region API

  // Специальные функции
  /**
   * TIC это главная функция. Она вызывается с частотой 60 fps (60 раз в секунду).
   * У неё нет параметров и она является местом, в котором и совершается вся магия.
   * Это единственная функция, которая обязательно должна присутствовать в коде.
   * 
   * https://nesbox.gitbooks.io/tic-80-rus/content/api/special/tic.html
   * 
   * TIC is the main function. It's call at 60 fps (60 times every second).
   * https://github.com/nesbox/TIC-80/wiki/tic
   */
  public abstract void TIC ();

  /**
   * scanline это callback функция, как и главная функция TIC, но вызывается системой после рендера каждой СТРОКИ.
   * Идея в том, что можно менять палитру в этот момент. Т.е. имея 136 строк * 16 цветов получим 2176 цветов, которые можно отобразить за один кадр. 
   * Этот трюк называется "scanline trick" и использовался раньше на приставках, для увеличения количества цветов. 
   * 
   * https://nesbox.gitbooks.io/tic-80-rus/content/api/special/scanline.html
   * 
   * scanline() is called on every line render and allows you to execute some code between each line, like for scanline color trick
   * https://github.com/nesbox/TIC-80/wiki/scanline
   */
  public void scanline (int line) {
    trace ("function 'scanline' not implemented");
  }

  /**
   * По дизайну в TIC-80 имеется только одна обязательная функция ядра. Это функция TIC.
   * Однако, многие пользователи хотели бы иметь функцию инициализации, вызываемую один раз перед выполнением основного кода.
   * 
   * https://nesbox.gitbooks.io/tic-80-rus/content/api/special/init.html
   * 
   * TIC has only two core functions in it's design. The tic function and the scanline function.
   * Anyway many user would like to have a init function, called only one time at the beginning of the code execution. 
   * https://github.com/nesbox/TIC-80/wiki/init
   */
  public abstract void init ();

  // Опрос ввода/вывода
  /**
   * Функция опрашивает состояние кнопки подключенной к TIC. Вернет true если кнопка нажата.
   *
   * id кнопок первого джойстика: вверх=0, вниз=1, влево=2, вправо=3, кнопа A=4, кнопка B=5, кнопка X=6, кнопка Y=7
   *
   * https://nesbox.gitbooks.io/tic-80-rus/content/api/io/btn.html
   * 
   * This function allow to read the status of one of the buttons attached to TIC. The function return true when the key interrogated using its id, is pressed.
   * https://github.com/nesbox/TIC-80/wiki/btn
   */
  public bool btn (int id) {
    return Input.GetKey (Keys.GetKey (id));
  }

  /**
   * Эта функция позволяет читать статус одной из кнопок, задействованных в TIC.
   * Функция возвращает значение true только в момент нажатия на клавишу.
   * Она также может быть использована с параметрами hold и period, которые позволяют возвращать true во время нажатия кнопки. После того как время нажатия hold закончится, функция вернет true каждый раз когда закончится время указанного периода period.
   * Время выражается в тиках: при 60 fps это означает, что 120 тиков равны 2 секундам.
   * 
   * https://nesbox.gitbooks.io/tic-80-rus/content/api/io/btnp.html
   * 
   * This function allow to read the status of one of the buttons attached to TIC. The function return true value only in the moment the key is pressed.
   * https://github.com/nesbox/TIC-80/wiki/btnp
   */
  public bool btnp (int id, int hold=1, int period=1) {
    var keyCode = Keys.GetKey (id);
    if (Input.GetKeyDown (keyCode)) {
      btnpStartTic = ticCounter - 1;
      btnpDuration = hold;
      return true;
    }

    if (Input.GetKey (keyCode) && (ticCounter - btnpStartTic) % btnpDuration == 0) {
      btnpDuration = period;
      return true;
    }

    return false;
  }

  /**
   * Функция возвращает координаты мыши и состояние нажатия.
   * Для использования этой функции в метаданных картриджа должно быть прописано -- input: mouse.
   * 
   * https://nesbox.gitbooks.io/tic-80-rus/content/api/io/mouse.html
   *
   * This function returns mouse coordinates and pressed state.
   * https://github.com/nesbox/TIC-80/wiki/mouse
   */
  public int[] mouse () {
    mouseStates[0] = (int) Input.mousePosition.x - Screen.width/2 + Tic80Config.WIDTH/2;
    mouseStates[1] =  Tic80Config.HEIGHT - (int) Input.mousePosition.y + Screen.height/2 - Tic80Config.HEIGHT/2;
    mouseStates[2] = Input.GetMouseButton (0) ? 1 : 0;

    return mouseStates;
  }

  // Вывод текста
  /**
   * Просто печатает текст на экран, используя системный шрифт, заданный в файле конфигурации.
   * Может печатать многострочный текст - для переноса строки используйте \n.
   * Для печати специально заданным шрифтом, воспользуйтесь оператором font.
   * Для печати в консоль воспользуйтесь оператором trace.
   * 
   * https://nesbox.gitbooks.io/tic-80-rus/content/api/text/print.html
   * 
   * This will simply print text to the screen using the font defined in config.
   * https://github.com/nesbox/TIC-80/wiki/print
   */
  public int print (string text, float x = 0, float y = 0, int colorIx = 15, bool @fixed = false, int scale = 1) {
    // в данной реализации работает только с фиксированным размером fixed=true
    // пока можно использовать print только один раз :( 
    textField.text = text;
    textField.color = GetColor (colorIx);
    if (!cachedTextRectTransform) cachedTextRectTransform = textField.gameObject.GetComponent<RectTransform> ();
    cachedTextRectTransform.anchoredPosition = new Vector2 (x, -y);

    var firstLine = text;
    int charLocation = text.IndexOf ('\n');
    if (charLocation != -1) firstLine = text.Substring (0, charLocation);

    return firstLine.Length * Tic80Config.FONT_WIDTH;
  }

  /**
   * Печатает текст на экран, используя пользовательский шрифт, заданный в области спрайтов переднего плана (FG).
   * Может печатать многострочный текст - для переноса строки используйте \n.
   * 
   * https://nesbox.gitbooks.io/tic-80-rus/content/api/text/font.html
   * 
   * This function will draw text to the screen using part of the spritesheet as the font.
   * https://github.com/nesbox/TIC-80/wiki/font
   */
  public int font (float x, float y, int colorKey, int charWidth, int charHeight, int scale) {
    trace ("function 'font' not implemented");
    return 0;
  }

  /**
   * Просто печатает текст на экран, используя системный шрифт, заданный в файле конфигурации.
   * Может печатать многострочный текст - для переноса строки используйте \n.
   * Для печати специально заданным шрифтом, воспользуйтесь оператором font.
   * Для печати в консоль воспользуйтесь оператором trace.
   * 
   * https://nesbox.gitbooks.io/tic-80-rus/content/api/text/print.html
   * 
   * This is a service function useful to debug your code. It will print back in the console the parameter passed.
   * https://github.com/nesbox/TIC-80/wiki/trace
   */
  public void trace (object msg) {
    Debug.Log (msg.ToString ());
  }

  // Вывод графики
  /**
   * Выводит на экран спрайт по указанному индексу в определенные координаты.
   * Вы можете выбрать цвет из палитры, который будет прозрачным. 
   * 
   * Используйте -1 для отсутствия прозрачных пикселей
   * 
   * https://nesbox.gitbooks.io/tic-80-rus/content/api/graphics/spr.html
   * 
   * It will put the sprite number index in the x and y coordinate.
   * https://github.com/nesbox/TIC-80/wiki/spr
   */
  public void spr (int id, float x, float y, int alpha_color = -1, int scale = 1, int flip = 0, int rotate = 0, int cell_width = 1, int cell_height = 1) {
    trace ("function 'spr' not implemented");
  }

  /**
   * Карта измеряется в ячейках, блоки 8x8 пикселей, куда вы можете поставить тайл в редакторе карты тайлов. 
   * Функция может печатать всю карту либо часть её. 
   * Максимальный размер карты ограничен значением 240x136 ячеек. 
   * 
   * https://nesbox.gitbooks.io/tic-80-rus/content/api/graphics/map.html
   * 
   * The map is measured in cells, 8x8 blocks where you can place sprites in the map editor.
   * https://github.com/nesbox/TIC-80/wiki/map
   */
  public void map (float cell_x = 0, float cell_y = 0, float cell_w = 30, float cell_h = 17, float x = 0, float y = 0, int alpha_color = -1, int scale = 1, Action<int, float, float> remap = null) {
    trace ("function 'map' not implemented");
  }

  /**
   * Заполняет память указанным значением.
   * Адрес указывается в шестнадцатеричном формате, но возвращаемое значение в десятичном.
   * 
   * https://nesbox.gitbooks.io/tic-80-rus/content/api/memory/memset.html
   * 
   * This function will change the sprite in map as specified coordinates.
   * https://github.com/nesbox/TIC-80/wiki/mset
   */
  public void memset (int dst, int val, int size) {
    trace ("function 'memset' not implemented");
  }

  /**
   * Считывает индекс тайла по указанным координатам на карте тайлов. 
   * Этот индекс соответствует индексу спрайта, который размещен в спрайт-листе графики заднего плана, индексы 0-255.
   * 
   * https://nesbox.gitbooks.io/tic-80-rus/content/api/graphics/mget.html
   * 
   * This function returns the sprite id at the given x and y map coordinate
   * https://github.com/nesbox/TIC-80/wiki/mget
   */
  public int mget (float cell_x, float cell_y) {
    trace ("function 'mget' not implemented");
    return 0;
  }

  /**
   * textri - отображает треугольник, заполненный текстурой
   *
   * https://nesbox.gitbooks.io/tic-80-rus/content/api/graphics/textri.html
   *
   * It renders a triangle filled with texture from image ram or map ram
   * https://github.com/nesbox/TIC-80/wiki/textri
   */
  public void textri (float x1, float y1, float x2, float y2, float x3, float y3, float u1, float v1, float u2, float v2, float u3, float v3, bool use_map = false, int alpha_color = -1) {
    trace ("function 'textri' not implemented");
  }

  // Рисование
  /**
   * Функция рисует цветной пиксель по указанным координатам.
   * Также может использоваться только для получения значения цвета пикселя на экране. 
   *
   * https://nesbox.gitbooks.io/tic-80-rus/content/api/draw/pix.html
   * 
   * This function color a pixel at the coordinates specified.
   * https://github.com/nesbox/TIC-80/wiki/pix
   */
  public void pix (float x, float y, int colorIx) {
    if (IsOffScreen (x, y)) return;

    screenTexture.DrawPixel (x, y, GetColor (colorIx));
  }

  /**
   * Используется для получения значения цвета пикселя на экране. 
   *
   * https://nesbox.gitbooks.io/tic-80-rus/content/api/draw/pix.html
   * 
   * The function can be used also to interrogate the color of a pixel in the screen.
   * https://github.com/nesbox/TIC-80/wiki/pix
   */
  public int pix (float x, float y) {
    if (IsOffScreen (x, y)) return 0;

    var color = rawScreenTexture.TakePixel(x + Tic80Config.BORDER_WIDTH, y +Tic80Config.BORDER_HEIGHT);
    return Palettes.GetColorIx (color, tic80Config.Palette);
  }

  /**
   * Рисует прямую цветную линию начиная с координат (x0,y0) до (x1,y1).
   * 
   * https://nesbox.gitbooks.io/tic-80-rus/content/api/draw/line.html
   * 
   * It draw a straight colored line from (x0,y0) point to (x1,y1) point.
   * https://github.com/nesbox/TIC-80/wiki/line
   */
  public void line (float x0, float y0, float x1, float y1, int colorIx) {
    screenTexture.DrawLine (x0, y0, x1, y1, GetColor (colorIx));
  }

  /**
   * Рисует заполненный цветной круг с центром x и y с указанным радиусом. Используется алгоритм "bresenham".
   * 
   * https://nesbox.gitbooks.io/tic-80-rus/content/api/draw/circ.html
   * 
   * It draw a filled circle with center x and y of the radius requested. It use bresenham algorithm.
   * https://github.com/nesbox/TIC-80/wiki/circ
   */
  public void circ (float x, float y, float radius, int colorIx) {
    screenTexture.DrawFilledCircle (x, y, radius, GetColor (colorIx));
  }

  /**
   * Рисует цветную окружность с центром x и y с указанным радиусом. Используется алгоритм "bresenham".
   * 
   * https://nesbox.gitbooks.io/tic-80-rus/content/api/draw/circb.html
   * 
   * It draw a circumference with center x and y of the radius requested. It use bresenham algorithm.
   * https://github.com/nesbox/TIC-80/wiki/circb
   */
  public void circb (float x, float y, float radius, int colorIx) {
    screenTexture.DrawCircle (x, y, radius, GetColor (colorIx));
  }

  /**
   * Эта функция рисует цветной заполненный прямоугольник по указанным координатам.
   * Если Вам нужно отрисовать только рамку, используйте функцию rectb
   * 
   * https://nesbox.gitbooks.io/tic-80-rus/content/api/draw/rect.html
   * 
   * This function draw a colored filled rectangle at the position request. If you need to draw only the border see rectb
   * https://github.com/nesbox/TIC-80/wiki/rect
   */
  public void rect (float x, float y, float w, float h, int colorIx) {
    if (x + w >= Tic80Config.WIDTH) w = w - (x + w - Tic80Config.WIDTH);
    if (y + h >= Tic80Config.HEIGHT) h = h - (y + h - Tic80Config.HEIGHT);

    if (w < 0 || h < 0) return;

    screenTexture.DrawFilledRectangle (x, y, w, h, GetColor (colorIx));
  }

  /**
   * Эта функция рисует цветную рамку-прямоугольник по указанным координатам.
   * Если Вам нужно отрисовать заполненный цветом прямоугольник, используйте функцию rect
   * 
   * https://nesbox.gitbooks.io/tic-80-rus/content/api/draw/rectb.html
   * 
   * This function draw a rectangle border of one pixel size at the position request.
   * If you need to fill the rectangle with a color see rect instead.
   * https://github.com/nesbox/TIC-80/wiki/rectb
   */
  public void rectb (float x, float y, float w, float h, int colorIx) {
    if (x + w >= Tic80Config.WIDTH) w = w - (x + w - Tic80Config.WIDTH);
    if (y + h >= Tic80Config.HEIGHT) h = h - (y + h - Tic80Config.HEIGHT);

    if (w < 0 || h < 0) return;

    screenTexture.DrawRectangle (x, y, w, h, GetColor (colorIx));
  }

  /**
   * Рисует треугольник заполненный цветом.
   * 
   * https://nesbox.gitbooks.io/tic-80-rus/content/api/draw/tri.html
   * 
   * It draw a triangle filled with color
   * https://github.com/nesbox/TIC-80/wiki/tri
   */
  public void tri (float x1, float y1, float x2, float y2, float x3, float y3, int colorIx) {
    screenTexture.DrawFilledTriangle(x1, y1, x2, y2, x3, y3, GetColor (colorIx));
  }

  /**
  trib - рисование треугольника (нет в API).
  */
  public void trib (float x1, float y1, float x2, float y2, float x3, float y3, int colorIx) {
    screenTexture.DrawTriangle(x1, y1, x2, y2, x3, y3, GetColor (colorIx));
  }

  // Экран
  /**
   * При вызове этой функции очищается весь экран и заливается цветом указанном в качестве параметра. По умолчанию, используется первый цвет (индекс = 0). 
   * Как правило, вызывается из функции TIC, но это не является обязательным условием. Вы можете сделать какие-нибудь странные эффекты либо мерцающий экран используя её.
   * 
   * https://nesbox.gitbooks.io/tic-80-rus/content/api/screen/cls.html
   * 
   * When called this function clear all the screen using the color passed as argument.
   * https://github.com/nesbox/TIC-80/wiki/cls
   */
  public void cls (int colorIx = 0) {
    screenTexture.SetPixels (0, 0, Tic80Config.WIDTH, Tic80Config.HEIGHT, getClsColors (colorIx));
  }

  /**
   * Unofficial
   */
  public void border (int colorIx = 0) {
    borderTexture.SetPixels (0, 0, Tic80Config.BORDER_TEXTURE_WIDTH, Tic80Config.BORDER_TEXTURE_HEIGHT, getBorderColors (colorIx));
    borderTexture.Apply ();
  }

  /**
   * Эта функция ограничивает то, что рисуется на экране параметрами ограничивающего прямоугольника. 
   * Всё что выходит за границы, не будет отображаться на экране.
   * 
   * https://nesbox.gitbooks.io/tic-80-rus/content/api/screen/clip.html
   * 
   * This function will limit what is drawn to the screen by x,y,w,h. Things drawn outside of the parameters will not be drawn to the screen.
   * https://github.com/nesbox/TIC-80/wiki/clip
   */
  public void clip (float x, float y, float width, float height) {
    trace ("function 'clip' not implemented");
  }

  // Память
  /**
   * Эта функция позволяет читать значения памяти TIC - байт.
   * Она удобна для доступа к ресурсам, созданным с помощью интегрированных средств, таких как спрайты, карты, звуки, данные картриджа.
   * 
   * Адрес указывается в шестнадцатеричном формате, но возвращаемое значение в десятичном.
   * 
   * https://nesbox.gitbooks.io/tic-80-rus/content/api/memory/peek.html
   * 
   * This function allow to read the memory from TIC.
   * https://github.com/nesbox/TIC-80/wiki/peek
   */
  public int peek (int addr) {
    trace ("function 'peek' not implemented");
    return 0;
  }

  /**
   * Эта функция позволяет записывать значения в память TIC - байт.
   * 
   * Адрес указывается в шестнадцатеричном формате, но возвращаемое значение в десятичном.
   * 
   * https://nesbox.gitbooks.io/tic-80-rus/content/api/memory/poke.html
   * 
   * This function allows you to write to the virtual RAM of TIC.
   * https://github.com/nesbox/TIC-80/wiki/poke
   */
  public int poke (int addr, int val) {
    trace ("function 'poke' not implemented");
    return 0;
  }

  /**
   * Эта функция позволяет читать значения памяти TIC - полубайт.
   * Она используется для доступа к ресурсам, созданным с помощью интегрированных средств, таких например как спрайты в спрайт-листе.
   * Адрес указывается в шестнадцатеричном формате, но возвращаемое значение в десятичном.
   * Стоит также отметить, что peek4 и poke4 оперируют полубайтами (4 бита), поэтому адрес умножается на два по отношению к обычным peek и poke, которые оперируют байтами (8 бит).
   * 
   * https://nesbox.gitbooks.io/tic-80-rus/content/api/memory/peek4.html
   * 
   * Read a half byte value from RAM
   * https://github.com/nesbox/TIC-80/wiki/peek4
   */
  public int peek4 (int addr) {
    trace ("function 'peek4' not implemented");
    return 0;
  }

  /**
   * Эта функция позволяет записывать значения в память TIC - полубайт.
   * С её помощью можно, например, записывать значения пикселей в спрайты спрайт-листа.
   * Адрес указывается в шестнадцатеричном формате, значение в десятичном.
   * Стоит также отметить, что peek4 и poke4 оперируют полубайтами (4 бита), поэтому адрес умножается на два по отношению к обычным peek и poke, которые оперируют байтами (8 бит).
   * 
   * https://nesbox.gitbooks.io/tic-80-rus/content/api/memory/poke4.html
   * 
   * Write a half byte value to RAM
   * https://github.com/nesbox/TIC-80/wiki/poke4
   */
  public int poke4 (int addr, int val) {
    trace ("function 'poke4' not implemented");
    return 0;
  }

  /**
   * Копирует блок памяти указанного размера из одной области в другую. 
   * Адрес указывается в шестнадцатеричном формате, но возвращаемое значение в десятичном.
   * 
   * https://nesbox.gitbooks.io/tic-80-rus/content/api/memory/memcpy.html
   * 
   * This function allow to copy a continuous block of the RAM memory of TIC to an another address. 
   * https://github.com/nesbox/TIC-80/wiki/memcpy
   */
  public void memcpy (int dst, int src, int size) {
    trace ("function 'memcpy' not implemented");
  }

  /**
   * Эта функция позволяет сохранять и получать данные одного из 7 доступных слотов в постоянной памяти. 
   * Она полезна, чтобы сохранить таблицу достижений и любого рода продвижения. 
   * 
   * https://nesbox.gitbooks.io/tic-80-rus/content/api/memory/pmem.html
   * 
   * This function allow to save and retrieve data in one of the 7 slots available in the persistent memory.
   * https://github.com/nesbox/TIC-80/wiki/pmem
   */
  public void pmem (int index, int val) {
    trace ("function 'pmem' not implemented");
  }
  public void pmem (int index) {
    trace ("function 'pmem' not implemented");
  }

  // Звуки
  /**
   * Воспроизводит звуковой эффект по указанному id и параметрам. 
   * Для того чтобы остановить воспроизведение звукового эффекта, нужно указать id равный -1 в том же канале.
   * 
   * https://nesbox.gitbooks.io/tic-80-rus/content/api/sound/sfx.html
   * 
   * This function will play a sound from the sfx editor. 
   * https://github.com/nesbox/TIC-80/wiki/sfx
   */
  public void sfx (int id, int note, int duration = -1, int channel = 0, int volume = 15, int speed = 0) {
    trace ("function 'sfx' not implemented");
  }
  public void sfx (int id, string note, int duration = -1, int channel = 0, int volume = 15, int speed = 0) {
    trace ("function 'sfx' not implemented");
  }

  /**
   * Воспроизводит музыкальный трек по указанному индексу трека. Для того чтобы остановить воспроизведение, нужно вызвать эту функцию без аргументов.
   * 
   * https://nesbox.gitbooks.io/tic-80-rus/content/api/sound/music.html
   * 
   * It starts playing the track created in the Music Editor. Call without arguments to stop the music.
   * https://github.com/nesbox/TIC-80/wiki/music
   */
  public void music (int track = -1, int frame = -1, bool loop = true) {
    trace ("function 'music' not implemented");
  }

  // Другое
  /**
   * Функция возвращает количество миллисекунд прошедших с начала запуска приложения. 
   * Полезно при отслеживании времени, анимации объектов и событиях изменяющихся во времени.
   * 
   * https://nesbox.gitbooks.io/tic-80-rus/content/api/others/time.html
   * 
   * The function return elapsed time from the start of the cartridge expressed in milliseconds. 
   * https://github.com/nesbox/TIC-80/wiki/time
   */
  public float time () {
    return Time.time * 1000;
  }

  /**
   * Данная функция используется для сохранения изменений в спрайтах/карте тайлов во время игры, иначе данные возвращаются к исходному состоянию. 
   * 
   * https://nesbox.gitbooks.io/tic-80-rus/content/api/others/sync.html
   * 
   * Sprite/map data restores on every startup. Call sync() api to save sprite/map data modified during runtime.
   * https://github.com/nesbox/TIC-80/wiki/sync
   */
  public void sync (bool toCart = true) {
    trace ("function 'sync' not implemented");
  }

  /**
   * Данная функция используется для выхода из программы в консоль.
   *
   * https://nesbox.gitbooks.io/tic-80-rus/content/api/others/exit.html
   *
   * Interrupt program execution and return to the console at the END of the TIC function.
   * https://github.com/nesbox/TIC-80/wiki/exit
   */
  public void exit () {
    Debug.Break ();
  }

  #endregion

}