using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BombController : MonoBehaviour
{
    [Header("Bomb")]
    [SerializeField] private GameObject bombPrefab;
    [SerializeField] private KeyCode inputKey = KeyCode.Space;
    [SerializeField] private float bombFuseTime = 3f;
    [SerializeField] private int bombAmount = 1;
    [SerializeField] private int bombRemaining;
    [Header("Explosion")]
    [SerializeField] private Explosion explosionPrefab;
    [SerializeField] private LayerMask explosionLayerMask;
    [SerializeField] private float explosionDuration = 1f;
    [SerializeField] private int explosionRadius = 1;
    [Header("Destructibles")]
    [SerializeField] private Tilemap destructibleTiles;
    [SerializeField] private Destructible destructiblePrefab;

    private void OnEnable() {
        bombRemaining = bombAmount;
    }

    private void Update() {
        if (bombRemaining > 0 && Input.GetKeyDown(inputKey)) {
            StartCoroutine(PlaceBomb());
        }
    }
    
    private IEnumerator PlaceBomb() {
        Vector2 position = transform.position;
        position.x = Mathf.Round(position.x) - 0.1f;
        position.y = Mathf.Round(position.y) + 0.1f;

        GameObject bomb = Instantiate(bombPrefab, position, Quaternion.identity);
        bombRemaining--;

        yield return new WaitForSeconds(bombFuseTime);

        position = bomb.transform.position;
        position.x = Mathf.Round(position.x) - 0.1f;
        position.y = Mathf.Round(position.y) + 0.1f;

        Explosion explosion = Instantiate(explosionPrefab, position, Quaternion.identity);
        explosion.SetActiveRenderer(explosion.start);
        explosion.DestroyAfter(explosionDuration);
        Explode(position, Vector2.up, explosionRadius);
        Explode(position, Vector2.down, explosionRadius);
        Explode(position, Vector2.left, explosionRadius);
        Explode(position, Vector2.right, explosionRadius);

        Destroy(bomb);
        bombRemaining++;
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Bomb")) {
            other.isTrigger = false;
        }
    }

    private void Explode(Vector2 position, Vector2 direction, int length) {
        if (length <= 0) {
            return;
        }
        position += direction;
        if (Physics2D.OverlapBox(position, Vector2.one/2, 0f, explosionLayerMask)) { // Verifica se há um collider próximo, retorna o próprio se houver
            ClearDestructible(position);
            return;
        }
        Explosion explosion = Instantiate(explosionPrefab, position, Quaternion.identity);
        explosion.SetActiveRenderer(length > 1 ? explosion.middle : explosion.end); // if(length > 1) {explosion middle}    else {explosion end}
        explosion.SetDirection(direction);
        explosion.DestroyAfter(explosionDuration);

        Explode(position, direction, length - 1);
    }

    private void ClearDestructible(Vector2 position) {
        Vector3Int cell = destructibleTiles.WorldToCell(position);
        TileBase tile = destructibleTiles.GetTile(cell);

        if (tile != null) {
            Instantiate(destructiblePrefab, position, Quaternion.identity);
            destructibleTiles.SetTile(cell, null);
        }
    }

    public void IncreaseBombAmount() {
        bombAmount++;
        bombRemaining++;
    }
    
    public void IncreaseExplosionRadius() {
        explosionRadius++;
    }

    public int GetBombRadius() {
        return explosionRadius;
    }

    public int GetBombAmount() {
        return bombAmount;
    }
}
