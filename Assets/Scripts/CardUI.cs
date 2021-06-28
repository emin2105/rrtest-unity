using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class CardUI : MonoBehaviour
{
    [SerializeField] private Text health;
    [SerializeField] private Text mana;
    [SerializeField] private Text attack;
    [SerializeField] private Image border;
    private Card card;
    void Start()
    {
        card = GetComponent<Card>();
        UpdateValues(card.cardParameters);
        if (card)
        {
            card.onChange.AddListener(UpdateValues);
            card.onPick.AddListener(ActivateGlow);
            card.onRelease.AddListener(DeactivateGlow);
        }

    }

    void UpdateValues(CardParameters parameters)
    {
        if (health.text != parameters.health.ToString())
        {
            health.rectTransform.DOLocalRotate(new Vector3(0, 91, 0), 0.15f).OnComplete(() =>
            {
                health.text = parameters.health.ToString();
                health.rectTransform.DOLocalRotate(new Vector3(0, 0, 0), 0.15f);
            });

        }
        if (mana.text != parameters.mana.ToString())
        {
            mana.rectTransform.DOLocalRotate(new Vector3(0, 91, 0), 0.15f).OnComplete(() =>
            {
                mana.text = parameters.mana.ToString();
                mana.rectTransform.DOLocalRotate(new Vector3(0, 0, 0), 0.15f);
            });
        }
        if (attack.text != parameters.attack.ToString())
        {
            attack.rectTransform.DOLocalRotate(new Vector3(0, 91, 0), 0.15f).OnComplete(() =>
           {
               attack.text = parameters.attack.ToString();
               attack.rectTransform.DOLocalRotate(new Vector3(0, 0, 0), 0.15f);
           });
        }
    }

    void ActivateGlow(Card card)
    {
        border.enabled = true;
    }

    void DeactivateGlow(Card card)
    {

        border.enabled = false;
    }

    void OnDestroy()
    {
        card.onChange.RemoveListener(UpdateValues);
    }
}
