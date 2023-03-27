using System;
using System.Collections.Generic;
using System.Text;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// ChatGPT��API���������N���X
/// https://note.com/negipoyoc/n/n88189e590ac3 �����p
/// </summary>
public class ChatGPTConnection
{
    //��b������ێ����郊�X�g
    private readonly List<ChatGPTMessageModel> _messageList = new();
    string _apikey;

    public ChatGPTConnection(string messageContent,string apikey)
    {
        _messageList.Add(
            new ChatGPTMessageModel() { role = "system", content = messageContent });
        _apikey = apikey;
    }

    public async UniTask<string> RequestAsync(string userMessage)
    {
        //���͐���AI��API�̃G���h�|�C���g��ݒ�
        var apiUrl = "https://api.openai.com/v1/chat/completions";

        _messageList.Add(new ChatGPTMessageModel { role = "user", content = userMessage });

        //OpenAI��API���N�G�X�g�ɕK�v�ȃw�b�_�[����ݒ�
        var headers = new Dictionary<string, string>
            {
                {"Authorization", "Bearer " + _apikey},
                {"Content-type", "application/json"},
                {"X-Slack-No-Retry", "1"}
            };

        //���͐����ŗ��p���郂�f����g�[�N������A�v�����v�g���I�v�V�����ɐݒ�
        var options = new ChatGPTCompletionRequestModel()
        {
            model = "gpt-3.5-turbo",
            messages = _messageList
        };
        var jsonOptions = JsonUtility.ToJson(options);

        Debug.Log("����:" + userMessage);

        //OpenAI�̕��͐���(Completion)��API���N�G�X�g�𑗂�A���ʂ�ϐ��Ɋi�[
        using var request = new UnityWebRequest(apiUrl, "POST")
        {
            uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(jsonOptions)),
            downloadHandler = new DownloadHandlerBuffer()
        };

        foreach (var header in headers)
        {
            request.SetRequestHeader(header.Key, header.Value);
        }
        try
        {
            await request.SendWebRequest();
            var responseString = request.downloadHandler.text;
            var responseObject = JsonUtility.FromJson<ChatGPTResponseModel>(responseString);
            Debug.Log("ChatGPT:" + responseObject.choices[0].message.content);
            _messageList.Add(responseObject.choices[0].message);
            return responseObject.choices[0].message.content;
        }
        catch(UnityWebRequestException exception)
        {
            Debug.LogWarning(request.error);
            Debug.Log("ChatGPT:" + "�������O����");
            return "�������O����";
        }
        

        if (request.result == UnityWebRequest.Result.ConnectionError ||
            request.result == UnityWebRequest.Result.ProtocolError)
        {

            //throw new Exception();
        }
        else
        {

        }
    }
}

[Serializable]
public class ChatGPTMessageModel
{
    public string role;
    public string content;
}

//ChatGPT API��Request�𑗂邽�߂�JSON�p�N���X
[Serializable]
public class ChatGPTCompletionRequestModel
{
    public string model;
    public List<ChatGPTMessageModel> messages;
}

//ChatGPT API�����Response���󂯎�邽�߂̃N���X
[System.Serializable]
public class ChatGPTResponseModel
{
    public string id;
    public string @object;
    public int created;
    public Choice[] choices;
    public Usage usage;

    [System.Serializable]
    public class Choice
    {
        public int index;
        public ChatGPTMessageModel message;
        public string finish_reason;
    }

    [System.Serializable]
    public class Usage
    {
        public int prompt_tokens;
        public int completion_tokens;
        public int total_tokens;
    }
}
