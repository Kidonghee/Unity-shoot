using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using TMPro;

public class LoginManager : MonoBehaviour
{
    public TMP_InputField idInput;
    public TMP_InputField passwordInput;
    public TMP_Text messageText;

    public GameObject loginPanel;

    string serverUrl = "http://127.0.0.1:3000";

    public static string UserId;
    public static string Token;

    void Start()
    {
        Debug.Log("LoginManager 실행됨");

        // 게임 시작 시 멈춤
        Time.timeScale = 0f;
    }

    // ================= 버튼 =================

    public void OnClickRegister()
    {
        messageText.text = ""; // 이전 메시지 초기화
        StartCoroutine(Register());
    }

    public void OnClickLogin()
    {
        messageText.text = ""; // 이전 메시지 초기화
        StartCoroutine(Login());
    }

    // ================= 회원가입 =================

    IEnumerator Register()
    {
        string json = JsonUtility.ToJson(new LoginData
        {
            user_id = idInput.text,
            password = passwordInput.text
        });

        UnityWebRequest request = new UnityWebRequest(serverUrl + "/register", "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);

        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            messageText.text = "서버 연결 실패";
        }
        else
        {
            ResponseData response = JsonUtility.FromJson<ResponseData>(request.downloadHandler.text);
            messageText.text = response.message;
        }
    }

    // ================= 로그인 =================

    IEnumerator Login()
    {
        string json = JsonUtility.ToJson(new LoginData
        {
            user_id = idInput.text,
            password = passwordInput.text
        });

        UnityWebRequest request = new UnityWebRequest(serverUrl + "/login", "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);

        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            messageText.text = "서버 연결 실패";
        }
        else
        {
            LoginResponse response = JsonUtility.FromJson<LoginResponse>(request.downloadHandler.text);

            // 🔥 로그인 결과 메시지 출력
            messageText.text = response.message;

            if (response.success)
            {
                UserId = idInput.text;
                Token = response.token;

                Debug.Log("로그인 성공");
                Debug.Log("토큰: " + Token);

                // 🔥 로그인 UI 숨기기
                if (loginPanel != null)
                {
                    loginPanel.SetActive(false);
                }

                // 🔥 게임 시작
                Time.timeScale = 1f;
            }
        }
    }
}

// ================= 데이터 클래스 =================

[System.Serializable]
public class LoginData
{
    public string user_id;
    public string password;
}

[System.Serializable]
public class ResponseData
{
    public bool success;
    public string message;
}

[System.Serializable]
public class LoginResponse
{
    public bool success;
    public string message;
    public string token;
    public int score;
}