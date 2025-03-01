using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;
using SimpleJSON;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private string apiUrl = "https://localhost:7250/api/score/set?";

    [SerializeField] private int objectsNumber = 5;
    [SerializeField] private Text scoreText;
    [SerializeField] private float spawnInterval = 3f;
    [SerializeField] private float scoreUpdateInterval = 1f;
    [SerializeField] private Vector2 startObejctPosition = new Vector2(12.0f, -4f);
    [SerializeField] private GameObject prefab;

    private List<GameObject> objects = new List<GameObject>();
    private int score = 0;
    private int activeObjects = 0;

    private void OnEnable()
    {
        DinosaurMovement.OnPlayerHit += StopGame;
    }
    private void OnDisable()
    {
        DinosaurMovement.OnPlayerHit -= StopGame;
    }

    void Start()
    {
        InvokeRepeating(nameof(SpawnObject), 0f, spawnInterval);
        InvokeRepeating(nameof(UpdateScore), 0f, scoreUpdateInterval);
    }
    private void Update()
    {
        activeObjects = transform.childCount;
    }

    private void SpawnObject()
    {
        if(activeObjects < objectsNumber)
        {
            GameObject gameObject = Instantiate(prefab, startObejctPosition, Quaternion.identity, transform);
            objects.Add(gameObject);
        }
    }
    private void UpdateScore()
    {
        ++score;
        scoreText.text = score.ToString();
    }
    private void StopGame()
    {
        Time.timeScale = 0;
        StartCoroutine(SaveScore());
        SceneManager.LoadScene("MenuScene");
    }
    private IEnumerator SaveScore()
    {
        string url = apiUrl + $"userName={GameData.nickname}&score={score}";
        using (UnityWebRequest request = UnityWebRequest.PostWwwForm(url, ""))
        {
            request.SetRequestHeader("accept", "application/json");

            yield return request.SendWebRequest();
        }
    }
}
