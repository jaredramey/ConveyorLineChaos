using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField]
    GameObject mainMenu;

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }
}
