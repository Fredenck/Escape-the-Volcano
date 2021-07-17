
using UnityEngine;

/**
 * ACTIVATION OBJECT
 * This object is activated (turned on or off) when the player reaches the activation score.
 *
 * Requires:
 *  - this game object must be active on the first frame of the scene (you can't disable it in the inspector).
 *    To get around this problem, uncheck ShouldStartActive if you want it to be off initially. 
 *
 * Troubleshooting:
 *  Q. My objects don't turn on when it should activate.
 *  A. Don't disable the GameObject in the inspector, uncheck "should start active" instead.
 */
public class ActivationObject : MonoBehaviour
{
    [Tooltip("Score when this object should activate (turn on or off)")]
    public int ActivationScore = 3;

    [Tooltip("Should this object remain active when the scene starts?")]
    public bool ShouldStartActive = true;


    void Start()
    {
        GameObject l_gameControllerObject = GameObject.FindGameObjectWithTag(GameConstants.GAME_CONTROLLER_TAG);
        if (null != l_gameControllerObject)
        {
            GameController l_gameController = l_gameControllerObject.GetComponent<GameController>();
            l_gameController.OnScoreUpdated += ScoreCheck;
        }
        m_hasBeenActivated = false;

        if (ShouldStartActive == false)
        {
            gameObject.SetActive(false);
        }
    }


    void ScoreCheck(int score)
    {
        if (!m_hasBeenActivated)
        {
            if (score >= ActivationScore)
            {
                gameObject.SetActive(!gameObject.activeSelf);
                m_hasBeenActivated = true;
            }
        }
    }


    bool m_hasBeenActivated = false;
}
