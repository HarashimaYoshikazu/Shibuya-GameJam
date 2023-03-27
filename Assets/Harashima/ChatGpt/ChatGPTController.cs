using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ChatGPTController : MonoBehaviour
{
    [SerializeField]
    Button _button;

    [SerializeField]
    InputField _inputField;
    void Start()
    {
        _button.onClick.AddListener(async () =>
        {
            if (_inputField == null || _inputField.text == "" || _inputField.text == null)
            {
                return;
            }
            var chatGPTConnection = new ChatGPTConnection("�����疼�O��񎦂���̂ł���ɑ΂��Ĉȉ��̏����ɉ�����������20�����ȓ��ŏo�͂��Ă��������B�E����Ɂu���v��t���Ă��������B�E�a�J�̒��Ɋ֘A����P������Ă��������B","");

            var responseModel = await chatGPTConnection.RequestAsync(_inputField.text);
        });
    }
}
