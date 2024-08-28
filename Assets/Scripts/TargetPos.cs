using UnityEngine;

public class TargetPos : MonoBehaviour
{
    private static readonly Vector2 InvalidPos = new(1000, 1000);
    private static Vector2 _targetPos = InvalidPos;

    public static void SetTargetPos(Vector2 pos)
    {
        _targetPos = pos;
    }

    public static bool TryGetTargetPos(out Vector2 pos)
    {
        pos = _targetPos;
        return !_targetPos.Equals(InvalidPos);
    }

    private Camera _camera;

    private void Awake()
    {
        _camera = GetComponent<Camera>();
    }

    private void Update()
    {
#if UNITY_EDITOR || UNITY_STANDALONE_WIN
        HandleMouse();
#else
        HandleTouch();
#endif
    }

    private void HandleTouch()
    {
        var touchCount = Input.touchCount;
        if (touchCount == 0)
        {
            _targetPos = InvalidPos;
        }
        else
        {
            var data = Input.GetTouch(0);
            _targetPos = _camera.ScreenToWorldPoint(data.position);
        }
    }

    private void HandleMouse()
    {
        if (Input.GetMouseButton(0))
        {
            _targetPos = _camera.ScreenToWorldPoint(Input.mousePosition);
        }
        else
        {
            _targetPos = InvalidPos;
        }
    }
}