using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Card : MonoBehaviour
{
    public bool isPicked = false;
    public bool isPickable = true;
    public CardParameters cardParameters;
    public UnityEvent<CardParameters> onChange;
    public UnityEvent<Card> onPick;
    public UnityEvent<Card> onRelease;

    public RectTransform rTransform;

    void Awake()
    {
        cardParameters = new CardParameters { health = UnityEngine.Random.Range(1, 9), attack = UnityEngine.Random.Range(-2, 9), mana = UnityEngine.Random.Range(-2, 9) };
    }

    void Start()
    {
        rTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        if (isPicked)
        {
            rTransform.position = Input.mousePosition;
            rTransform.rotation = Quaternion.identity;
        }
    }

    public void RandomizeOneParameter()
    {
        var paramIndex = UnityEngine.Random.Range(0, 3);

        switch (paramIndex)
        {
            case (0):
                {
                    cardParameters.health = UnityEngine.Random.Range(-2, 9);
                    onChange.Invoke(cardParameters);
                    break;
                }
            case (1):
                {
                    cardParameters.mana = UnityEngine.Random.Range(-2, 9);
                    onChange.Invoke(cardParameters);
                    break;
                }
            case (2):
                {
                    cardParameters.attack = UnityEngine.Random.Range(-2, 9);
                    onChange.Invoke(cardParameters);
                    break;
                }
        }

    }

    public void Clicked()
    {
        if (isPickable)
        {
            onPick.Invoke(this);
            isPicked = true;
        }
    }

    public void UnClicked()
    {
        if (isPickable)
        {
            isPicked = false;
            onRelease.Invoke(this);
        }

    }

    private void OnDestroy()
    {
        onChange.RemoveAllListeners();
        onRelease.RemoveAllListeners();
    }

}
