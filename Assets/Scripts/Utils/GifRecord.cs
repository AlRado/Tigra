using Moments;
using UnityEngine;
using ThreadPriority = System.Threading.ThreadPriority;

  // It automatically records the last few seconds of gameplay and lets you save to a GIF file on demand.
  // If you do not need to save Gif, please disable it.
public class GifRecord : MonoBehaviour {
  [Range (1, 30)] [Tooltip("Does not changed in runtime")]
  public int FramesPerSecond = 15;
  [Range (1, 30)] [Tooltip("Does not changed in runtime")]
  public int RecordTime = 3;
  [Range (1, 3)] [Tooltip("Does not changed in runtime")]
  public int Scale = 1;

  [HideInInspector]
  public int Quality = 1;
  [HideInInspector]
  public ThreadPriority WorkerPriority = ThreadPriority.Highest;

  private Recorder m_Recorder;
  private float m_Progress = 0f;
  private string m_LastFile = "";
  private bool lastSavingState = false;
  private bool m_IsSaving = false;

#if UNITY_EDITOR
  void OnEnable () {
    m_Recorder = View.Instance.GetGifRecorder ();
    m_Recorder.Setup (false, Tic80Config.SCREEN_AND_BORDER_WIDTH * Scale, Tic80Config.SCREEN_AND_BORDER_HEIGHT * Scale, FramesPerSecond, RecordTime, 0, Quality);
    m_Recorder.WorkerPriority = WorkerPriority;
    m_Recorder.Record ();

    m_Recorder.OnPreProcessingDone = OnProcessingDone;
    m_Recorder.OnFileSaveProgress = OnFileSaveProgress;
    m_Recorder.OnFileSaved = OnFileSaved;
  }

  void OnProcessingDone () {
    m_IsSaving = true;
  }

  void OnFileSaveProgress (int id, float percent) {
    m_Progress = percent * 100f;
    Debug.Log ("Gif progress: " + (int) m_Progress);
  }

  void OnFileSaved (int id, string filepath) {
    m_LastFile = filepath;
    m_IsSaving = false;
    m_Recorder.Record ();
  }

  void Update () {
    if (lastSavingState && !m_IsSaving) {
      Debug.Log ("Gif saved to: " + m_LastFile);
      UnityEditor.EditorUtility.RevealInFinder (m_LastFile);
    }
    lastSavingState = m_IsSaving;
  }

  public void SaveGif () {
    m_Recorder.Save ();
    m_Progress = 0f;
  }
#endif
}