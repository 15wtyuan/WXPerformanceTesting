using UnityEngine;
using UnityEngine.SceneManagement;

public class FPS : MonoBehaviour
{
    private string _sceneName;
    private readonly Rect _labelRect = new(30, 30, 100, 30);
    private GUIStyle _guiStyle;
    private int _frameCount;
    private float _timeCount;
    private float _frameRate;

    private const float Interval = 0.5f;

    private void Awake()
    {
        _guiStyle = new GUIStyle
        {
            fontSize = 50,
            normal =
            {
                textColor = new Color(256f / 256f, 0 / 256f, 0 / 256f, 256f / 256f)
            }
        };

        var scene = SceneManager.GetActiveScene();
        _sceneName = scene.name;
    }

    private void Update()
    {
        _frameCount++;
        _timeCount += Time.unscaledDeltaTime;
        if (_timeCount >= Interval)
        {
            _frameRate = _frameCount / _timeCount;
            _frameCount = 0;
            _timeCount -= Interval;
        }
    }


    private void OnGUI()
    {
        GUI.Label(_labelRect, $"{_sceneName} FPS：{_frameRate:F1}", _guiStyle);
    }
}