using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject gameWinPanel;
    [SerializeField] private GameObject gamePauseButton;
    [SerializeField]  private EnemyAI[] bossEnemyList;
    private void Start()
    {
        PlayerMovement.GameOver += GameOver;
    }

    private void GameOver()
    {
        Time.timeScale = 0f;
        gamePauseButton.SetActive(false);
        gameOverPanel.SetActive(true);
    }

    public void WonGame()
    {
        for(int i=0;i<bossEnemyList.Length;i++)
        {
            if (bossEnemyList[i]!=null)
            {
                return;
            }
        }
        Time.timeScale = 0f;
        gameWinPanel.SetActive(true);
    }


}
