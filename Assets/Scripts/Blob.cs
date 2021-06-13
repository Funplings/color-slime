using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blob : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    SpriteRenderer spriteRenderer;
    Vector3 moveDirection = Vector3.zero;

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update() {
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
    }

    public void SetDirection(Vector3 direction) {
        moveDirection = direction.normalized;
    }

    public void SetColor(Color color) {
        spriteRenderer.color = color;
    }

    public Color GetColor() {
        return spriteRenderer.color;
    }
    public void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            other.gameObject.GetComponent<ColorAbsorption>().Absorb(spriteRenderer.color);
            Destroy(gameObject);
        } else if (other.CompareTag("Blob Destroyer")) {
            Destroy(gameObject);
        }
    }
}
