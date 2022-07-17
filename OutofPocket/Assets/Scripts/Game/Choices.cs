using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/* HOW TO USE
private void Listener(object sender, Choices.choiceEventArgs e) 
{
    if(e.choice) 
    {
        Debug.Log("Choice 1");
    }
    else
    {
        Debug.Log("Choice 2");
    }
}

Choices c = Choices.GetInstance();
c.SetChoiceText("choice 1 text", "choice 2 text");
Choices.choiceEvent += Listener; //will automatically remove Listener after choice 1 or 2 is selected
c.Activate();

//will automatically deactivate after triggering once

*/


public class Choices : Singleton<Choices>
{
    public class choiceEventArgs
    {
        public bool choice; // true = choice1, false = choice2
    }
    public GameObject leftText;
    public GameObject rightText;
    public GameObject[] choice1Pockets;
    public GameObject[] choice2Pockets;
    public static event System.EventHandler<choiceEventArgs> choiceEvent; 
    
    private bool isActive = false;
    // Start is called before the first frame update
    void Start()
    {
        InitializeSingleton();
        PoolBall.ballInPocketEvent += WaitForChoice;
        //choiceEvent += DefaultListener;
        //SetChoiceText("Choice 1", "Choice 2");
        //Activate();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public static Choices GetInstance()
    {
        return _instance;
    }
    void Activate() 
    {
        isActive = true;
    }
    void Deactivate()
    {
        isActive = false;
        SetChoiceText("","");
    }
    void SetChoiceText(string t1, string t2)
    {
        leftText.GetComponent<TextMeshPro>().text = t1;
        rightText.GetComponent<TextMeshPro>().text = t2;
    }

    private void WaitForChoice(object sender, PoolBall.BallEventArgs e) 
    {
        Debug.Log("Choices got pool ball event");
        Debug.Log(e.pocket.name);
        if(!isActive) {
            return;
        }
        if(Array.IndexOf(choice1Pockets, e.pocket) != -1)
        {
            choiceEvent?.Invoke(this, new choiceEventArgs{choice=true});
        }
        else if(Array.IndexOf(choice2Pockets, e.pocket) != -1)
        {
            choiceEvent?.Invoke(this, new choiceEventArgs{choice=false});
        }
        else 
        {
            return;
        }
        Deactivate();
        foreach (var d in choiceEvent.GetInvocationList())
            choiceEvent -= (d as System.EventHandler<choiceEventArgs>);

    }

    private void DefaultListener(object sender, choiceEventArgs e) 
    {
        if(e.choice) 
        {
            Debug.Log("Choice 1");
        }
        else
        {
            Debug.Log("Choice 2");
        }
    }
}
