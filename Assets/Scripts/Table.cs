using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Table : CardsHolder
{
    Canvas canvas;

    private void Start()
    {
        canvas = FindObjectOfType<Canvas>();
    }

    public bool IsPointerOver()
    {
        var mousePos = Input.mousePosition;
        var rTransform = GetComponent<RectTransform>();

        return (mousePos.x < (rTransform.position.x + (rTransform.rect.width / 2) * canvas.scaleFactor * rTransform.localScale.x) &&
                mousePos.x > (rTransform.position.x - (rTransform.rect.width / 2) * canvas.scaleFactor * rTransform.localScale.x) &&
                mousePos.y > (rTransform.position.y - (rTransform.rect.height / 2) * canvas.scaleFactor * rTransform.localScale.y) &&
                mousePos.y < (rTransform.position.y + (rTransform.rect.height / 2) * canvas.scaleFactor * rTransform.localScale.y))
        ;

        // var es = EventSystem.current;
        // return (es.IsPointerOverGameObject());
    }


}
