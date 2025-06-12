using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Collectable
{
    public Sprite emptychest;
    public int koinAmount = 5;

    protected override void OnCollect()
    {
        if (!collected)
        {collected = true;
         GetComponent<SpriteRenderer>().sprite = emptychest;
         GameManager.instance.koin += koinAmount;
         GameManager.instance.ShowText("+" + koinAmount + " Koin!",25,Color.yellow,transform.position,Vector3.up * 25, 1.5f);
         }
    }
}

