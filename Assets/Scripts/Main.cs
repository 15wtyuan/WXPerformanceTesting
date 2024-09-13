using System.Collections;
using UnityEngine;
using YooAsset;

/// <summary>
/// 远端资源地址查询服务类
/// </summary>
public class RemoteServices : IRemoteServices
{
    private readonly string _defaultHostServer;
    private readonly string _fallbackHostServer;

    public RemoteServices(string defaultHostServer, string fallbackHostServer)
    {
        _defaultHostServer = defaultHostServer;
        _fallbackHostServer = fallbackHostServer;
    }

    string IRemoteServices.GetRemoteMainURL(string fileName)
    {
        return $"{_defaultHostServer}/{fileName}";
    }

    string IRemoteServices.GetRemoteFallbackURL(string fileName)
    {
        return $"{_fallbackHostServer}/{fileName}";
    }
}

public class Main : MonoBehaviour
{
    public string packageName = "res";
    public string hostServerURL;

    private bool _mainDone;

    IEnumerator Start()
    {
        GameDefine.PackageName = packageName;

        // 初始化
        yield return YooAssetInit();

        // 更新版本数据
        yield return YooAssetUpdatePackageVersion();

        _mainDone = true;
    }

    private IEnumerator YooAssetInit()
    {
        YooAssets.Initialize();
        // 创建资源包裹类
        var package = YooAssets.TryGetPackage(packageName) ?? YooAssets.CreatePackage(packageName);
        InitializationOperation initializationOperation;
#if UNITY_EDITOR
        // 编辑器下的模拟模式
        var simulateBuildResult =
            EditorSimulateModeHelper.SimulateBuild(EDefaultBuildPipeline.BuiltinBuildPipeline, packageName);
        var createParameters = new EditorSimulateModeParameters();
        createParameters.EditorFileSystemParameters =
            FileSystemParameters.CreateDefaultEditorFileSystemParameters(simulateBuildResult);
        initializationOperation = package.InitializeAsync(createParameters);

#elif UNITY_WEBGL && WEIXINMINIGAME
        // WebGL运行模式
        var defaultHostServer = hostServerURL;
        var fallbackHostServer = hostServerURL;
        IRemoteServices remoteServices = new RemoteServices(defaultHostServer, fallbackHostServer);
        var createParameters = new WebPlayModeParameters();
        createParameters.WebFileSystemParameters =
            WechatFileSystemCreater.CreateWechatFileSystemParameters(remoteServices);
        initializationOperation = package.InitializeAsync(createParameters);

#else
        // 联机运行模式
        var defaultHostServer = hostServerURL;
        var fallbackHostServer = hostServerURL;
        IRemoteServices remoteServices = new RemoteServices(defaultHostServer, fallbackHostServer);
        var createParameters = new HostPlayModeParameters();
        createParameters.BuildinFileSystemParameters =
            FileSystemParameters.CreateDefaultBuildinFileSystemParameters();
        createParameters.CacheFileSystemParameters =
            FileSystemParameters.CreateDefaultCacheFileSystemParameters(remoteServices);
        initializationOperation = package.InitializeAsync(createParameters);
#endif
        yield return initializationOperation;
    }

    private IEnumerator YooAssetUpdatePackageVersion()
    {
        var package = YooAssets.GetPackage(packageName);
        var requestPackageVersionOperation = package.RequestPackageVersionAsync();
        yield return requestPackageVersionOperation;

        var updatePackageManifestOperation =
            package.UpdatePackageManifestAsync(requestPackageVersionOperation.PackageVersion);
        yield return updatePackageManifestOperation;
    }

    public void RvoTest()
    {
        if (!_mainDone)
        {
            return;
        }

        StartCoroutine(LoadScene("RvoTest"));
    }

    public void PhysicsTest()
    {
        if (!_mainDone)
        {
            return;
        }

        StartCoroutine(LoadScene("PhysicsTest"));
    }

    private IEnumerator LoadScene(string sceneName)
    {
        var package = YooAssets.GetPackage(packageName);
        var handle = package.LoadSceneAsync(sceneName);
        yield return handle;
        Debug.Log($"Scene name is {handle.SceneName}");
    }
}