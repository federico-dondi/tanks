using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[System.Serializable]
class Player
{
  public GameObject instance;
  public Transform origin;
}

public class GameManager : MonoBehaviour
{
  [SerializeField] private Player[] players;
  [SerializeField] private int roundsToWin = 5;
  [SerializeField] private float startDelay = 3f;
  [SerializeField] private float endDelay = 3f;

  [Header("UI:")]
  [SerializeField] private Canvas canvas;
  [SerializeField] private Text playerOneText;
  [SerializeField] private Text playerTwoText;
  [SerializeField] private Text winnerText;

  private int playerOneCount = 0;
  private int playerTwoCount = 0;

  private void Start()
  {
    Invoke("StartRound", startDelay);
  }

  private void StartRound()
  {
    canvas.gameObject.SetActive(false);

    players[0].instance.SetActive(true);
    players[0].instance.transform.position = players[0].origin.position;
    players[0].instance.transform.rotation = players[0].origin.rotation;

    players[1].instance.SetActive(true);
    players[1].instance.transform.position = players[1].origin.position;
    players[1].instance.transform.rotation = players[1].origin.rotation;
  }

  public void FinishRound(int winner)
  {
    players[0].instance.SetActive(false);
    players[1].instance.SetActive(false);

    if (winner == 1)
    {
      playerOneCount += 1;
    }
    else
    {
      playerTwoCount += 1;
    }

    canvas.gameObject.SetActive(true);

    playerOneText.text = $"<color=#147DF7>PLAYER 1</color>: {playerOneCount} WINS";
    playerTwoText.text = $"<color=#DE1616>PLAYER 2</color>: {playerTwoCount} WINS";

    if (playerOneCount >= roundsToWin)
    {
      winnerText.text = $"<color=#147DF7>PLAYER 1</color> WON THE BATTLE!";

      Invoke("ReloadGame", endDelay);
    }
    else if (playerTwoCount >= roundsToWin)
    {
      winnerText.text = $"<color=#DE1616>PLAYER 2</color> WON THE BATTLE!";

      Invoke("ReloadGame", endDelay);
    }
    else
    {
      winnerText.text = (winner == 1)
        ? $"<color=#147DF7>PLAYER 1</color> WINS THE ROUND!"
        : $"<color=#DE1616>PLAYER 2</color> WINS THE ROUND!";

      Invoke("StartRound", endDelay);
    }
  }

  private void ReloadGame()
  {
    SceneManager.LoadScene(0);
  }
}
