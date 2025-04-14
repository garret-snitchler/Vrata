using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class MovePlayer : MonoBehaviour
{
    // SteamVR action connected to joystick
    public SteamVR_Action_Vector2 moveValue;
    // set in inspector
    public float maxSpeed;
    // used as multiplier to indicate how much control joystick has on speed, set in inspector
    public float sensitivity;
    // distance we stop player at in case of collision
    public float distance;
    // rigidbody used to determine when player close to collider
    public Rigidbody head;

    // current speed
    private float speed = 0.0f;

    // Update is called once per frame
    void Update()
    {
        // this would be used to stop the player from walking into things, needs tweaks to account for more directions than forwards
        RaycastHit hit;
        if (!(head.SweepTest(Player.instance.hmdTransform.TransformDirection(Vector3.forward), out hit, distance)))
        {
            // get direction HMD is facing
            Vector3 direction = Player.instance.hmdTransform.TransformDirection(new Vector3(moveValue.axis.x, 0, moveValue.axis.y));
            // joystick value * sensitivity value
            speed = moveValue.axis.magnitude * sensitivity;
            // keep speed in intended limits
            speed = Mathf.Clamp(speed, 0, maxSpeed);
            // move player along HORIZONTAL plane in direction specified earlier
            transform.position += speed * Time.deltaTime * Vector3.ProjectOnPlane(direction, Vector3.up);
        }
    }
}
