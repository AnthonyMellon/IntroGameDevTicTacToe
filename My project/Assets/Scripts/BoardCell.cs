using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Timeline;

public class BoardCell : MonoBehaviour
{
    public float size = 2.5f;
    public SpriteRenderer tile;    
    public int owner;

    public delegate void onPiecePlacedHandler(BoardCell cell);
    public onPiecePlacedHandler onPiecePlaced;

    private void Start()
    {        
        owner = -1;
    }

    private void OnMouseDown()
    {
        if(owner == -1)
        {            
            onPiecePlaced.Invoke(this);
        }
    }
}
