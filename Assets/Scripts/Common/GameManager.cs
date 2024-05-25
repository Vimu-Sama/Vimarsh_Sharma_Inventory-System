using System.Collections;
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
        PlayerMovement.GameOver += GameOverFunction;
    }

    private void GameOverFunction()
    {
        if (this.gameObject == null)
            return;
        StartCoroutine(GameOverMainFunction());
    }

    private IEnumerator GameOverMainFunction()
    {
        yield return new WaitForSeconds(1f);
        Time.timeScale = 0f;
        gameOverPanel.SetActive(true);
        gamePauseButton.SetActive(false);
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

    private void OnDestroy()
    {
        PlayerMovement.GameOver -= GameOverFunction;
    }
}
