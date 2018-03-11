using System.Collections;
using System.Collections.Generic;
using System.Security.Principal;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    public GameObject player;
    public Camera camera;
    [HideInInspector] public float rotateSpeed;
    [SerializeField] private float defaultRotateSpeed;
    private LevelParameters levelParameters;
    public float cameraViewOffset;
    public Vector3 defaultPosition;
    public Vector2 windowSizeOffset;

    // Use this for initialization
    void Start() {
        if (player == null) {
            Debug.LogError("Camera can't find player");
            Application.Quit();
        }
         
        rotateSpeed = defaultRotateSpeed;
        levelParameters = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelParameters>();
        camera.transform.localPosition = defaultPosition;
    }

    // Update is called once per frame
    void Update() {
        if (levelParameters.onRails) {
            Vector3 targetPosition = FindTargetPosition(player.transform.position);

            transform.position = targetPosition;
            Vector3 lookAt = player.transform.position + (cameraViewOffset * Vector3.forward);
            camera.transform.LookAt(lookAt);
        } else {
            transform.position = player.transform.position;
            float step = rotateSpeed * Time.deltaTime;
            transform.rotation = Quaternion.Slerp(transform.rotation, player.transform.rotation, step);
            Vector3 lookAt = player.transform.position + (cameraViewOffset * player.transform.forward);
            camera.transform.LookAt(lookAt);
        }
    }

    private Vector3 FindTargetPosition(Vector3 position) {
        float maxX = levelParameters.GetMaxX() - windowSizeOffset.x;
        float minX = levelParameters.GetMinX() + windowSizeOffset.x;
        float maxY = levelParameters.GetMaxY() - windowSizeOffset.y;
        float minY = levelParameters.GetMinY() + windowSizeOffset.y;
        Debug.Log("MaxX: " + maxX + " minX: " + minX + " maxY: " + maxY + " minY: " + minY);
        if (position.x > maxX) {
            position.x = maxX;
        } else if (position.x < minX) {
            position.x = minX;
        }

        if (position.y > maxY) {
            position.y = maxY;
        } else if (position.y < minY) {
            position.y = minY;
        }

        position.x = levelParameters.center.x;
        return position;
    }
}