using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoleBehaviour : MonoBehaviour
{
    void Start()
    {
        levelState = GameObject.FindGameObjectWithTag("GameController").GetComponent<LevelState>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ball")
        {
            Debug.Log("Ball");
            Destroy(other.gameObject);
            levelState.score();
        }
    }

    private LevelState levelState;
}
