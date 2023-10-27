using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    public GameObject gameOverUi;
    private PlayerMoverment playerMoverment;
    // Start is called before the first frame update
    void Start()
    {
        playerMoverment = FindObjectOfType<PlayerMoverment>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerMoverment.getDead() && !gameOverUi.activeSelf)
        {
            gameOverUI();
        }
    }
    public void gameOverUI()
    {
        gameOverUi.SetActive(true);
    }
    public void gameRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void Quit()
    {
        Application.Quit();
    }
}
