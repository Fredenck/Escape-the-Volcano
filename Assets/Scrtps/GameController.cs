
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/**
 * GAME CONTROLLER
 * Controls the overall game state and connects game score to UI
 */

public class GameController : MonoBehaviour
{
    public System.Action<int> OnScoreUpdated;

    [Header("Scene Management")]
    public string GameOverScene = "";
    public string GameStartScene = "";
    public string MenuScene = "";



    [Header("UI Elements")]
    public Text UI_PlayerScoreText = null;

    void Start()
    {
        m_playerScore = 0;
        UpdateUIScore();
    }

	public void QuitApplication()
    {
        Application.Quit();
    }


    public void Collect(int score)
    {
        m_playerScore += score;
        UpdateUIScore();
        OnScoreUpdated(m_playerScore);
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }

    public void LoadGameOverScene()
    {
        if (!string.IsNullOrEmpty(GameOverScene))
            SceneManager.LoadScene(GameOverScene, LoadSceneMode.Single);
    }

	public void LoadGameStartScene()
    {
        if (!string.IsNullOrEmpty(GameStartScene))
            SceneManager.LoadScene(GameStartScene, LoadSceneMode.Single);
    }

	public void LoadMenuScene()
    {
        if (!string.IsNullOrEmpty(MenuScene))
            SceneManager.LoadScene(MenuScene, LoadSceneMode.Single);
    }

    private void UpdateUIScore()
    {
        if (null != UI_PlayerScoreText)
            UI_PlayerScoreText.text = string.Format("{0}", m_playerScore);
    }


    private int m_playerScore;
}
