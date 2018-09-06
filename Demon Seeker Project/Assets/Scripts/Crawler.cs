using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crawler : MonoBehaviour
{
    [Tooltip("The Player game object")]
    [SerializeField] private GameObject player;

    [Tooltip("The speed of the crawler while 'walking'")]
    [SerializeField] private float walkSpeed = 1f;

    [Tooltip("The speed of the crawler while 'running'")]
    [SerializeField] private float runSpeed = 4f;

    [Tooltip("The distance the creature can detect the player from")]
    [SerializeField] private float detectionDistance = 15f;

    [Tooltip("How far the creature is from the player when it begins to 'run'")]
    [SerializeField] private float chaseDistance = 7f;

    [Tooltip("How fast the creature can rotate")]
    [SerializeField] private float lookRotation = 5f;

    private Animator crawlerAnimator;
    private float currentSpeed;

    private void Start()
    {
        crawlerAnimator = GetComponentInChildren<Animator>();
        currentSpeed = walkSpeed;
    }

    // Update is called once per frame
    void Update ()
    {
		if(Vector3.Distance(player.transform.position, transform.position) < detectionDistance && Vector3.Distance(player.transform.position, transform.position) > 1.5f)
        {
            crawlerAnimator.SetBool("isDetectingPlayer", true);

            Vector3 D = player.transform.position - transform.position;

            Quaternion rot = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(D), lookRotation * Time.deltaTime);

            transform.rotation = rot;

            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);

            transform.position += transform.forward * currentSpeed * Time.deltaTime;

            if (Vector3.Distance(player.transform.position, transform.position) < chaseDistance && Vector3.Distance(player.transform.position, transform.position) > 1.5f)
            {
                crawlerAnimator.SetBool("isChasing", true);
                currentSpeed = runSpeed;
            }
            else if (Vector3.Distance(player.transform.position, transform.position) > chaseDistance && Vector3.Distance(player.transform.position, transform.position) > 1.5f)
            {
                crawlerAnimator.SetBool("isChasing", false);
                currentSpeed = walkSpeed;
            }
            else if(Vector3.Distance(player.transform.position, transform.position) < 1.5f)
            {
                crawlerAnimator.SetBool("isChasing", false);
            }
        }
        else if(Vector3.Distance(player.transform.position, transform.position) < 1.5f)
        {
            crawlerAnimator.SetBool("isDetectingPlayer", false);
        }
        else if(Vector3.Distance(player.transform.position, transform.position) > detectionDistance)
        {
            crawlerAnimator.SetBool("isDetectingPlayer", false);
        }
	}
}
