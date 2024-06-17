using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public int startingHealth = 100;
    private int health;

    private Vector3 startingPosition;


    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
        health = startingHealth;
    }

    public void TakeDamage(float amount)
    {
        Die();
    }

    private void ApplyDamage(int damage) {
        // Apply damage to the target
        health -= damage;
        if (health <= 0) {
            Die();
        }
    }

    private void Die() {
        Respawn();
    }

    private void Respawn() {
        // Respawn the target
        health = startingHealth;
        transform.position = RandomVector3(startingPosition, 5f);
    }
    
    private Vector3 RandomVector3(Vector3 center, float range) {
        // Generate a random vector within a specified range from the center
        float x = UnityEngine.Random.Range(center.x - range, center.x + range);
        float z = UnityEngine.Random.Range(center.z - range, center.z + range);
        return new Vector3(x, 1.5f, z);
    }
}
