﻿using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class Stats : MonoBehaviour {

  public int Fps {get; private set;}
  public int UsedMemory {get {return (int) (UnityEngine.Profiling.Profiler.GetTotalAllocatedMemoryLong () / 1024f / 1024f);}}

  private int counter;
  private float lastTime;

  protected virtual void Awake () {
    counter = 0;
    lastTime = Time.time;
  }

  private void Update () {
    if (Time.time - lastTime < 1) {
      ++counter;
    } else {
      Fps = counter;
      counter = 0;
      lastTime = Time.time;
    }
  }

}