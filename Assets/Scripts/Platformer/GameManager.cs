using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameManager : MonoBehaviour
{
    [SerializeField] private int collectablesToWin = 3;
    private PlayerController2D player;

    private int nextLevelIndex;

    private bool isPaused = false;
    //change to private or reference???
    public GameObject pauseMenu;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController2D>();
        nextLevelIndex = SceneManager.GetActiveScene().buildIndex;
        nextLevelIndex++;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.getCollectCnt() == collectablesToWin)
        {
            Debug.Log("Player finished level!");
            if(nextLevelIndex < SceneManager.sceneCountInBuildSettings)
            {
                SceneManager.LoadScene(nextLevelIndex);
            }
            else
            {
                Debug.LogError("No more levels available!");
                //Load Start scene instead
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene(nextLevelIndex); //refactor to instead load specific scene (Level_1)
    }

    private void PauseGame()
    {
        Time.timeScale = 0;
        isPaused = true;
        pauseMenu.SetActive(isPaused);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        isPaused = false;
        pauseMenu.SetActive(isPaused);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        Debug.Log("Quit Game");
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
