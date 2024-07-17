using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject[] players;
    public static event Action OnNewRound;

    public void CheckWinStateVersus() {
        int aliveCount = 0;
        foreach (GameObject player in players) {
            if (player.activeSelf) {
                aliveCount++;
            }
        }
        if (aliveCount <= 1) {
            Invoke(nameof(NewRound), 2f);
        }
    }

    private void NewRound() {
        OnNewRound?.Invoke();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
