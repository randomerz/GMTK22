using UnityEngine;

//Player input should go in here
//OOP stands for Out of Pocket, not Object Oriented Programming
public class OOPInput : Singleton<OOPInput>
{
    public class MouseDragEventArgs
    {
        public Vector3 startPos;
        public Vector3 endPos;
    }
    public static float horizontal;
    public static float vertical;

    //Subscribe to these events for mouse dragging!
    public static event System.EventHandler mouseDragStart;
    public static event System.EventHandler<MouseDragEventArgs> mouseDragOngoing;
    public static event System.EventHandler<MouseDragEventArgs> mouseDragEnd;

    private bool dragStarted;
    private Vector3 dragStart;

    private void Update()
    {
        //Synchronize inputs each frame.
        PollInput();
    }

    private void PollInput()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        UpdateDragEvents();
    }

    private void UpdateDragEvents()
    {
        if (Input.GetMouseButtonDown(0))
        {
            dragStart = Input.mousePosition;

        }

        if (Input.GetMouseButton(0))
        {
            if (Input.mousePosition != dragStart)
            {
                MouseDragEventArgs e = new MouseDragEventArgs
                {
                    startPos = dragStart,
                    endPos = Input.mousePosition
                };

                if (!dragStarted)
                {
                    dragStarted = true;
                    mouseDragStart?.Invoke(this, new System.EventArgs());
                }
                
                mouseDragOngoing?.Invoke(this, e);

            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (Input.mousePosition != dragStart)
            {
                mouseDragEnd?.Invoke(this, new MouseDragEventArgs
                {
                    startPos = dragStart,
                    endPos = Input.mousePosition
                });
            }
        }
    }
}

