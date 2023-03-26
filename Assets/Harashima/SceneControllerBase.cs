using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;
using Cysharp.Threading.Tasks;

public class SceneControllerBase : MonoBehaviour
{
    [SerializeField]
    Image _fadePanel;

    [SerializeField]
    string _nextSceneName = "SampleScene";

    private void Start()
    {
        _fadePanel.color = new Color(0, 0, 0, 0);
        OnStart();
    }

    protected virtual void OnStart()
    {

    }

    protected async UniTask SceneChangeAsync()
    {
        var sceneController = new SceneChange();
        var ct = new CancellationToken();
        await sceneController.SceneChangeAsync(_nextSceneName, _fadePanel, ct);
    }
}
