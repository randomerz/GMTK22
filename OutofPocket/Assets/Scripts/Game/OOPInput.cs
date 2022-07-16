using UnityEngine;

//Player input should go in here
//OOP stands for Out of Pocket, not Object Oriented Programming
public class OOPInput : Singleton<OOPInput>
{
    public static float horizontal;
    public static float vertical;

    private void Update()
    {
        //Synchronize inputs each frame.
        PollInput();
    }

    private void PollInput()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
    }
}

