using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 
public class Collidable : MonoBehaviour
{
    public ContactFilter2D filter;
    private BoxCollider2D boxCollider;
    private Collider2D[] hits = new Collider2D[10];

    protected virtual void Start()
    {
        boxCollider = GetComponent <BoxCollider2D>();
    }

    protected virtual void Update()
    {

        //Collision work
        boxCollider.Overlap(filter, hits );
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i] == null)
                continue;

                OnCollide(hits[i]);

                hits[i] = null;
        }
    }

    protected virtual void OnCollide(Collider2D coll)
    {
        Debug.Log("OnCollide was not impelemnted in " + this.name);
    }
}