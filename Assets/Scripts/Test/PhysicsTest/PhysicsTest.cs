using System.Collections;
using UnityEngine;
using YooAsset;

public class PhysicsTest : MonoBehaviour
{
    public int col;
    public int row;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        var package = YooAssets.GetPackage(GameDefine.PackageName);
        var handle = package.LoadAssetAsync<GameObject>("Physics1000");
        yield return handle;

        var myCamera = FindObjectOfType<Camera>();
        Vector2 oriPos = myCamera.ScreenToWorldPoint(new Vector3(0, Screen.height, 0));
        for (var i = 0; i < col; i++)
        {
            for (var j = 0; j < row; j++)
            {
                var go = handle.InstantiateSync();
                go.transform.position = oriPos + new Vector2(j * 0.8f, i * 0.8f);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}