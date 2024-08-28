using System.Collections.Generic;
using UnityEngine;
using Vector2 = RVO.Vector2;

public class RvoBlock : MonoBehaviour
{
    private readonly Vector2[] _corners =
    {
        new(-0.5f, 0.5f),
        new(-0.5f, -0.5f),
        new(0.5f, -0.5f),
        new(0.5f, 0.5f),
    };

    private void Start()
    {
        var rvoTest = FindObjectOfType<RvoTest>();
        if (rvoTest)
        {
            var rvoList = new List<Vector2>();
            for (var i = 0; i < _corners.Length; i++)
            {
                rvoList.Add(new Vector2(
                    _corners[i].x() * transform.localScale.x + transform.position.x,
                    _corners[i].y() * transform.localScale.y + transform.position.y));
            }

            rvoTest.Simulator.addObstacle(rvoList);
        }
    }
}