using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] Transform[] waypoints;
    [SerializeField] float startWaitTime;
    [SerializeField] bool isFacingUp = false;
    float waitTime;
    int waypointIndex;
    bool enableMovement;
    Animator EnemyAnimator;
    Rigidbody2D enemyRigidBody;
    Vector2 prevPos;

    [Header("Field of View Settings")]
    [SerializeField] GameObject enemyFieldOfViewPrefab;
    [SerializeField] float angleFieldOfView;
    [SerializeField] float viewDistance;
    [SerializeField] LayerMask raycastLayerMask;
    EnemyFOV enemyFieldOfView;
    Vector3 aimDirection;

    [Header("SFX")]
    [SerializeField] AudioClip spottedSFX;
    float SFXVolume;
    [SerializeField] int SFXTime;

    [Header("Player Settings")]
    [SerializeField] Player player;

    BoxCollider2D enemyCollider;
    bool playerFound = false;
    bool playerFoundOnceFlag = true;

    // Start is called before the first frame update
    void Start()
    {
        EnemyAnimator = gameObject.GetComponent<Animator>();
        enemyRigidBody = gameObject.GetComponent<Rigidbody2D>();
        enemyCollider = gameObject.GetComponent<BoxCollider2D>();

        enemyFieldOfView = Instantiate(enemyFieldOfViewPrefab, null).GetComponent<EnemyFOV>();
        enemyFieldOfView.SetFOV(angleFieldOfView);
        enemyFieldOfView.SetViewDistance(viewDistance);
        aimDirection = new Vector3();

        enableMovement = true;
        EnemyAnimator.SetBool("IsFacingUp", isFacingUp);
        waitTime = startWaitTime;
        waypointIndex = 0;

        Physics2D.queriesStartInColliders = false;
        Physics2D.queriesHitTriggers = false;

        SFXVolume = PlayerPrefsController.GetGameVolume();
    }

    private void Update()
    {
        Vector3 target = waypoints[waypointIndex].position;

        if (Vector3.Distance(target, GetPosition()) <= 0.1f)
        {
            if(isFacingUp)
            {
                aimDirection = Vector3.up;
            }
            else
            {
                aimDirection = Vector3.down;
            }
        }
        else
        {
            aimDirection = (target - GetPosition()).normalized;
        }

        //Debug.Log(aimDirection);

        enemyFieldOfView.SetAimDirection(aimDirection);
        enemyFieldOfView.SetOrigin(transform.position);

        FindPlayer();
        if (playerFound && playerFoundOnceFlag)
        {
            playerFoundOnceFlag = false;
            FoundPlayer();
        }
    }

    private void FixedUpdate()
    {
        if (enableMovement)
        {
            prevPos = transform.position;
            MoveToWaypoint();
            UpdateAnimator();
        }
    }

    private void MoveToWaypoint()
    {
        enemyRigidBody.position = Vector2.MoveTowards(GetPosition(), waypoints[waypointIndex].position, moveSpeed * Time.fixedDeltaTime);

        if (Vector2.Distance(transform.position, waypoints[waypointIndex].position) <= 0.1f)
        {
            if (waitTime <= 0)
            {
                if (waypointIndex >= waypoints.Length - 1)
                {
                    waypointIndex = 0;
                }
                else
                {
                    waypointIndex++;
                }
                waitTime = startWaitTime;
            }
            else
            {
                waitTime -= Time.fixedDeltaTime;
            }
        }
    }

    private void UpdateAnimator()
    {
        Vector2 nextVector = Vector2.MoveTowards(GetPosition(), waypoints[waypointIndex].position, moveSpeed * Time.fixedDeltaTime);
        float horizontal = nextVector.x - prevPos.x;
        float vertical = nextVector.y - prevPos.y;
        float speed = Vector2.Distance(nextVector, prevPos);

        EnemyAnimator.SetFloat("Horizontal", horizontal);
        EnemyAnimator.SetFloat("Vertical", vertical);
        EnemyAnimator.SetFloat("Speed", speed);
    }

    private void FindPlayer()
    {
        if (Vector3.Distance(GetPosition(), player.GetPosition()) <= viewDistance + 0.4f)
        {
            // Player is inside viewDistance
            Vector3 directionToPlayer = (player.GetPosition() - GetPosition()).normalized;
            if (Vector3.Angle(aimDirection, directionToPlayer) < (angleFieldOfView+10f) / 2f) // additional 9 due to size of player sprite compared to player.getposition()
            {
                // Player is inside angle of FOV
                RaycastHit2D hitInfo = Physics2D.Raycast(GetPosition(), directionToPlayer, viewDistance + 1f, raycastLayerMask);
                if (hitInfo.collider != null)
                {
                    // Hit something
                    if (hitInfo.collider.gameObject.GetComponent<Player>() != null)
                    {
                        // Hit Player
                        playerFound = true;
                    }
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.GetComponent<Player>();
        if(player != null)
        {
            playerFound = true;
        }
    }

    private void FoundPlayer()
    {
        AudioSource.PlayClipAtPoint(spottedSFX, Camera.main.transform.position, SFXVolume);
        enableMovement = false;
        EnemyAnimator.SetTrigger("PlayerSpotted");
        player.Surrender();
        enemyFieldOfView.Spotted();
        StartCoroutine(TransitionToLoseScreen());
    }

    IEnumerator TransitionToLoseScreen()
    {
        LevelManager levelManager = FindObjectOfType<LevelManager>();
        yield return new WaitForSeconds(SFXTime);
        levelManager.LoseScreen();
    }

    private Vector3 GetPosition()
    {
        return transform.position;
    }
}
