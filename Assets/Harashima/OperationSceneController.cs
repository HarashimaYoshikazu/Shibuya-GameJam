using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class OperationSceneController : SceneControllerBase
{
    [SerializeField]
    Button _cliclToNextButton;

    protected override void OnStart()
    {
        _cliclToNextButton.onClick.AddListener(async () =>
        {
            await SceneChangeAsync();
        });
    }
}
