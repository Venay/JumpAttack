using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public int health = 1;
    public UnityEngine.Events.UnityEvent onDeath;

    public void damage(int dmg)
    {
        health -= dmg;
        if (health <= 0)
            onDeath.Invoke();
    }
}
