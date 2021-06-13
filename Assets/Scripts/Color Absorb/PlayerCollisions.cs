using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ColorAbsorption))]
public class PlayerCollisions : MonoBehaviour
{

    private ColorAbsorption colorAbsorption;

    void Start()
    {
        colorAbsorption = GetComponent<ColorAbsorption>();
    }
    

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Blob"))
        {
            Debug.Log("hi");
            Color color = other.gameObject.GetComponent<SpriteRenderer>().color;
            colorAbsorption.Absorb(color);
            Destroy(other.gameObject);
        }
    }
    
}
