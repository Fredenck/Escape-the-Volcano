
using UnityEngine;

/**
 * COLLECTIBLE OBJECT
 * Makes this object collectible by the player, and reports when collected
 * 
 * Requires:
 *  - A Collider that IsTrigger is checked (marked as true).
 *
 * Troubleshooting:
 *  Q. Objects are solid, player can't 'enter' the collectible to collect it
 *  A. Collider needs to be a trigger! (set the IsTrigger to true)
 */
public class CollectibleObject : MonoBehaviour
{
    [Tooltip("The Value of this collected object when collected")]
    public int CollectedValue = 10;

    void Start()
    {
        GameObject l_gameControllerObject = GameObject.FindGameObjectWithTag(GameConstants.GAME_CONTROLLER_TAG);
        if (null != l_gameControllerObject)
        {
            m_gameController = l_gameControllerObject.GetComponent<GameController>();
        }
    }


    void OnTriggerEnter(Collider p_collider)
    {
        if (p_collider.gameObject.tag == GameConstants.PLAYER_TAG)
        {
            if (null != m_gameController)
            {
                // notify the game controller of collection
                m_gameController.Collect(CollectedValue);


                // then destroy this object
                Destroy(this.gameObject, 0f);
            }
        }
    }

    private GameController m_gameController;

}
