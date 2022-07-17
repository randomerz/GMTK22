using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class GameManager : Singleton<GameManager>
{
    [Header("References")]
    public GameObject floor;
    public GameObject poolTable;
    public PoolStateManager poolGameManager;
    public GameObject poolBallTriangle;
    public GameObject cueBall;
    public GameObject inGameUI;




    private State<GameManager> currentState;

    public bool currNarrationFinished;

    #region State Definitions
    private Act1State act1;
    private Act2State act2;
    private Act3State act3;
    #endregion

    private void Awake()
    {
        InitializeSingleton();

        act1 = new Act1State(this);
        act2 = new Act2State(this);
        act3 = new Act3State(this);
    }

    private void Start()
    {
        SwitchState(act1);
    }

    private void Update()
    {
        currentState?.UpdateState();
    }

    private void SwitchState(State<GameManager> a_state)
    {
        if (this.currentState != null)
        {
            this.currentState.ExitState();
        }

        this.currentState = a_state;
        this.currentState.EnterState();
    }

    public void DoNarrationAndSetFlag(string path, params CallbackWithDelay[] afterDelay)
    {
        currNarrationFinished = false;
        NarrationManager.PlayVoiceClip(path, () => {
            currNarrationFinished = true;
        });
    }

    #region State Classes

    #region Act1
    public class Act1State : State<GameManager>
    {
        private bool playerPutBallInPocket;
        private bool turnCompleted;
        public Act1State(GameManager ctx) : base(ctx)
        {
        }

        public override void EnterState()
        {

            context.StartCoroutine(DoAct1());
        }

        public IEnumerator DoAct1()
        {
            context.floor.SetActive(false);
            context.poolTable.SetActive(false);
            context.poolBallTriangle.SetActive(false);
            context.cueBall.SetActive(false);
            context.poolGameManager.gameObject.SetActive(false);
            context.inGameUI.SetActive(false);

            //Oppy � and I�ve already got someone playtesting it right now!
            context.DoNarrationAndSetFlag("Optimist/HelloWelcomeTo");
            yield return new WaitUntil(() => { return context.currNarrationFinished; });

            ////Cynic already?
            //context.DoNarrationAndSetFlag("Pessimist/HelloWelcomeTo");
            //yield return new WaitUntil(() => { return context.currNarrationFinished; });

            ////Oppy Crazy, right? Okay, so picture this�*elevator ding sound*
            //context.DoNarrationAndSetFlag("Optimist/HelloWelcomeTo");
            //yield return new WaitUntil(() => { return context.currNarrationFinished; });

            context.floor.SetActive(true);  //Turn on lights

            //Oppy A pool table
            context.DoNarrationAndSetFlag("Optimist/HelloWelcomeTo");
            yield return new WaitUntil(() => { return context.currNarrationFinished; });

            context.poolTable.SetActive(true);

            //Oppy and, and it�s like your standard pool game you know
            context.DoNarrationAndSetFlag("Optimist/HelloWelcomeTo");
            yield return new WaitUntil(() => { return context.currNarrationFinished; });

            //Enable the pool game
            context.poolBallTriangle.SetActive(true);
            context.cueBall.SetActive(true);
            context.poolGameManager.gameObject.SetActive(true);
            context.inGameUI.SetActive(true);

            //Wait until player made a turn.
            turnCompleted = false;
            PoolStateManager.TurnEnded += SetTurnCompleted;   //Scuffy me Luffy 
            yield return new WaitUntil(() => { return turnCompleted; });
            PoolStateManager.TurnEnded -= SetTurnCompleted;

            //Oppy �except, they�re not actually balls, they�re dice!
            context.DoNarrationAndSetFlag("Optimist/HelloWelcomeTo");
            yield return new WaitUntil(() => { return context.currNarrationFinished; });

            PoolStateManager.ChangeAllToShape(PoolBall.Shape.Dice);


            playerPutBallInPocket = false;
            yield return new WaitUntil(() => playerPutBallInPocket);


            //Cynic

            //Oppy




        }

        private void SetTurnCompleted(object sender, System.EventArgs e)
        {
            turnCompleted = true;
        }
    }
    #endregion

    #region Act2 
    public class Act2State : State<GameManager>
    {
        public Act2State(GameManager ctx) : base(ctx)
        {
        }

        public override void EnterState()
        {
            context.StartCoroutine(DoAct2());
        }

        public IEnumerator DoAct2()
        {
            yield return null;
        }
    }
    #endregion

    #region Act 3
    public class Act3State : State<GameManager>
    {   
        private Choices c;
        private bool madeDecision;
        private bool wasDecisionLeft;
        public Act3State(GameManager ctx) : base(ctx)
        {
        }

        public override void EnterState()
        { 
            c = Choices.GetInstance();
            context.StartCoroutine(DoAct3());
        }

        public IEnumerator DoAct3()
        {
            //Oppy: *determined* Alright, I’ve got it. So I’ve tried adding a bunch of different mechanics to the game, 
            context.DoNarrationAndSetFlag("Act3/Optimist/IveGotIt");    
            //but none of them have actually solved the core problem of “how do we make dice with pool work.”
            context.DoNarrationAndSetFlag("Act3/Optimist/CoreProblem");    
            
            //Cynic: *interested* Go on.
            context.DoNarrationAndSetFlag("Act3/Cynic/GoOn");    

            //Oppy: Well, I got to thinking. If adding these mechanics in hasn’t helped at all, why don’t we take away the ones that haven’t worked…
            context.DoNarrationAndSetFlag("Act3/Optimist/AddingMechanics");    

            //Cynic: *interested* Ok…
            context.DoNarrationAndSetFlag("Act3/Cynic/Ok");   

            //Oppy: *laughing* and add procedural generation!
            context.DoNarrationAndSetFlag("Act3/Optimist/ProcGen");    

            //Cynic: NO!
            context.DoNarrationAndSetFlag("Act3/Cynic/No");   

            //Oppy: Hold on. Wait, I was just messing with you, but this could actually be kind of interesting.
            context.DoNarrationAndSetFlag("Act3/Optimist/HoldOn");    
            //The pockets could get repositioned randomly every time you play-
            context.DoNarrationAndSetFlag("Act3/Optimist/PocketReposition");    

            //Cynic: Ok, stop talking and just - just listen to me! A) you haven’t gotten rid of the mechanics that you said were causing problems,
            context.DoNarrationAndSetFlag("Act3/Cynic/StopTalkingA");    
            //B) you haven’t solved any problems that I’ve pointed out so far, and C) procedural generation here is, quite frankly, a terrible idea.
            context.DoNarrationAndSetFlag("Act3/Cynic/StopTalkingBC");   
            
            //Oppy: *lighthearted* Relax. I’m just messing around. Wait, didn’t you put procedural generation in one of your first games?
            context.DoNarrationAndSetFlag("Act3/Optimist/JustMessing");    

            //Cynic: That game performed terribly! Look: it’s not like I hate procedural generation or anything. It can work great when done correctly.
            context.DoNarrationAndSetFlag("Act3/Cynic/GameTerrible"); 
            //But when you try to add in a feature without considering how it’ll impact all the other parts of the design, you’re almost guaranteed to fail.
            context.DoNarrationAndSetFlag("Act3/Cynic/TryAddFeature"); 
            //Sticking everything together with a bit of duct tape and praying for a hit is a recipe for disaster!
            context.DoNarrationAndSetFlag("Act3/Cynic/DuctTape");

            //Oppy: *sarcastically* Oh, so I should just copy what other people do all the time and never try to innovate at all?
            context.DoNarrationAndSetFlag("Act3/Optimist/Sarcastic");    

            //Cynic: No. You should always try to stand out from everyone else! Do you even understand what I’m saying? 
            context.DoNarrationAndSetFlag("Act3/Cynic/StandOut");
            //Adding more mechanics doesn’t always make a game more sophisticated. Here, they’re just overloading it! No one is going to want to play this.
            context.DoNarrationAndSetFlag("Act3/Cynic/MoreMechanics");
            //All the pockets get marked with “I’m still here”
            SetUpChoice("I'm still here", "I'm still here");
            //*silence*
            yield return new WaitUntil(() => { return madeDecision; });

            //Oppy: Did you ever ask yourself why I stuck around even though I hated your advice? You’re one of my favorite designers.
            context.DoNarrationAndSetFlag("Act3/Optimist/AskYourself");    
            //You’ve put so much into your work and gotten barely anything out of it.
            context.DoNarrationAndSetFlag("Act3/Optimist/PutSoMuch");     
            //I get that you hate my game, but there’s obviously some people that would like it. 
            context.DoNarrationAndSetFlag("Act3/Optimist/YouHate");    
            //The proof of that is playing my prototype right now! If I just make what I want to make, I’ll find an audience one day.
            context.DoNarrationAndSetFlag("Act3/Optimist/Proof");    


            //Cynic: You do what you want. But I don’t want to help you if you’re not going to listen to me.
            context.DoNarrationAndSetFlag("Act3/Cynic/DoWhatYouWant");
             
            //*insincerely* Thanks for showing me your game. I’ll see you around.
            context.DoNarrationAndSetFlag("Act3/Cynic/SeeYouAround");

            //Oppy: *disappointed* Ugh, good riddance. Never meet your heroes, right?
            context.DoNarrationAndSetFlag("Act3/Optimist/GoodRiddance");    

            //*silence*
            yield return new WaitForSeconds(3);
            //But maybe he was right. He’s got tons of time in the industry… maybe I should try out his advice.
            context.DoNarrationAndSetFlag("Act3/Optimist/MaybeRight");    


            //The game becomes regular pool except with different physics on all the balls.
            context.GetComponent<EndOfGame>().EndOfGameify();

            //Oppy: It’s something I came up with when we were talking. All the balls have slightly different interactions with everything.
            context.DoNarrationAndSetFlag("Act3/Optimist/Final1");    

            //Oppy: Yeah, I like how this plays. But which one should be worth the most? What if we…
            context.DoNarrationAndSetFlag("Act3/Optimist/Final2");    


 
            
            yield return null;   
        }
        private void SetUpChoice(string textLeft, string textRight)
        {
            c.SetChoiceText(textLeft, textRight);
            madeDecision = false;
            c.Activate();
            Choices.choiceEvent += (object sender, Choices.choiceEventArgs e) =>
            {
                madeDecision = true;
                wasDecisionLeft = e.choice;
            };
        }
    }

    #endregion

    #endregion
}

//For naming convention 
public enum GameState
{
    Default
}

