using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using System;

public class CardsHolder : MonoBehaviour
{

    public List<Card> cards;
    [SerializeField] protected int minCardsCount = 4;
    [SerializeField] protected int maxCardsCount = 6;
    [SerializeField] protected float interval;
    [SerializeField] protected float maxRotation = 25f;
    [SerializeField] protected float curviness = 10f;

    public virtual void ResetPositions()
    {
        if (cards.Count > 0)
        {
            for (int i = 0; i < cards.Count; i++)
            {
                cards[i].GetComponent<RectTransform>().DOAnchorPos(GetPosition(i, cards.Count), 0.5f, false);
            }
        }
    }

    public virtual void ResetRotations()
    {
        if (cards.Count > 0)
        {
            for (int i = 0; i < cards.Count; i++)
            {
                cards[i].GetComponent<RectTransform>().DORotate(new Vector3(0, 0, GetRotation(i, cards.Count)), 0.5f);
            }
        }
    }

    public virtual void ResetTransforms()
    {
        ResetPositions();
        ResetRotations();
    }

    public virtual void AddCardAtIndex(int index, Card card)
    {
        if (CanAddCard())
        {
            cards.Insert(index, card);
            card.rTransform.parent = GetComponent<RectTransform>();
            card.rTransform.localScale = new Vector3(1, 1, 1);
            ResetTransforms();
        }
    }

    public virtual void AddCard(Card card)
    {
        if (CanAddCard())
        {
            cards.Add(card);
            card.rTransform.parent = GetComponent<RectTransform>();
            card.rTransform.localScale = new Vector3(1, 1, 1);
            ResetTransforms();
        }
    }


    public virtual void RemoveCard(Card card)
    {
        cards.Remove(card);
        ResetTransforms();
    }

    public virtual void RemoveCard(int index)
    {
        if (index < 0 || index >= cards.Count)
        {
            return;
        }

        var card = cards[index];
        RemoveCard(card);
    }


    public virtual void DestroyCard(int index)
    {
        if (index < 0 || index >= cards.Count)
        {
            return;
        }
        var card = cards[index];
        RemoveCard(card);
        GameObject.Destroy(card.gameObject);
    }

    public virtual void Clear()
    {
        foreach (var card in cards)
        {
            GameObject.Destroy(card.gameObject);
        }
        cards.Clear();
    }

    public bool CanAddCard()
    {
        return cards.Count < maxCardsCount;
    }

    protected virtual Vector3 GetPosition(int index, int count)
    {
        if (index < 0 || index >= count)
        {
            throw new IndexOutOfRangeException();
        }
        //calculating position of the left card + index times interval
        float x = 0 - ((float)count / 2 - 0.5f) * interval + index * interval;
        float y = Mathf.Abs(GetDistanceFromCenter(index, count)) * curviness * -1;
        return new Vector3(x, y, 0);

    }

    protected virtual float GetRotation(int index, int count)
    {
        float rotationStep = maxRotation / ((float)maxCardsCount / 2f);
        var factor = GetDistanceFromCenter(index, count);
        float rotation = factor * rotationStep;
        return rotation;
    }


    protected virtual float GetDistanceFromCenter(int index, int count)
    {
        return (float)count / 2 - index - 0.5f;
    }

}
