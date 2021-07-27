using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGravityBody : MonoBehaviour {

    Rigidbody _rigidbody;

    //SerializeField per farlo comparire in Unity lasciandolo private
    [SerializeField] PlanetScript attractorPlanet;

    Transform playerTransform;

	void Awake()
	{
        _rigidbody = GetComponent<Rigidbody>();
        playerTransform = transform;
    }

	void Start()
    {
        _rigidbody.useGravity = false;
        _rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
    }

    void FixedUpdate()
    {
        if (attractorPlanet)
        {
            attractorPlanet.Attract(playerTransform);
        }
    }
}
