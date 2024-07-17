using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject[] players;
    [SerializeField] private TMP_Text[] bombRadiusT;
    [SerializeField] private TMP_Text[] bombCountT;
    [SerializeField] private TMP_Text[] playerSpeedT;

    public void AttPowerUpCount() {
        for (int i = 0; i < players.Length; i++) {
        bombRadiusT[i].text = players[i].GetComponent<BombController>().GetBombRadius().ToString();
        bombCountT[i].text = players[i].GetComponent<BombController>().GetBombAmount().ToString();
        playerSpeedT[i].text = (players[i].GetComponent<MovementControler>().GetSpeed() - 4).ToString();
        }
    }

    private void Start() {
        ItemPickup.OnOnItemPickup += AttPowerUpCount;
        GameManager.OnNewRound += UnsubscribeAll;
    }

    private void OnEnable() {
        AttPowerUpCount();
    }

    private void OnDisable() {
        
    }

    private void UnsubscribeAll() {
        ItemPickup.OnOnItemPickup -= AttPowerUpCount;
        GameManager.OnNewRound -= UnsubscribeAll;
    }
}
