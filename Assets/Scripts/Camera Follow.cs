using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject player;
    public float xMin, xMax, yMin, yMax;

    void LateUpdate()
    {
        float x = Mathf.Clamp(player.transform.position.x, xMin, xMax);
        float y = Mathf.Clamp(player.transform.position.y, yMin, yMax);
        gameObject.transform.position = new Vector3(x, y, gameObject.transform.position.z);
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject == player) {
            player.GetComponent<NewInput>().inBounds = true;
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.gameObject == player) {
            player.GetComponent<NewInput>().inBounds = false;

        }
    }
}
