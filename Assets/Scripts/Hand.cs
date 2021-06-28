using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Hand : CardsHolder
{

    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private Table table;

    private int currentIndex = -1;

    void Start()
    {
        cards = new List<Card>();
        Shuffle();
        if (table == null)
            table = FindObjectOfType<Table>();

    }

    public void RandomizeNextCard()
    {
        var card = GetNextCard();
        card.RandomizeOneParameter();
        if (card.cardParameters.health <= 0)
            DestroyCard(cards.IndexOf(card));
    }




    public void Shuffle()
    {
        cards.ForEach(c => GameObject.Destroy(c.gameObject));
        cards.Clear();
        int cardsCount = UnityEngine.Random.Range(minCardsCount, maxCardsCount + 1);
        for (int i = 0; i < cardsCount; i++)
        {
            var instance = GameObject.Instantiate(cardPrefab, transform.position, Quaternion.identity);
            var rTransform = instance.GetComponent<RectTransform>();
            rTransform.parent = GetComponent<RectTransform>();
            rTransform.localPosition = GetPosition(i, cardsCount);
            rTransform.rotation = Quaternion.Euler(0, 0, GetRotation(i, cardsCount));
            rTransform.localScale = new Vector3(1, 1, 1);
            cards.Add(instance.GetComponent<Card>());
            instance.GetComponent<Card>().onRelease.AddListener(ReleaseCard);
        }
    }


    void ReleaseCard(Card card)
    {
        if (table.IsPointerOver() && table.CanAddCard())
        {
            card.isPickable = false;
            table.AddCard(card);
            RemoveCard(card);
        }
        else
            ResetTransforms();
    }



    int GetNextIndex()
    {
        currentIndex++;
        if (currentIndex >= cards.Count)
            currentIndex = 0;
        return currentIndex;
    }


    Card GetNextCard()
    {
        return cards[GetNextIndex()];
    }
}
