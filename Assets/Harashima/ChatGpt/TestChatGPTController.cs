using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestChatGPTController : MonoBehaviour
{
    [SerializeField]
    Button _button;
    void Start()
    {
        _button.onClick.AddListener(async () =>
        {
            var chatGPTConnection = new ChatGPTConnection("dog","�����疼�O��񎦂���̂ł���ɑ΂��ă|�W�e�B�u�Ȕ�����15�����ȓ��ł��肢���܂��B�܂��A����Ɂu���v��t���Ă��������B");
            await chatGPTConnection.RequestAsync("");
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
