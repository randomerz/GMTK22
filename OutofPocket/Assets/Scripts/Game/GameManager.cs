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

            //Oppy ... and I've already got someone playtesting it right now!
            context.DoNarrationAndSetFlag("Act1/Oppy/001_Playtest");
            yield return new WaitUntil(() => { return context.currNarrationFinished; });

            //Cynic already?
            context.DoNarrationAndSetFlag("Act1/Cynic/002_already");
            yield return new WaitUntil(() => { return context.currNarrationFinished; });

            //Turn on lights
            context.floor.SetActive(true);

            //Oppy Crazy, right? Okay, so picture this�*elevator ding sound*
            context.DoNarrationAndSetFlag("Act1/Oppy/003_PictureThis");
            yield return new WaitUntil(() => { return context.currNarrationFinished; });



            //Turn on pool table
            context.poolTable.SetActive(true);

            //Oppy A pool table
            context.DoNarrationAndSetFlag("Act1/Oppy/004_PoolTable");
            yield return new WaitUntil(() => { return context.currNarrationFinished; });

            //Oppy and, and it�s like your standard pool game you know
            context.DoNarrationAndSetFlag("Act1/Oppy/005_PoolGame");
            yield return new WaitUntil(() => { return context.currNarrationFinished; });

            //Enable the pool game
            context.poolBallTriangle.SetActive(true);
            context.cueBall.SetActive(true);

            //Oppy �except, they�re not actually balls, they�re dice!
            context.DoNarrationAndSetFlag("Act1/Oppy/006_DICE");
            yield return new WaitUntil(() => { return context.currNarrationFinished; });

            //Change all balls to dice
            PoolStateManager.ChangeAllToShape(PoolBall.Shape.Dice);
            PoolStateManager.ResetGame();
            context.poolGameManager.gameObject.SetActive(true);            //Enable the game and the controls.
            context.inGameUI.SetActive(true);

            playerPutBallInPocket = false;
            PoolBall.ballInPocketEvent += PutBallInPocket;  //Start keeping track of if the player pocketed the die.

            //Cynic: Dice?
            context.DoNarrationAndSetFlag("Act1/Cynic/007_Dice");
            yield return new WaitUntil(() => { return context.currNarrationFinished; });

            //Oppy: Yeah, so I got to thinking about making a pool game with a twist...
            context.DoNarrationAndSetFlag("Act1/Oppy/008_DiceExpl");
            yield return new WaitUntil(() => { return context.currNarrationFinished; });

            //Wait until ball in pocket.
            yield return new WaitUntil(() => playerPutBallInPocket);
            PoolBall.ballInPocketEvent -= PutBallInPocket;

            //Cynic: That�s stupid.
            context.DoNarrationAndSetFlag("Act1/Cynic/009_Stupid");
            yield return new WaitUntil(() => { return context.currNarrationFinished; });

            //Oppy: *pause for a moment*what ?
            context.DoNarrationAndSetFlag("Act1/Oppy/010_What");
            yield return new WaitUntil(() => { return context.currNarrationFinished; });

            //Cynic: I said that�s stupid. How does changing them from balls to cubes make the game more about skill?
            context.DoNarrationAndSetFlag("Act1/Cynic/011_SkillIssue");
            yield return new WaitUntil(() => { return context.currNarrationFinished; });

            //Oppy: Well�
            context.DoNarrationAndSetFlag("Act1/Oppy/012_Well");
            yield return new WaitUntil(() => { return context.currNarrationFinished; });

            //Cynic: Weren�t pool balls made to be balls for a reason?
            context.DoNarrationAndSetFlag("Act1/Cynic/013_Reason");
            yield return new WaitUntil(() => { return context.currNarrationFinished; });

            //Oppy: Yeah, but this is new!
            context.DoNarrationAndSetFlag("Act1/Oppy/014_new");
            yield return new WaitUntil(() => { return context.currNarrationFinished; });

            //Cynic: Just because something is new doesn�t make it better. You can�t just mash two ideas together and expect it to be fun.
            context.DoNarrationAndSetFlag("Act1/Cynic/015_NewNotBetter");
            yield return new WaitUntil(() => { return context.currNarrationFinished; });

            //Oppy: But I haven�t even told you about the numbers!
            context.DoNarrationAndSetFlag("Act1/Oppy/016_numbers");
            yield return new WaitUntil(() => { return context.currNarrationFinished; });

            //Cynic: You mean the numbers on the dice?
            context.DoNarrationAndSetFlag("Act1/Cynic/017_Numbers");
            yield return new WaitUntil(() => { return context.currNarrationFinished; });

            //Oppy: There are so many possibilities! We could make the numbers add more balls to the table, or maybe only odd numbers give you points, or something else.
            context.DoNarrationAndSetFlag("Act1/Oppy/018_Possibilities");
            yield return new WaitUntil(() => { return context.currNarrationFinished; });

            //Cynic: Okay I really need to stop you there. Having �possibilities� is not the same as having �ideas�. *upbeat * Be decisive and pick one!Can you settle on one for christ�s sake ?
            context.DoNarrationAndSetFlag("Act1/Cynic/019_PossibilitiesNotIdeas");
            yield return new WaitUntil(() => { return context.currNarrationFinished; });

            //Oppy: Ok, ok.When you pocket something, you earn however many points are on the top face of the die!
           context.DoNarrationAndSetFlag("Act1/Oppy/020_Pocket");
            yield return new WaitUntil(() => { return context.currNarrationFinished; });

            //Enable Score
            PoolStateManager._instance.ScoreEnabled = true;
            PoolStateManager.scoreMode = PoolStateManager.ScoreMode.ONLY_ONES;

            playerPutBallInPocket = false;
            PoolBall.ballInPocketEvent += PutBallInPocket;  //Start keeping track of if the player pocketed the die.

            //Cynic: So its like a random chance to get 1 - 6 points�
            context.DoNarrationAndSetFlag("Act1/Cynic/021_Random");
            yield return new WaitUntil(() => { return context.currNarrationFinished; });

            //Oppy: No! Watch, the playtester will get a 6 in soon!
            context.DoNarrationAndSetFlag("Act1/Oppy/022_WatchFor6");
            yield return new WaitUntil(() => { return context.currNarrationFinished; });

            //*Wait until ball in pocket.
            yield return new WaitUntil(() => playerPutBallInPocket);
            PoolBall.ballInPocketEvent -= PutBallInPocket;

            //Cynic:
            context.DoNarrationAndSetFlag("Act1/Cynic/023_Empty");
            yield return new WaitUntil(() => { return context.currNarrationFinished; });

            //Oppy:
            context.DoNarrationAndSetFlag("Act1/Oppy/024_Empty");
            yield return new WaitUntil(() => { return context.currNarrationFinished; });

            //Cynic: Okay, I still don�t think there�s enough control here. You�re just hitting the cube and praying for a 6.
            context.DoNarrationAndSetFlag("Act1/Cynic/025_Praying");
            yield return new WaitUntil(() => { return context.currNarrationFinished; });

            //Oppy: Hmm� I have an idea
            context.DoNarrationAndSetFlag("Act1/Oppy/026_AnIdea");
            yield return new WaitUntil(() => { return context.currNarrationFinished; });

            //Cynic: Oh boy Oh good, I love it when you get those
            context.DoNarrationAndSetFlag("Act1/Cynic/027_LoveIt");
            yield return new WaitUntil(() => { return context.currNarrationFinished; });

            //Oppy: I was thinking about this time I played pool with my friends ...
            context.DoNarrationAndSetFlag("Act1/Oppy/028_TiltExplanation");
            yield return new WaitUntil(() => { return context.currNarrationFinished; });

            //Cynic: So� tilt controls ?
            context.DoNarrationAndSetFlag("Act1/Cynic/029_TiltControls");
            yield return new WaitUntil(() => { return context.currNarrationFinished; });

            //Enable Tilting
            PoolStateManager._instance.TiltingEnabled = true;

            //Oppy: Exactly! Now the player has control over where the balls� *erm * dice go
            context.DoNarrationAndSetFlag("Act1/Oppy/030_TiltActivate");
            yield return new WaitUntil(() => { return context.currNarrationFinished; });

            //Cynic: Hmmm� so the player is tilting the table? And they can use this strategically as another way to get the dice in?
            context.DoNarrationAndSetFlag("Act1/Cynic/031_TiltStrategy");
            yield return new WaitUntil(() => { return context.currNarrationFinished; });

            //[Cut off if player tilts a bunch of dice in, else skip]
            yield return new WaitUntil(() => { return PoolStateManager._instance.numBallsSunk > 5; });

            //Cynic: No no no! You see how many they just got in? That�s not skill
            context.DoNarrationAndSetFlag("Act1/Cynic/031a_NotSkill");
            yield return new WaitUntil(() => { return context.currNarrationFinished; });

            //Oppy: Look! How exciting! Their score shot way up!
            context.DoNarrationAndSetFlag("Act1/Oppy/032_HowExciting");
            yield return new WaitUntil(() => { return context.currNarrationFinished; });

            //Cynic: Sigh, That�s not exciting! You completely removed any semblance of strategy your �game� once had. Now it�s just a QTE of a bunch of cubes falling into holes.
            context.DoNarrationAndSetFlag("Act1/Cynic/033_QTE");
            yield return new WaitUntil(() => { return context.currNarrationFinished; });

            //Oppy: Oh. Huh. Well, nothing that a little juice and polish can�t fix! Here, it just needs a little more � uh� 
            context.DoNarrationAndSetFlag("Act1/Oppy/034_JuiceStart");
            yield return new WaitUntil(() => { return context.currNarrationFinished; });

            //Cynic: I�m sure that�ll do wonders for the gameplay a little more ?
            context.DoNarrationAndSetFlag("Act1/Cynic/035_Gameplay");
            yield return new WaitUntil(() => { return context.currNarrationFinished; });

            //Oppy : Juice! Yeah, Ok hold on, I saw some tutorials about this online.Behold, SCREENSHAKE!
            context.DoNarrationAndSetFlag("Act1/Oppy/036_SCREENSHAKE");
            yield return new WaitUntil(() => { return context.currNarrationFinished; });

            //Screenshake is added (ADD THIS!!!!!!!!!!!!!!)

            //Oppy: and now, the cherry on top: , and just a little bitta particle effects as the cherry on top!
            context.DoNarrationAndSetFlag("Act1/Oppy/037_ParticleEffects");
            yield return new WaitUntil(() => { return context.currNarrationFinished; });

            //Particle effects added (ADD THIS!!!!!!!!!!!!!!)

            //Oppy: mwa! Magnifique!
            context.DoNarrationAndSetFlag("Act1/Oppy/038_Magnifique");
            yield return new WaitUntil(() => { return context.currNarrationFinished; });

            //Cynic: Well uh, aesthetics are� important.Look kid, I�m gonna go grab a coffee.If you�ve got more to say, I guess stick around and I�ll be back.But I gotta take a break.
            context.DoNarrationAndSetFlag("Act1/Cynic/039_Aesthetic");
            yield return new WaitUntil(() => { return context.currNarrationFinished; });

            //Oppy: Oh, uh okay. Don�t worry, I�ll be right here!
            context.DoNarrationAndSetFlag("Act1/Oppy/040_Act1Finish");
            yield return new WaitUntil(() => { return context.currNarrationFinished; });

            //Cynic: I�m sure you will� *footstep sounds *
            context.DoNarrationAndSetFlag("Act1/Cynic/041_Act1Finish");
            yield return new WaitUntil(() => { return context.currNarrationFinished; });
        }

        private void SetTurnCompleted(object sender, System.EventArgs e)
        {
            turnCompleted = true;
        }

        private void PutBallInPocket(object sender, PoolBall.BallEventArgs e)
        {
            playerPutBallInPocket = true;
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
            // Oppy: Well, looks like it�s just you and me for a little bit. I guess now�s a good time to get some feedback on the game! It�s not over or anything, I just thought it�d be nice right now.
            context.DoNarrationAndSetFlag("Optimist/HelloWelcomeTo");
            yield return new WaitUntil(() => { return context.currNarrationFinished; });


            // Oppy: Okay, how about this. Shoot a dice into the far left pocket there to say �yes�, and the far right pocket to say �no�
            context.DoNarrationAndSetFlag("Optimist/HelloWelcomeTo");
            SetUpChoice("Yes", "No");
            yield return new WaitUntil(() => { return madeDecision; });

            if (wasDecisionLeft)
            {
                // Oppy: Yes! You can give feedback. That�s perfect
                context.DoNarrationAndSetFlag("Optimist/HelloWelcomeTo");
                yield return new WaitUntil(() => { return context.currNarrationFinished; });
            }
            else
            {
                // Oppy: No? Well you kinda just did what I said. So I�ll take that as a yes
                context.DoNarrationAndSetFlag("Optimist/HelloWelcomeTo");
                yield return new WaitUntil(() => { return context.currNarrationFinished; });
            }


            // Oppy: Hmmm, this could actually be a pretty interesting mechanic. I bet there�s something we could do with it�
            context.DoNarrationAndSetFlag("Optimist/HelloWelcomeTo");
            SetUpChoice("Tell a Story", "Not really");
            yield return new WaitUntil(() => { return madeDecision; });

            if (wasDecisionLeft)
            {
                // Oppy: A story. Good idea! That�s the best part about games really. And you can play an important role in choosing where it goes!
                context.DoNarrationAndSetFlag("Optimist/HelloWelcomeTo");
                yield return new WaitUntil(() => { return context.currNarrationFinished; });
            }
            else
            {
                // Oppy: Ahh, cmon, you can see the potential! I bet this would work great with a branching story
                context.DoNarrationAndSetFlag("Optimist/HelloWelcomeTo");
                yield return new WaitUntil(() => { return context.currNarrationFinished; });
            }


            // Cynic: Hey, I�m back. What'd I miss
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
                    // Oppy:  I- I don�t know. I just thought� every game needs a story, you know? And since it�s a game, why not make it dynamic?
                    context.DoNarrationAndSetFlag("Optimist/HelloWelcomeTo");
                    yield return new WaitUntil(() => { return context.currNarrationFinished; });

                    // Cynic: The problem with a branching story with multiple endings is that it always leaves the player wondering whether they got the �real� ending or not.
                    context.DoNarrationAndSetFlag("Optimist/HelloWelcomeTo");
                    yield return new WaitUntil(() => { return context.currNarrationFinished; });

                    // Cynic: I hate to say it, but most times when a story branches out, its only superficial. It always comes to the same ending.
                    context.DoNarrationAndSetFlag("Optimist/HelloWelcomeTo");
                    yield return new WaitUntil(() => { return context.currNarrationFinished; });
                }
                else
                {
                    // Oppy: Woah, calm down. If I wanted someone to yell at me I would�ve gotten the boss. You won�t even hear me out before you shut me down.
                    context.DoNarrationAndSetFlag("Optimist/HelloWelcomeTo");
                    yield return new WaitUntil(() => { return context.currNarrationFinished; });

                    // Cynic: Well, maybe if you spent more than 5 seconds on each idea you�d get more out of me. Rapid firinge ideas like these always comes to the same ending
                    context.DoNarrationAndSetFlag("Optimist/HelloWelcomeTo");
                    yield return new WaitUntil(() => { return context.currNarrationFinished; });
                }
            }
            else
            {
                // Oppy: Oh uh, nothing much has changed really. I was just er uh, tightening the graphics a bit
                context.DoNarrationAndSetFlag("Optimist/HelloWelcomeTo");
                yield return new WaitUntil(() => { return context.currNarrationFinished; });

                // Cynic: Do they feel good now? I�m gonna be honest I don�t think I notice a difference
                context.DoNarrationAndSetFlag("Optimist/HelloWelcomeTo");
                SetUpChoice("Yes", "No");
                yield return new WaitUntil(() => { return madeDecision; });

                if (wasDecisionLeft)
                {
                    // Oppy:  yes! Yes they definitely feel better to me.
                    context.DoNarrationAndSetFlag("Optimist/HelloWelcomeTo");
                    yield return new WaitUntil(() => { return context.currNarrationFinished; });

                    // Cynic: Hmm. well if you�re sure. A lot of improvements that seem huge to you go unnoticed by almost every player. 
                    context.DoNarrationAndSetFlag("Optimist/HelloWelcomeTo");
                    yield return new WaitUntil(() => { return context.currNarrationFinished; });

                    // Cynic: Some indies spend their 20s praying for the big names to notice them� but their work gets buried in Steam and forgotten. Their careers always get the same ending.
                    context.DoNarrationAndSetFlag("Optimist/HelloWelcomeTo");
                    yield return new WaitUntil(() => { return context.currNarrationFinished; });
                }
                else
                {
                    // Oppy: No� they really aren�t much better.
                    context.DoNarrationAndSetFlag("Optimist/HelloWelcomeTo");
                    yield return new WaitUntil(() => { return context.currNarrationFinished; });

                    // Cynic: Hey, don�t stress about it. Graphics aren�t everything. Most indie games I see, a little extra polish doesn�t make a difference. 
                    context.DoNarrationAndSetFlag("Optimist/HelloWelcomeTo");
                    yield return new WaitUntil(() => { return context.currNarrationFinished; });

                    // Cynic: Some people spend their 20s praying for the big names to notice them, but their work gets buried in Steam and forgotten. Their careers always get the same ending.
                    context.DoNarrationAndSetFlag("Optimist/HelloWelcomeTo");
                    yield return new WaitUntil(() => { return context.currNarrationFinished; });
                }
            }


            // Oppy: I feel like there�s a story associated with what you�re saying.
            context.DoNarrationAndSetFlag("Optimist/HelloWelcomeTo");
            yield return new WaitUntil(() => { return context.currNarrationFinished; });

            // Cynic: Yeah, it�s a pretty short story that goes a little something like this: that�s none of your business.
            context.DoNarrationAndSetFlag("Optimist/HelloWelcomeTo");
            yield return new WaitUntil(() => { return context.currNarrationFinished; });

            // Cynic: Ok, I want to talk about the score. Does it affect gameplay, or is it just for brownie points?
            context.DoNarrationAndSetFlag("Optimist/HelloWelcomeTo");
            yield return new WaitUntil(() => { return context.currNarrationFinished; });

            // Oppy: Ya know, I didn�t think about that one.
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
                // Oppy: Eh, I can see where you�re coming from. Making the game arcade-style would be pretty neat. But� hear me out y�all�
                context.DoNarrationAndSetFlag("Optimist/HelloWelcomeTo");
                yield return new WaitUntil(() => { return context.currNarrationFinished; });
            }


            // Oppy: There�s gotta be a way to link score and gameplay without any rough edges. How about we turn your score into currency that you can spend in a shop?
            context.DoNarrationAndSetFlag("Optimist/HelloWelcomeTo");
            yield return new WaitUntil(() => { return context.currNarrationFinished; });

            // TODO: SHOP STUFF ####################################################################################

            // Oppy: Ah, see how nice it looks?
            context.DoNarrationAndSetFlag("Optimist/HelloWelcomeTo");
            yield return new WaitUntil(() => { return context.currNarrationFinished; });

            // Cynic: It�s the visual equivalent of stepping on a lego brick. And nothing you can buy here is useful! It�s just cosmetics! 
            context.DoNarrationAndSetFlag("Optimist/HelloWelcomeTo");
            yield return new WaitUntil(() => { return context.currNarrationFinished; });

            // Cynic: You�ve succeeded in making the score valuable, but only superficially. I really think you should make some changes to the gameplay.
            context.DoNarrationAndSetFlag("Optimist/HelloWelcomeTo");
            yield return new WaitUntil(() => { return context.currNarrationFinished; });

            // Oppy: Ok then, we�ll add powerups! What do you think would be cooler, a slow-mo cue ball, or a giant one?
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


            // Cynic: What a surprise. The powerups do nothing to change the gameplay. In fact, they�re really annoying to use! You should -
            context.DoNarrationAndSetFlag("Optimist/HelloWelcomeTo");
            yield return new WaitUntil(() => { return context.currNarrationFinished; });

            // Oppy: Excuse you! This is my game; I�ll put in whatever I want. How are you so sure this won�t work?
            context.DoNarrationAndSetFlag("Optimist/HelloWelcomeTo");
            yield return new WaitUntil(() => { return context.currNarrationFinished; });

            // Oppy: You say I�m not the one listening, but you can never hear the benefits of any of my ideas! You�ve never even played a game like this!
            context.DoNarrationAndSetFlag("Optimist/HelloWelcomeTo");
            yield return new WaitUntil(() => { return context.currNarrationFinished; });

            // Cynic: *exasperated* That�s true! Let�s ask the one who�s actually playing. What do you think about the mechanic?
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
                // Oppy: Well, maybe they�ll grow on ya.
                context.DoNarrationAndSetFlag("Optimist/HelloWelcomeTo");
                yield return new WaitUntil(() => { return context.currNarrationFinished; });
            }

            // Oppy: We�ll keep 'em for now. But I do agree the gameplay could use a little facelift. We need something dramatic. And flashy! Something already tried, tested, and proven to be fun!
            context.DoNarrationAndSetFlag("Optimist/HelloWelcomeTo");
            yield return new WaitUntil(() => { return context.currNarrationFinished; });

            // Cynic: Everybody shush he�s about to make a breakthrough
            context.DoNarrationAndSetFlag("Optimist/HelloWelcomeTo");
            yield return new WaitUntil(() => { return context.currNarrationFinished; });

            // Oppy: We�ll combine this game� with superhot!
            context.DoNarrationAndSetFlag("Optimist/HelloWelcomeTo");
            yield return new WaitUntil(() => { return context.currNarrationFinished; });

            // SUPER HOT ####################################################################

            // Oppy: Now, time will only move when the cue ball moves! I�ve finally cracked the code
            context.DoNarrationAndSetFlag("Optimist/HelloWelcomeTo");

            yield return new WaitForSeconds(15f);

            // Oppy: We�ll combine this game� with superhot!
            context.DoNarrationAndSetFlag("Optimist/HelloWelcomeTo");
            yield return new WaitUntil(() => { return context.currNarrationFinished; });

            // Oppy: I can�t say I didn�t see this coming. You haven�t actually addressed the core issues of the game, so none of your problems were solved. You just created new ones!
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
            context.DoNarrationAndSetFlag("Act3/Optimist/01_IveGotIt");    
            //but none of them have actually solved the core problem of “how do we make dice with pool work.”
            context.DoNarrationAndSetFlag("Act3/Optimist/02_CoreProblem");    
            
            //Cynic: *interested* Go on.
            context.DoNarrationAndSetFlag("Act3/Cynic/03_GoOn");    

            //Oppy: Well, I got to thinking. If adding these mechanics in hasn’t helped at all, why don’t we take away the ones that haven’t worked…
            context.DoNarrationAndSetFlag("Act3/Optimist/04_AddingMechanics");    

            //Cynic: *interested* Ok…
            context.DoNarrationAndSetFlag("Act3/Cynic/05_Ok");   

            //Oppy: *laughing* and add procedural generation!
            context.DoNarrationAndSetFlag("Act3/Optimist/06_ProcGen");    

            //Cynic: NO!
            context.DoNarrationAndSetFlag("Act3/Cynic/07_No");   

            //Oppy: Hold on. Wait, I was just messing with you, but this could actually be kind of interesting.
            context.DoNarrationAndSetFlag("Act3/Optimist/08_HoldOn");    
            //The pockets could get repositioned randomly every time you play-
            context.DoNarrationAndSetFlag("Act3/Optimist/09_PocketReposition");    

            //Cynic: Ok, stop talking and just - just listen to me! A) you haven’t gotten rid of the mechanics that you said were causing problems,
            context.DoNarrationAndSetFlag("Act3/Cynic/10_StopTalkingA");    
            //B) you haven’t solved any problems that I’ve pointed out so far, and C) procedural generation here is, quite frankly, a terrible idea.
            context.DoNarrationAndSetFlag("Act3/Cynic/11_StopTalkingBC");   
            
            //Oppy: *lighthearted* Relax. I’m just messing around. Wait, didn’t you put procedural generation in one of your first games?
            context.DoNarrationAndSetFlag("Act3/Optimist/12_JustMessing");    

            //Cynic: That game performed terribly! Look: it’s not like I hate procedural generation or anything. It can work great when done correctly.
            context.DoNarrationAndSetFlag("Act3/Cynic/13_GameTerrible"); 
            //But when you try to add in a feature without considering how it’ll impact all the other parts of the design, you’re almost guaranteed to fail.
            context.DoNarrationAndSetFlag("Act3/Cynic/14_TryAddFeature"); 
            //Sticking everything together with a bit of duct tape and praying for a hit is a recipe for disaster!
            context.DoNarrationAndSetFlag("Act3/Cynic/15_DuctTape");

            //Oppy: *sarcastically* Oh, so I should just copy what other people do all the time and never try to innovate at all?
            context.DoNarrationAndSetFlag("Act3/Optimist/16_Sarcastic");    

            //Cynic: No. You should always try to stand out from everyone else! Do you even understand what I’m saying? 
            context.DoNarrationAndSetFlag("Act3/Cynic/17_StandOut");
            //Adding more mechanics doesn’t always make a game more sophisticated. Here, they’re just overloading it! No one is going to want to play this.
            context.DoNarrationAndSetFlag("Act3/Cynic/18_MoreMechanics");
            //All the pockets get marked with “I’m still here”
            SetUpChoice("I'm still here", "I'm still here");
            //*silence*
            yield return new WaitUntil(() => { return madeDecision; });

            //Oppy: Did you ever ask yourself why I stuck around even though I hated your advice? You’re one of my favorite designers.
            context.DoNarrationAndSetFlag("Act3/Optimist/19_AskYourself");    
            //You’ve put so much into your work and gotten barely anything out of it.
            context.DoNarrationAndSetFlag("Act3/Optimist/20_PutSoMuch");     
            //I get that you hate my game, but there’s obviously some people that would like it. 
            context.DoNarrationAndSetFlag("Act3/Optimist/21_YouHate");    
            //The proof of that is playing my prototype right now! If I just make what I want to make, I’ll find an audience one day.
            context.DoNarrationAndSetFlag("Act3/Optimist/22_Proof");    


            //Cynic: You do what you want. But I don’t want to help you if you’re not going to listen to me.
            context.DoNarrationAndSetFlag("Act3/Cynic/23_DoWhatYouWant");
             
            //*insincerely* Thanks for showing me your game. I’ll see you around.
            context.DoNarrationAndSetFlag("Act3/Cynic/24_SeeYouAround");

            //Oppy: *disappointed* Ugh, good riddance. Never meet your heroes, right?
            context.DoNarrationAndSetFlag("Act3/Optimist/25_GoodRiddance");    

            //*silence*
            yield return new WaitForSeconds(3);
            //But maybe he was right. He’s got tons of time in the industry… maybe I should try out his advice.
            context.DoNarrationAndSetFlag("Act3/Optimist/26_MaybeRight");    


            //The game becomes regular pool except with different physics on all the balls.
            context.GetComponent<EndOfGame>().EndOfGameify();

            //Oppy: It’s something I came up with when we were talking. All the balls have slightly different interactions with everything.
            context.DoNarrationAndSetFlag("Act3/Optimist/27_Final1");    

            //Oppy: Yeah, I like how this plays. But which one should be worth the most? What if we…
            context.DoNarrationAndSetFlag("Act3/Optimist/28_Final2");    


 
            
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

