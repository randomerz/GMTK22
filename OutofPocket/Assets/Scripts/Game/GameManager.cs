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

            //Oppy … and I’ve already got someone playtesting it right now!
            context.DoNarrationAndSetFlag("Optimist/HelloWelcomeTo");
            yield return new WaitUntil(() => { return context.currNarrationFinished; });

            ////Cynic already?
            //context.DoNarrationAndSetFlag("Pessimist/HelloWelcomeTo");
            //yield return new WaitUntil(() => { return context.currNarrationFinished; });

            ////Oppy Crazy, right? Okay, so picture this…*elevator ding sound*
            //context.DoNarrationAndSetFlag("Optimist/HelloWelcomeTo");
            //yield return new WaitUntil(() => { return context.currNarrationFinished; });

            context.floor.SetActive(true);  //Turn on lights

            //Oppy A pool table
            context.DoNarrationAndSetFlag("Optimist/HelloWelcomeTo");
            yield return new WaitUntil(() => { return context.currNarrationFinished; });

            context.poolTable.SetActive(true);

            //Oppy and, and it’s like your standard pool game you know
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

            //Oppy …except, they’re not actually balls, they’re dice!
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
        private Choices c;
        private bool madeDecision;
        private bool wasDecisionLeft;

        public Act2State(GameManager ctx) : base(ctx)
        {
        }

        public override void EnterState()
        {
            c = Choices.GetInstance();
            context.StartCoroutine(DoAct2());
        }

        // author: boomo
        public IEnumerator DoAct2()
        {
            // Oppy: Well, looks like it’s just you and me for a little bit.
            context.DoNarrationAndSetFlag("Optimist/HelloWelcomeTo");
            yield return new WaitUntil(() => { return context.currNarrationFinished; });


            // Oppy: Okay, how about this. Shoot a dice into the far left pocket there to say “yes”, and the far right pocket to say “no”
            context.DoNarrationAndSetFlag("Optimist/HelloWelcomeTo");
            SetUpChoice("Yes", "No");
            yield return new WaitUntil(() => { return madeDecision; });

            if (wasDecisionLeft)
            {
                // Oppy: Yes! You can give feedback. That’s perfect
                context.DoNarrationAndSetFlag("Optimist/HelloWelcomeTo");
                yield return new WaitUntil(() => { return context.currNarrationFinished; });
            }
            else
            {
                // Oppy: No? Well you kinda just did what I said. So I’ll take that as a yes
                context.DoNarrationAndSetFlag("Optimist/HelloWelcomeTo");
                yield return new WaitUntil(() => { return context.currNarrationFinished; });
            }


            // Oppy: Hmmm, this could actually be a pretty interesting mechanic. I bet there’s something we could do with it…
            context.DoNarrationAndSetFlag("Optimist/HelloWelcomeTo");
            SetUpChoice("Tell a Story", "Not really");
            yield return new WaitUntil(() => { return madeDecision; });

            if (wasDecisionLeft)
            {
                // Oppy: A story. Good idea! That’s the best part about games really. And you can play an important role in choosing where it goes!
                context.DoNarrationAndSetFlag("Optimist/HelloWelcomeTo");
                yield return new WaitUntil(() => { return context.currNarrationFinished; });
            }
            else
            {
                // Oppy: Ahh, cmon, you can see the potential! I bet this would work great with a branching story
                context.DoNarrationAndSetFlag("Optimist/HelloWelcomeTo");
                yield return new WaitUntil(() => { return context.currNarrationFinished; });
            }


            // Cynic: Hey, I’m back. What'd I miss
            context.DoNarrationAndSetFlag("Optimist/HelloWelcomeTo");
            yield return new WaitUntil(() => { return context.currNarrationFinished; });

            // Oppy: Oh uh
            context.DoNarrationAndSetFlag("Optimist/HelloWelcomeTo");
            SetUpChoice("Truth", "Lie");
            yield return new WaitUntil(() => { return madeDecision; });

            if (wasDecisionLeft)
            {
                // Oppy: I was thinking, maybe the game could use more story. We can use the different pockets to give the player choices in how the story plays out
                context.DoNarrationAndSetFlag("Optimist/HelloWelcomeTo");
                yield return new WaitUntil(() => { return context.currNarrationFinished; });

                // Cynic: Story? You think a bloody game about pool and dice needs a story? AND you wanna make it dynamic??
                context.DoNarrationAndSetFlag("Optimist/HelloWelcomeTo");
                SetUpChoice("Passive", "Aggrssive");
                yield return new WaitUntil(() => { return madeDecision; });

                if (wasDecisionLeft)
                {
                    // Oppy:  I- I don’t know. I just thought… every game needs a story, you know? And since it’s a game, why not make it dynamic?
                    context.DoNarrationAndSetFlag("Optimist/HelloWelcomeTo");
                    yield return new WaitUntil(() => { return context.currNarrationFinished; });

                    // Cynic: The problem with a branching story with multiple endings is that it always leaves the player wondering whether they got the “real” ending or not.
                    context.DoNarrationAndSetFlag("Optimist/HelloWelcomeTo");
                    yield return new WaitUntil(() => { return context.currNarrationFinished; });

                    // Cynic: I hate to say it, but most times when a story branches out, its only superficial. It always comes to the same ending.
                    context.DoNarrationAndSetFlag("Optimist/HelloWelcomeTo");
                    yield return new WaitUntil(() => { return context.currNarrationFinished; });
                }
                else
                {
                    // Oppy: Woah, calm down. If I wanted someone to yell at me I would’ve gotten the boss. You won’t even hear me out before you shut me down.
                    context.DoNarrationAndSetFlag("Optimist/HelloWelcomeTo");
                    yield return new WaitUntil(() => { return context.currNarrationFinished; });

                    // Cynic: Well, maybe if you spent more than 5 seconds on each idea you’d get more out of me. Rapid firinge ideas like these always comes to the same ending
                    context.DoNarrationAndSetFlag("Optimist/HelloWelcomeTo");
                    yield return new WaitUntil(() => { return context.currNarrationFinished; });
                }
            }
            else
            {
                // Oppy: Oh uh, nothing much has changed really. I was just er uh, tightening the graphics a bit
                context.DoNarrationAndSetFlag("Optimist/HelloWelcomeTo");
                yield return new WaitUntil(() => { return context.currNarrationFinished; });

                // Cynic: Do they feel good now? I’m gonna be honest I don’t think I notice a difference
                context.DoNarrationAndSetFlag("Optimist/HelloWelcomeTo");
                SetUpChoice("Yes", "No");
                yield return new WaitUntil(() => { return madeDecision; });

                if (wasDecisionLeft)
                {
                    // Oppy:  yes! Yes they definitely feel better to me.
                    context.DoNarrationAndSetFlag("Optimist/HelloWelcomeTo");
                    yield return new WaitUntil(() => { return context.currNarrationFinished; });

                    // Cynic: Hmm. well if you’re sure. A lot of improvements that seem huge to you go unnoticed by almost every player. 
                    context.DoNarrationAndSetFlag("Optimist/HelloWelcomeTo");
                    yield return new WaitUntil(() => { return context.currNarrationFinished; });

                    // Cynic: Some indies spend their 20s praying for the big names to notice them… but their work gets buried in Steam and forgotten. Their careers always get the same ending.
                    context.DoNarrationAndSetFlag("Optimist/HelloWelcomeTo");
                    yield return new WaitUntil(() => { return context.currNarrationFinished; });
                }
                else
                {
                    // Oppy: No… they really aren’t much better.
                    context.DoNarrationAndSetFlag("Optimist/HelloWelcomeTo");
                    yield return new WaitUntil(() => { return context.currNarrationFinished; });

                    // Cynic: Hey, don’t stress about it. Graphics aren’t everything. Most indie games I see, a little extra polish doesn’t make a difference. 
                    context.DoNarrationAndSetFlag("Optimist/HelloWelcomeTo");
                    yield return new WaitUntil(() => { return context.currNarrationFinished; });

                    // Cynic: Some people spend their 20s praying for the big names to notice them, but their work gets buried in Steam and forgotten. Their careers always get the same ending.
                    context.DoNarrationAndSetFlag("Optimist/HelloWelcomeTo");
                    yield return new WaitUntil(() => { return context.currNarrationFinished; });
                }
            }


            // Oppy: I feel like there’s a story associated with what you’re saying.
            context.DoNarrationAndSetFlag("Optimist/HelloWelcomeTo");
            yield return new WaitUntil(() => { return context.currNarrationFinished; });

            // Cynic: Yeah, it’s a pretty short story that goes a little something like this: that’s none of your business.
            context.DoNarrationAndSetFlag("Optimist/HelloWelcomeTo");
            yield return new WaitUntil(() => { return context.currNarrationFinished; });

            // Cynic: Ok, I want to talk about the score. Does it affect gameplay, or is it just for brownie points?
            context.DoNarrationAndSetFlag("Optimist/HelloWelcomeTo");
            yield return new WaitUntil(() => { return context.currNarrationFinished; });

            // Oppy: Ya know, I didn’t think about that one.
            context.DoNarrationAndSetFlag("Optimist/HelloWelcomeTo");
            SetUpChoice("Gameplay", "Bragging Rights");
            yield return new WaitUntil(() => { return madeDecision; });

            if (wasDecisionLeft)
            {
                // Oppy: Yeah, I gotta agree with that choice.
                context.DoNarrationAndSetFlag("Optimist/HelloWelcomeTo");
                yield return new WaitUntil(() => { return context.currNarrationFinished; });
            }
            else
            {
                // Oppy: Eh, I can see where you’re coming from. Making the game arcade-style would be pretty neat. But… hear me out y’all…
                context.DoNarrationAndSetFlag("Optimist/HelloWelcomeTo");
                yield return new WaitUntil(() => { return context.currNarrationFinished; });
            }


            // Oppy: There’s gotta be a way to link score and gameplay without any rough edges. How about we turn your score into currency that you can spend in a shop?
            context.DoNarrationAndSetFlag("Optimist/HelloWelcomeTo");
            yield return new WaitUntil(() => { return context.currNarrationFinished; });

            // TODO: SHOP STUFF ####################################################################################

            // Oppy: Ah, see how nice it looks?
            context.DoNarrationAndSetFlag("Optimist/HelloWelcomeTo");
            yield return new WaitUntil(() => { return context.currNarrationFinished; });

            // Cynic: It’s the visual equivalent of stepping on a lego brick. And nothing you can buy here is useful! It’s just cosmetics! 
            context.DoNarrationAndSetFlag("Optimist/HelloWelcomeTo");
            yield return new WaitUntil(() => { return context.currNarrationFinished; });

            // Cynic: You’ve succeeded in making the score valuable, but only superficially. I really think you should make some changes to the gameplay.
            context.DoNarrationAndSetFlag("Optimist/HelloWelcomeTo");
            yield return new WaitUntil(() => { return context.currNarrationFinished; });

            // Oppy: Ok then, we’ll add powerups! What do you think would be cooler, a slow-mo cue ball, or a giant one?
            context.DoNarrationAndSetFlag("Optimist/HelloWelcomeTo");
            SetUpChoice("Slow-Mo Ball", "Giant Ball");
            yield return new WaitUntil(() => { return madeDecision; });

            if (wasDecisionLeft)
            {
                // TODO: POWER STUFF ####################################################################################
            }
            else
            {
                // TODO: POWER STUFF ####################################################################################
            }


            // Cynic: What a surprise. The powerups do nothing to change the gameplay. In fact, they’re really annoying to use! You should -
            context.DoNarrationAndSetFlag("Optimist/HelloWelcomeTo");
            yield return new WaitUntil(() => { return context.currNarrationFinished; });

            // Oppy: Excuse you! This is my game; I’ll put in whatever I want. How are you so sure this won’t work?
            context.DoNarrationAndSetFlag("Optimist/HelloWelcomeTo");
            yield return new WaitUntil(() => { return context.currNarrationFinished; });

            // Oppy: You say I’m not the one listening, but you can never hear the benefits of any of my ideas! You’ve never even played a game like this!
            context.DoNarrationAndSetFlag("Optimist/HelloWelcomeTo");
            yield return new WaitUntil(() => { return context.currNarrationFinished; });

            // Cynic: *exasperated* That’s true! Let’s ask the one who’s actually playing. What do you think about the mechanic?
            context.DoNarrationAndSetFlag("Optimist/HelloWelcomeTo");
            SetUpChoice("Like 'em", "Hate 'em");
            yield return new WaitUntil(() => { return madeDecision; });

            if (wasDecisionLeft)
            {
                // Oppy: Hah! I knew it!
                context.DoNarrationAndSetFlag("Optimist/HelloWelcomeTo");
                yield return new WaitUntil(() => { return context.currNarrationFinished; });
            }
            else
            {
                // Oppy: Well, maybe they’ll grow on ya.
                context.DoNarrationAndSetFlag("Optimist/HelloWelcomeTo");
                yield return new WaitUntil(() => { return context.currNarrationFinished; });
            }

            // Oppy: We’ll keep 'em for now. But I do agree the gameplay could use a little facelift. We need something dramatic. And flashy! Something already tried, tested, and proven to be fun!
            context.DoNarrationAndSetFlag("Optimist/HelloWelcomeTo");
            yield return new WaitUntil(() => { return context.currNarrationFinished; });

            // Cynic: Everybody shush he’s about to make a breakthrough
            context.DoNarrationAndSetFlag("Optimist/HelloWelcomeTo");
            yield return new WaitUntil(() => { return context.currNarrationFinished; });

            // Oppy: We’ll combine this game… with superhot!
            context.DoNarrationAndSetFlag("Optimist/HelloWelcomeTo");
            yield return new WaitUntil(() => { return context.currNarrationFinished; });

            // SUPER HOT ####################################################################

            // Oppy: Now, time will only move when the cue ball moves! I’ve finally cracked the code
            context.DoNarrationAndSetFlag("Optimist/HelloWelcomeTo");

            yield return new WaitForSeconds(15f);

            // Oppy: We’ll combine this game… with superhot!
            context.DoNarrationAndSetFlag("Optimist/HelloWelcomeTo");
            yield return new WaitUntil(() => { return context.currNarrationFinished; });

            // Oppy: I can’t say I didn’t see this coming. You haven’t actually addressed the core issues of the game, so none of your problems were solved. You just created new ones!
            context.DoNarrationAndSetFlag("Optimist/HelloWelcomeTo");
            yield return new WaitUntil(() => { return context.currNarrationFinished; });
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

    #region Act 3
    public class Act3State : State<GameManager>
    {
        public Act3State(GameManager ctx) : base(ctx)
        {
        }

        public override void EnterState()
        {
            context.StartCoroutine(DoAct3());
        }

        public IEnumerator DoAct3()
        {
            yield return null;   
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

