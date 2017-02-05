using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    [SerializeField]
    private GameObject player;

    private Vector3 _offset;

    // Use this for initialization
    void Start() {
        _offset = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void Update() {
        transform.position = player.transform.position + _offset;
    }
}
