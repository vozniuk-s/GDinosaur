using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{
    public void StartGame()
    {
        if (GameData.nickname != null)
            SceneManager.LoadScene("GameScene");
    }
}
