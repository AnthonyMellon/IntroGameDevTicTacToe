using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BoardCell : MonoBehaviour
{
    public float size = 2.5f;
    [SerializeField] private SpriteRenderer tile;
    [SerializeField] private Sprite tempTileSprite;

    private void OnMouseDown()
    {
        if(tile.sprite == null)
        {
            Debug.Log("Setting sprite");
            tile.sprite = tempTileSprite;
        }
    }
}
