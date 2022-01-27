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
    private GameObject pauseMenuParent;
    private GameObject pauseMenu;
    private int nextLevelIndex;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController2D>();

        nextLevelIndex = SceneManager.GetActiveScene().buildIndex;
        nextLevelIndex++;

        pauseMenuParent = GameObject.Find("PauseMenu");
        if(pauseMenuParent.transform.childCount >= 1)
            pauseMenu = pauseMenuParent.transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        //Loading next level upon win condition
        if (SceneManager.GetActiveScene().buildIndex != 0 && player.getCollectCnt() == collectablesToWin)
        {
            Debug.Log("Player finished level!");
            if(nextLevelIndex < SceneManager.sceneCountInBuildSettings)
            {
                SceneManager.LoadScene(nextLevelIndex);
            }
            else
            {
                Debug.LogError("No more levels available!");
                //Load Start scene instead?
            }
        }

        //Pause Input
        if (SceneManager.GetActiveScene().buildIndex != 0 && Input.GetKeyDown(KeyCode.Escape))
        {
            //Change to Lamba/Ternary?
            if(!pauseMenu.activeSelf)
            {
                PauseGame();
            }
            else
            {
                ResumeGame();
            }
        }

        //Stop time scale if pause menu is active
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            Time.timeScale = pauseMenu.activeSelf ? 0 : 1;
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene(nextLevelIndex); //refactor to instead load specific scene (Level_1)?
    }

    private void PauseGame()
    {
        pauseMenu.SetActive(true);
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
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
