using RVO;
using UnityEngine;

public class RvoAgent : MonoBehaviour
{
    public float speed;
    private Simulator _simulator;
    private int _rvoId;

    private void Start()
    {
        var rvoTest = FindObjectOfType<RvoTest>();
        if (rvoTest)
        {
            _simulator = rvoTest.Simulator;
            _rvoId = _simulator.addAgent(transform.position.ToRvoVector2());
        }
    }

    private void Update()
    {
        if (_simulator == null)
        {
            return;
        }

        var rvoPos = _simulator.getAgentPosition(_rvoId);
        transform.position = rvoPos.ToUnityVector2();

        if (TargetPos.TryGetTargetPos(out var pos))
        {
            var prefVelocity = (pos - (UnityEngine.Vector2)transform.position).normalized * (Time.deltaTime * speed);
            _simulator.setAgentPrefVelocity(_rvoId, prefVelocity.ToRvoVector2());
        }
        else
        {
            _simulator.setAgentPrefVelocity(_rvoId, new RVO.Vector2());
        }
    }
}