using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject endPanel;
    public TextMeshProUGUI winOrLoss;
    public PlayerManager playerManager;
    public EntityManager entityManager;
    public void GameOver(string status)
    {
        if (status == "Loss")
        {
            winOrLoss.text = "You lose!";
        }
        else
        {
            winOrLoss.text = "You win!";
        }
        endPanel.SetActive(true);
    }
    public void TryAgain()
    {
        endPanel.SetActive(false);
        List<GameObject> entities = entityManager.GetEntities();
        foreach (var entity in entities)
        {
            Destroy(entity);
        }
        entityManager.Start();
        playerManager.ResetSize();
        playerManager.UpdateSize();
    }

}
