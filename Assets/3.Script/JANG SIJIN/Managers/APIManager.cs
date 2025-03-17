using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System.Text;
using Newtonsoft.Json;
using Cysharp.Threading.Tasks;
using System;

public class APIManager : MonoBehaviour
{
    private static APIManager _instance;
    public static APIManager Instance => _instance;

    // "https://54.180.136.74:5123/api/auth"
    private const string BASE_URL = "https://localhost/api/auth"; // ✅ EC2 서버 URL

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// 회원가입 API 요청
    /// </summary>
    public async UniTask<string> Register(string email, string nickName, 
        string phoneNumber, string password, Func<UniTask> callback)
    {
        var registerData = new
        {            
            NickName = nickName,
            Email = email,
            PhoneNumber = phoneNumber,
            Password = password      
        };

        string jsonData = JsonConvert.SerializeObject(registerData);
        string response = await PostRequest($"{BASE_URL}/register", jsonData);
        
        if(response.StartsWith("Error:"))
        {
            throw new Exception(response);
        }
        
        if (callback != null)
            await callback();

        return response; // 정상 응답 반환
    }

    /// <summary>
    /// 로그인 API 요청
    /// </summary>
    public async UniTask<string> Login(string email, string password)
    {
        var loginData = new
        {           
            Email = email,
            Password = password            
        };


        string jsonData = JsonConvert.SerializeObject(loginData);
        string response = await PostRequest($"{BASE_URL}/login", jsonData);

        // API 응답이 "Error:"로 시작하면 예외가 발생된 것.
        if (response.StartsWith("Error:"))
        {
            throw new Exception(response);
        }

        return response; // 정상 응답 반환
    }
    
    /// 공통 POST 요청 메서드    
    private async UniTask<string> PostRequest(string url, string jsonData)
    {
        using (UnityWebRequest request = new UnityWebRequest(url, "POST"))
        {
            byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            var operation = request.SendWebRequest();
            while (!operation.isDone)
                await UniTask.Yield();

            if (request.result == UnityWebRequest.Result.Success)
            {
                return request.downloadHandler.text;
            }
            else
            {
                string responseBody = request.downloadHandler.text;
                return $"Error: {request.responseCode} {request.error}";
            }
        }
    }
}