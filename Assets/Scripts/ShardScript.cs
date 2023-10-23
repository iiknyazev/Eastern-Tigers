using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShardScript : MonoBehaviour, IPointerClickHandler
{
    public bool IsPlaced { get; private set; }

    private Vector3 startPosition;
    private int currentLevelNumber;

    private static List<Vector2> vectorsCorrespondingToShards = new List<Vector2>()
    {
        Vector3.zero,   // Plug
        Vector3.right,  // Pic i shard 1
        Vector3.down,   // Pic i shard 2
        Vector2.left,   // Pic i shard 3
        Vector3.up,     // Pic i shard 4
    };
    
    public void OnPointerClick(PointerEventData eventData)
    {
        if (!IsPlaced || GameManager.IsPaused)
            transform.Rotate(new Vector3(0, 0, 90));
    }

    private void Awake()
    {
        int randomNumberRotations = new System.Random().Next(4);
        for (int i = 0; i < randomNumberRotations; ++i)
            transform.Rotate(new Vector3(0, 0, 90));
    }

    private void Start()
    {
        IsPlaced = false;
        startPosition = gameObject.transform.position;

        currentLevelNumber =  GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().LevelNumber;

        CollisionHandler.OnCollisionOccurred += CollisionHandler_OnCollisionOccurred;
        SwipeDetection.OnSwipeDetected += SwipeDetection_OnSwipeDetected;
    }

    private void SwipeDetection_OnSwipeDetected(object sender, SwipeEventArgs e)
    {
        if (GameManager.IsPaused)
            return;

        int numberOfShard = vectorsCorrespondingToShards.IndexOf(e.Direction);
        if (gameObject.name == "Pic " + currentLevelNumber + " shard " + numberOfShard)
            StartCoroutine(Movement());
    }

    private void CollisionHandler_OnCollisionOccurred(object sender, EventArgs e)
    {
        IsPlaced = false;
        StopAllCoroutines();
        gameObject.transform.position = startPosition;
    }

    private IEnumerator Movement()
    {
        while (!IsEqualVectors(transform.position, Vector3.zero))
        {
            float hypotenuse = transform.position.x * transform.position.x + transform.position.y * transform.position.y;
            float stepX = transform.position.x / hypotenuse * 0.015f;
            float stepY = transform.position.y / hypotenuse * 0.015f;
            transform.position = new Vector3(transform.position.x - stepX, transform.position.y - stepY, transform.position.z);
            yield return new WaitForSeconds(0.001f);
        }
        transform.position = Vector3.zero;
        IsPlaced = true;
    }

    private bool IsEqualVectors(Vector3 a, Vector3 b)
    {
        double eps = 1e-1;
        return a.x <= b.x + eps && a.x >= b.x - eps && a.y <= b.y + eps && a.y >= b.y - eps ? true : false;
    }

    private void OnDestroy()
    {
        CollisionHandler.OnCollisionOccurred -= CollisionHandler_OnCollisionOccurred;
        SwipeDetection.OnSwipeDetected -= SwipeDetection_OnSwipeDetected;
    }
}