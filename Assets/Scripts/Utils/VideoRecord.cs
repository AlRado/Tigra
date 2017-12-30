using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Collections;

#if UNITY_EDITOR
using System;
using System.IO;
using UnityEditor;
using UnityEditor.Media;
#endif

public class VideoRecord : MonoBehaviour {
  [Range (1, 30)][Tooltip ("Does not changed in runtime")]
  public int FramesPerSecond = 15;
  [Range (1, 30)][Tooltip ("Does not changed in runtime")]
  public int RecordTime = 3;
  //TODO set SoundFreq when there is sound
  private int SoundFreq = 48000;

  private string encodedFilePath;

  void Start () {
    encodedFilePath = Application.dataPath + "/" + GenerateFileName () + ".mp4";
  }

  public void RecordMovie () {
#if UNITY_EDITOR
    StartCoroutine (recordCoroutine ());
    Debug.Log ("Video record started");
#endif
  }

#if UNITY_EDITOR

  private IEnumerator recordCoroutine () {
    var videoAttr = new VideoTrackAttributes {
      frameRate = new MediaRational (FramesPerSecond),
      width = Tic80Config.WIDTH,
      height = Tic80Config.HEIGHT,
      includeAlpha = false
    };

    var audioAttr = new AudioTrackAttributes {
      sampleRate = new MediaRational (SoundFreq),
      channelCount = 2,
      language = "en"
    };

    int sampleFramesPerVideoFrame = audioAttr.channelCount *
      audioAttr.sampleRate.numerator / videoAttr.frameRate.numerator;

    var tex = View.Instance.GetScreenTexture ();
    var frames = RecordTime * FramesPerSecond;
    var deltaTime = (float) 1 / FramesPerSecond;

    using (var encoder = new MediaEncoder (encodedFilePath, videoAttr, audioAttr))
    using (var audioBuffer = new NativeArray<float> (sampleFramesPerVideoFrame, Allocator.Persistent)) {
      for (int i = 0; i < frames; ++i) {
        encoder.AddFrame (tex);
        // Fill 'audioBuffer' with the audio content to be encoded into the file for this frame.
        encoder.AddSamples (audioBuffer);
        yield return deltaTime;
      }
    }
    Debug.Log ("Video saved to: " + encodedFilePath);
    EditorUtility.RevealInFinder (encodedFilePath);
  }

  private string GenerateFileName () {
    string timestamp = DateTime.Now.ToString ("yyyyMMddHHmmssffff");
    return "VideoCapture-" + timestamp;
  }

#endif

}