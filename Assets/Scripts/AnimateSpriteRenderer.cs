using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateSpriteRenderer : MonoBehaviour
{
    [SerializeField] private float animationTime = 0.25f;
    [SerializeField] private int animationFrame;
    [SerializeField] private Sprite iddleAnimation;
    [SerializeField] private Sprite[] animationSprites;
    [SerializeField] private SpriteRenderer spriteRenderer;
    public bool loop = true; // Define se uma animação vai entrar em loop
    public bool iddle = true; // Define se uma animação vai entrar em iddle
    
    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable() {
        spriteRenderer.enabled = true;
    }

    private void OnDisable() {
        spriteRenderer.enabled = false;
    }

    private void Start() {
        InvokeRepeating(nameof(NextFrame), animationTime, animationTime);
    }

    private void NextFrame() {
        animationFrame++;
        if (animationFrame >= animationSprites.Length) {
            animationFrame = 0;
        }
        if (iddle) {
            spriteRenderer.sprite = iddleAnimation;
        } else if (animationFrame >= 0 && animationFrame < animationSprites.Length) spriteRenderer.sprite = animationSprites[animationFrame];
    }
}
