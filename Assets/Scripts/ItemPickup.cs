using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    [SerializeField] private enum ItemType {
        BlastRadius,
        ExtraBomb,
        SpeedIncrease,
    }
    [SerializeField] private ItemType type;
    public static event Action OnOnItemPickup;

    public void OnItemPickup(GameObject player) {
        if (OnOnItemPickup != null) {
            switch (type) {
                case ItemType.BlastRadius:
                    player.GetComponent<BombController>().IncreaseExplosionRadius();
                    OnOnItemPickup?.Invoke();
                    break;
                case ItemType.ExtraBomb:
                    player.GetComponent<BombController>().IncreaseBombAmount();
                    OnOnItemPickup?.Invoke();
                    break;
                case ItemType.SpeedIncrease:
                    player.GetComponent<MovementControler>().IncreaseSpeed();
                    OnOnItemPickup?.Invoke();
                    break;
            }
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            OnItemPickup(other.gameObject);
        }
    }
}
