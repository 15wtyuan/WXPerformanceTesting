using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsTest : MonoBehaviour
{
    public int col;
    public int row;
    public GameObject prefab;
    
    // Start is called before the first frame update
    void Start()
    {
        var myCamera = FindObjectOfType<Camera>();
        Vector2 oriPos = myCamera.ScreenToWorldPoint(new Vector3(0, Screen.height, 0));
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
        
    }
}
