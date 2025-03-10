using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using SimpleJSON;

public class ChangeNickname : MonoBehaviour
{
    private string apiUrl = "https://localhost:7250/api/score/get/";
    public InputField inputField;
    public Text highScoreText;
    public Text lastScoreText;

    private void Awake()
    {
        inputField = GetComponent<InputField>();
        Application.targetFrameRate = 60;

        if (GameData.nickname != null)
        {
            inputField.text = GameData.nickname;
            StartCoroutine(GetScore(GameData.nickname));
        }
    }
    void Start()
    {
        if (inputField != null)
            inputField.onValueChanged.AddListener(OnInputChanged);
    }   

    void OnInputChanged(string newText)
    {
        if (!string.IsNullOrEmpty(newText))
        {
            GameData.nickname = newText;
            StartCoroutine(GetScore(newText));
        }
    }

    IEnumerator GetScore(string userName)
    {
        string url = apiUrl + $"{userName}";
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            request.SetRequestHeader("accept", "application/json");

            yield return request.SendWebRequest();

            string json = request.downloadHandler.text;
            var jsonData = JSON.Parse(json);

            if (request.result == UnityWebRequest.Result.ConnectionError ||
                request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError($"Error: {request.error}");
                highScoreText.text = jsonData["high_score"].ToString();
                lastScoreText.text = jsonData["last_score"].ToString();
            }
            else
            {
                highScoreText.text = jsonData["high_score"].ToString();
                lastScoreText.text = jsonData["last_score"].ToString();
            }
        }
    }
}
