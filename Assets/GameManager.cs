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
            winOrLoss.text = "You Lose!";
        }
        else
        {
            winOrLoss.text = "You Win!";
        }
        endPanel.SetActive(true);
        List<GameObject> entities = entityManager.GetEntities();
        foreach (var entity in entities)
        {
            Destroy(entity);
        }
    }
    public void TryAgain()
    {
        endPanel.SetActive(false);
        entityManager.Start();
        playerManager.ResetSize();
        playerManager.UpdateSize();
    }

}
