using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CornerCollision : MonoBehaviour
{
    private Collider2D pCollider;
    private Transform pTransform;
    private Collider2D[] cornerColliders;
    // Start is called before the first frame update
    void Start()
    {
        pCollider = GetComponentInParent<Collider2D>();
        pTransform = GetComponentInParent<Transform>();
        cornerColliders = new Collider2D[4];
        for (int i = 0; i < 4; ++i)
        {
            cornerColliders[i] = gameObject.AddComponent<Collider2D>();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
