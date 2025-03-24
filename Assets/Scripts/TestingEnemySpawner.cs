using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingEnemySpawner : MonoBehaviour
{
    public GameObject enemy;
    GameObject newEnemy;
    Vector3 zero;

    void Start() {
        zero = transform.position;
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.T)) {
            newEnemy = Instantiate(enemy, zero, Quaternion.identity);
            newEnemy.SetActive(true);
        }
    }
}
