﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class handcard : MonoBehaviour {

    public GameObject cardPrefab;
    public Transform card1;
    public Transform card2;
    public string[] cardNames;
    public int index;
    private int startdepth = 3;

    private UISprite newcard;
    private float xOffset;

    private List<GameObject> cards = new List<GameObject>();

    void Start()
    {
        xOffset = card2.position.x - card1.position.x;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            addCard();
        else if (Input.GetKeyDown(KeyCode.W))
            loseCard();
    }

    //生成新卡牌
    public void addCard()                                                      //鼠标悬停事件缺少深度判断！！！！！
    {
        if(cards.Count < 10)
        {
            Debug.Log("cccccccccccccccccccccccc");
            GameObject go = NGUITools.AddChild(this.gameObject, cardPrefab);
            Vector3 toPosition = card1.position + new Vector3(xOffset, 0, 0) * cards.Count;
            iTween.MoveTo(go, toPosition, 1f);

            cards.Add(go);

            index = Random.Range(1, cardNames.Length);
            newcard = go.GetComponent<UISprite>();
            newcard.spriteName = cardNames[index];
            go.GetComponent<cardInHand>().initproperty();
            go.GetComponent<cardInHand>().setdepth(startdepth + (cards.Count - 1) * 2);
        }
    }

    //直接销毁手牌，可考虑不要
    public void loseCard()
    {
        int index = Random.Range(0, cards.Count);
        Destroy(cards[index]);
        cards.RemoveAt(index);
        for(int i = 0;i<cards.Count;i++)
        {
            Vector3 toPosition = card1.position + new Vector3(xOffset, 0, 0) * i;
            iTween.MoveTo(cards[i], toPosition, 0.5f);
        }
    }

    public void UpdateShow()
    {
        for(int i=0;i<cards.Count;i++)
        {
            Vector3 toPosition = card1.position + new Vector3(xOffset, 0, 0) * i;
            iTween.MoveTo(cards[i], toPosition, 0.5f);
            //=====================================================================================================         手牌重排时重新设置深度
            cards[i].GetComponent<cardInHand>().setdepth(startdepth + i * 2);   
        }
    }
    
    //卡牌被打出
    public void removeCard(GameObject go)
    {
        cards.Remove(go);
    }
}
