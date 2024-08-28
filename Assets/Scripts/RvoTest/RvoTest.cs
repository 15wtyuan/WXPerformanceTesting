using System.Collections.Generic;
using RVO;
using UnityEngine;

public class RvoTest : MonoBehaviour
{
    public int col;
    public int row;
    public GameObject prefab;

    public Simulator Simulator { get; private set; }

    private bool _isProcessObstacles;

    // Start is called before the first frame update
    private void Awake()
    {
        Simulator = new Simulator();
        Simulator.setTimeStep(1.0f);
        Simulator.setAgentDefaults(5f, 5, 1.0f, 5.0f, 0.4f, 1f,
            new RVO.Vector2(0.0f, 0.0f));
        Simulator.obstacles_ = new List<Obstacle>();

        var myCamera = FindObjectOfType<Camera>();
        UnityEngine.Vector2 oriPos = myCamera.ScreenToWorldPoint(new Vector3(0, Screen.height, 0));
        for (var i = 0; i < col; i++)
        {
            for (var j = 0; j < row; j++)
            {
                var go = Instantiate(prefab);
                go.transform.position = oriPos + new UnityEngine.Vector2(j * 0.8f, i * 0.8f);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!_isProcessObstacles)
        {
            Simulator.processObstacles();
            _isProcessObstacles = true;
        }

        Simulator.doStep();
    }

    // private void FixedUpdate()
    // {
    //     Simulator.doStep();
    // }
}