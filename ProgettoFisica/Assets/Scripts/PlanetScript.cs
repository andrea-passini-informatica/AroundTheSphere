using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetScript : MonoBehaviour {

    public void Attract(Transform playerTransform)
    {
        Rigidbody rbPlayer = playerTransform.GetComponent<Rigidbody>();

        //Trovo il vettore che va dal centro del pianeta al corpo
        // directionToFace = destintion - source
        Vector3 gravityUp = (playerTransform.position - transform.position).normalized;

        //Vettore Y del Player
        Vector3 localUp = playerTransform.up;

        //Applico la forza di attrazione al corpo
        rbPlayer.AddForce(gravityUp * - Gravity(rbPlayer.mass));

        //Ruoto il player in modo che:
        //      - localUp (il suo asse delle Y) segua la direzione di gravityUp
        Quaternion targetRotation = Quaternion.FromToRotation(localUp, gravityUp) * playerTransform.rotation;
        playerTransform.rotation = Quaternion.Slerp(playerTransform.rotation, targetRotation, 50f * Time.deltaTime);
    }

    //Calcolo la forza di Gravità come se si fosse sulla terra
    public static float Gravity(float mass)
	{
        return mass * 9.8f;
	}
}
