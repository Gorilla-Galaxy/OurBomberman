using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementControler : MonoBehaviour
{
    [SerializeField] private new Rigidbody2D rigidbody;
    [SerializeField] private float speed = 5f;
    [SerializeField] private Vector2 direction = Vector2.down;
    [SerializeField] private KeyCode inputUp = KeyCode.W;
    [SerializeField] private KeyCode inputDown = KeyCode.S;
    [SerializeField] private KeyCode inputLeft = KeyCode.A;
    [SerializeField] private KeyCode inputRight = KeyCode.D;
    [SerializeField] private AnimateSpriteRenderer animateSpriteRendererUp;
    [SerializeField] private AnimateSpriteRenderer animateSpriteRendererDown;
    [SerializeField] private AnimateSpriteRenderer animateSpriteRendererLeft;
    [SerializeField] private AnimateSpriteRenderer animateSpriteRendererRight;
    [SerializeField] private AnimateSpriteRenderer animateSpriteRendererDeath;
    [SerializeField] private AnimateSpriteRenderer activeAnimateSpriteRenderer;

    private void Awake() {
        rigidbody = GetComponent<Rigidbody2D>();
        activeAnimateSpriteRenderer = animateSpriteRendererDown;
    }

    private void Update() {
        if (Input.GetKey(inputUp)) {
            SetDirection(Vector2.up, animateSpriteRendererUp);
        } else if (Input.GetKey(inputDown)) {
            SetDirection(Vector2.down, animateSpriteRendererDown);
        } else if (Input.GetKey(inputLeft)) {
            SetDirection(Vector2.left, animateSpriteRendererLeft);
        } else if (Input.GetKey(inputRight)) {
            SetDirection(Vector2.right, animateSpriteRendererRight);
        } else {
            SetDirection(Vector2.zero, activeAnimateSpriteRenderer);
        }
    }

    private void FixedUpdate() {
        Vector2 position = rigidbody.position;
        Vector2 translation = direction * speed * Time.fixedDeltaTime;
        rigidbody.MovePosition(position + translation);
    }

    private void SetDirection(Vector2 newDirection, AnimateSpriteRenderer animateSpriteRenderer) {
        direction = newDirection;
        animateSpriteRendererUp.enabled = animateSpriteRenderer == animateSpriteRendererUp;
        animateSpriteRendererDown.enabled = animateSpriteRenderer == animateSpriteRendererDown;
        animateSpriteRendererLeft.enabled = animateSpriteRenderer == animateSpriteRendererLeft;
        animateSpriteRendererRight.enabled = animateSpriteRenderer == animateSpriteRendererRight;

        activeAnimateSpriteRenderer = animateSpriteRenderer;
        activeAnimateSpriteRenderer.iddle = direction == Vector2.zero; // if (direction == Vector2.zero) actAnimateSpriteRender. iddle = true
    }
    
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Explosion")) {
            DeathSequence();
        }
    }

    private void DeathSequence() {
        enabled = false;
        GetComponent<BombController>().enabled = false;
        animateSpriteRendererUp.enabled = false;
        animateSpriteRendererDown.enabled = false;
        animateSpriteRendererLeft.enabled = false;
        animateSpriteRendererRight.enabled = false;
        animateSpriteRendererDeath.enabled = true;
        Invoke(nameof(OnDeathSequenceEnded), 1.25f);
    }

    private void OnDeathSequenceEnded() {
        gameObject.SetActive(false);
        FindAnyObjectByType<GameManager>().CheckWinStateVersus();
    }

    public void IncreaseSpeed() {
        speed++;
    }

    public float GetSpeed() {
        return speed;
    }
}
