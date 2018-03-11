using System;
using UnityEngine;
using XboxCtrlrInput;

public class ShipMovement : MonoBehaviour {
    [SerializeField] XboxController controller;
    private LevelParameters levelParameters;

    private float moveRate = 25f;
    private float turnRate = 80f;

    private float maxVertAngle = 40f;
    private float maxHorzAngle = 60f;

    void Start() {
        levelParameters = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelParameters>();
    }

    void Update() {
        // Movement
        float moveSpeed = Time.deltaTime * moveRate;

        if (levelParameters.onRails) {
            Vector3 forward = transform.forward * moveSpeed;

            // Clamping movement to the rails window. Achieved by altering the forward vector
            if(forward.x > 0 && transform.position.x > levelParameters.GetMaxX()) {
                forward.x = 0;
            }
            if(forward.x < 0 && transform.position.x < levelParameters.GetMinX()) {
                forward.x = 0;
            }
            if(forward.y > 0 && transform.position.y > levelParameters.GetMaxY()) {
                forward.y = 0;
            }
            if(forward.y < 0 && transform.position.y < levelParameters.GetMinY()) {
                forward.y = 0;
            }
            transform.Translate(forward,Space.World);
        } else {
            transform.Translate(Vector3.forward * moveSpeed);
        }


        //Rotation
        float turnSpeed = Time.deltaTime * turnRate;

        // We turn towards this angle, setting the x and y for vertical and horizontal respectively
        Vector3 targetVector3 = new Vector3(0,0,0);
        if (levelParameters.onRails) {
            targetVector3.y = InputMapping.TurnHorizontalAxis(controller) * maxHorzAngle;

        } else {
            transform.Rotate(Vector3.up * InputMapping.TurnHorizontalAxis(controller) * turnSpeed);
            targetVector3.y = transform.eulerAngles.y;
        }

        //Turning Up/Down
        float vertInput = InputMapping.TurnVerticalAxis(controller);
        if(Mathf.Abs(vertInput) > 0) {
            targetVector3.x = InputMapping.TurnVerticalAxis(controller) * maxVertAngle * -1;
        } 
        Quaternion targetAngle = Quaternion.Euler(targetVector3);
        transform.rotation = Quaternion.RotateTowards(transform.rotation,targetAngle,turnSpeed);

        // Lock on Z axis to remove odd rotations
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0);
    }
}