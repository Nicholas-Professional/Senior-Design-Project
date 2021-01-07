using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResizeButtons : MonoBehaviour
{
    [SerializeField]
    private GameObject container;

    void Update()
    {
        GridLayoutGroup gridLayout = container.GetComponent<GridLayoutGroup>();
        RectTransform thisRect = gameObject.GetComponent<RectTransform>();

        if(gridLayout != null && thisRect != null)
        {            
            float rectWidth = thisRect.rect.width;
            Vector2 newCellSize = new Vector2 (rectWidth/3, rectWidth/3);
            gridLayout.cellSize = newCellSize;
        }
    }
}
