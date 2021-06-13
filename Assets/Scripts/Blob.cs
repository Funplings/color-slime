using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blob : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] SpriteRenderer baseSpriteRenderer;
    Vector3 moveDirection = Vector3.zero;

    private void Update() {
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
    }

    public void SetDirection(Vector3 direction) {
        moveDirection = direction.normalized;
    }

    public void SetColor(Color color) {
        baseSpriteRenderer.color = color;
    }

    public Color GetColor() {
        return baseSpriteRenderer.color;
    }
    public void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            other.gameObject.GetComponent<ColorAbsorption>().Absorb(baseSpriteRenderer.color);
            Destroy(gameObject);
        } else if (other.CompareTag("Blob Destroyer")) {
            Destroy(gameObject);
        }
    }
}
