using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayButton : MonoBehaviour
{
    public void StartGame()
    {
        if (GameData.nickname != null)
            SceneManager.LoadScene("GameScene");
    }
}
