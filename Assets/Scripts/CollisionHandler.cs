using System;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{    
    public static EventHandler OnCollisionOccurred;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnCollisionOccurred?.Invoke(this, EventArgs.Empty);
    }
}