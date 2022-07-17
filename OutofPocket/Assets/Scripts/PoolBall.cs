using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Or Cube or whatever it may be.
public class PoolBall : MonoBehaviour
{
    public enum Shape
    {
        Sphere,
        Cube,
        Dice
    }

    [System.Serializable] 
    public class ShapeMesh
    {
        public Shape type;
        public GameObject gameObject;
    }

    public class BallHitEventArgs
    {
        public PoolBall ball;
        public GameObject hitBy;
    }
    public class BallEventArgs
    {
        public PoolBall ball;
        public GameObject pocket;
    }
    public bool sunk;

    [SerializeField] private Shape shapeAtStart;
    [SerializeField] private List<ShapeMesh> shapeMeshes;

    [Header("READ ONLY, DON'T NEED TO SET")]
    public Vector3 initialPos;

    private Dictionary<Shape, GameObject> shapeMeshesDict;

    public static event System.EventHandler<BallHitEventArgs> ballHitEvent;
    public static event System.EventHandler<BallEventArgs> ballInPocketEvent;

    private void Awake()
    {
        initialPos = transform.position;
        shapeMeshesDict = new Dictionary<Shape, GameObject>();
        shapeMeshes.ForEach(mesh =>
        {
            shapeMeshesDict[mesh.type] = mesh.gameObject;
        });

        ChangeShape(shapeAtStart);
    }

    private void OnEnable()
    {
        ballHitEvent += DefaultHitEventHandler;
        ballInPocketEvent += DefaultSunkEventHandler;
    }

    private void OnDisable()
    {
        ballHitEvent -= DefaultHitEventHandler;
        ballInPocketEvent -= DefaultSunkEventHandler;
    }

    private void Update()
    {
        //Failsafe in case a ball gets out of bounds and doesn't hit the pocket trigger.
        if (transform.position.y < -10)
        {
            sunk = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("PoolBall"))
        {
            ballHitEvent?.Invoke(this, new BallHitEventArgs
            {
                ball = this,
                hitBy = collision.gameObject
            }); ;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pocket"))
        {
            ballInPocketEvent?.Invoke(this, new BallEventArgs
            {
                ball = this,
                pocket = other.gameObject
            });
        }
    }

    public void ChangeShape(Shape shape)
    {
        shapeMeshes.ForEach(mesh =>
        {
            if (mesh.type == shape)
            {
                mesh.gameObject.SetActive(true);
            } else
            {
                mesh.gameObject.SetActive(false);
            }
        });
    }

    private void DefaultHitEventHandler(object sender, BallHitEventArgs e)
    {
        //Debug.Log($"{e.ball.gameObject.name} Hit by {e.hitBy.gameObject.name}");
    }

    private void DefaultSunkEventHandler(object sender, BallEventArgs e)
    {
        if (e.ball == this)
        {
            sunk = true;
        }
        //Debug.Log($"{e.ball.gameObject.name} Sunk!");
    }
}
