using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    // Buscar método melhor
    [SerializeField] private GameObject player;
    [SerializeField] private TMP_Text bombRadiusT;
    [SerializeField] private TMP_Text bombCountT;
    [SerializeField] private TMP_Text playerSpeedT;

    public void AttPowerUpCount() {
        bombRadiusT.text = player.GetComponent<BombController>().GetBombRadius().ToString();
        bombCountT.text = player.GetComponent<BombController>().GetBombAmount().ToString();
        playerSpeedT.text = (player.GetComponent<MovementControler>().GetSpeed() - 4).ToString();
    }

    private void Start() {
        ItemPickup.OnOnItemPickup += AttPowerUpCount;
    }

    private void OnEnable() {
        AttPowerUpCount();
    }

    private void OnDisable() {
        
    }
}
