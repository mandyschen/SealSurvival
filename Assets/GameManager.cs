// Manage game over status.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// Describe what happens when a win or loss occurs
public class GameManager : MonoBehaviour
{
    public GameObject endPanel; // Panel that pops up for game over
    public TextMeshProUGUI winOrLoss; // Text that states win or loss
    public PlayerManager playerManager; // Updates player for game over event
    public EntityManager entityManager; // Updates entities for game over event

    // What happens when a game over event occurs
    public void GameOver(string status)
    {
        if (status == "Loss") // States the player loses
        {
            winOrLoss.text = "You Lose!";
        }
        else // States the player wins
        {
            winOrLoss.text = "You Win!";
        }

        endPanel.SetActive(true); // Make game over panel visible and active

        // Destroys current active entities
        List<GameObject> entities = entityManager.GetEntities();
        foreach (var entity in entities)
        {
            Destroy(entity);
        }
    }

    // Reset the game
    public void TryAgain()
    {
        endPanel.SetActive(false); // Hide game over panel
        entityManager.Start(); // Respawn entities
        playerManager.ResetSize(); // Reset player size to 5
        playerManager.UpdateSize(); // Updates sprite size and boundaries for player
    }

}
