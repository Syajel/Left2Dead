using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.FirstPerson;

public class InfectedController : MonoBehaviour
{
    [HideInInspector] static int SpecialInfectedKilled;
    [HideInInspector] public static bool incraseCompanionClip = false;
    public Infected infected;
    FirstPersonController playerController;
    Animator playerAnimator;
    [HideInInspector]
    public Transform target;
    NavMeshAgent agent;
    Animator animator;
    [HideInInspector]
    public FieldOfView fieldOfView;
    int health;
    int attackSpeed;
    [HideInInspector]
    public int attackDamage;
    [HideInInspector]

    public string type;
    GameObject player;
    private float attackCountdown;
    float distance;
    private JumpCurve jumpCurve;
    public GameObject Goop;
    Vector3 playerPos;
    private bool stunned;
    [HideInInspector]
    public bool confused;
    private bool attracted;
    JoelMeters joelMeter;
    private Vector3 chargePos;
    private bool chargeDone = true;
    private bool isDead = false;
    private float timeSinceStun;
    private bool stunning;
    private Vector3 spawnPoint;
    joelAnimator joelAnimator;
    AudioManager audioManager;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        fieldOfView = GetComponent<FieldOfView>();
        health = infected.health;
        attackSpeed = infected.attackSpeed;
        attackDamage = infected.attackDamage;
        type = infected.type;
        player = PlayerManager.instance.player;
        jumpCurve = GetComponent<JumpCurve>();
        playerPos = player.GetComponent<Transform>().position;
        playerController = player.GetComponent<FirstPersonController>();
        playerAnimator = player.GetComponent<Animator>();
        joelAnimator = player.GetComponentInChildren<joelAnimator>();
        joelMeter = player.GetComponent<JoelMeters>();
        timeSinceStun = 0;
        stunning = false;
        spawnPoint = transform.position;
        audioManager = FindObjectOfType<AudioManager>();

    }

    void Update()
    {
        if (playerController.isStunned() && stunning == true)
        {
            if ((type == "Jockey" || type == "Hunter") && timeSinceStun >= 5)
            {
                releasePlayer();
            }
            timeSinceStun += Time.deltaTime;
        }
        attackCountdown -= Time.deltaTime;


        if(fieldOfView.detected && !stunned && !attracted && !isDead){ 
            if(fieldOfView.visibleTargets.Count>0 && confused && chargeDone)
                target = fieldOfView.visibleTargets[0];
            else if(chargeDone)
                target = player.transform;
            distance = Vector3.Distance(target.position, transform.position);
            Attack();
        }
        else
        {
            if (transform.position.x < spawnPoint.x + 10)
                agent.SetDestination(new Vector3(transform.position.x + 50, transform.position.y, transform.position.z + 10));
            
            else
                agent.SetDestination(new Vector3(transform.position.x - 50, transform.position.y, transform.position.z - 10));
            animator.SetBool("run",true);
        }

        // Cheats
        if(Input.GetKeyDown("5")){
            TakeDamage(1200);
        }
        if(Input.GetKeyDown("6")){
            TakeDamage(10);
        }


    }

    public void TakeDamage (int amount)
    {
        health -= amount;
        fieldOfView.detected = true;
        if(!confused)
            target = PlayerManager.instance.player.transform;
        else
            target = fieldOfView.visibleTargets[0];
        if(health <= 0f){
            audioManager.play("zombieDying");
            isDead = true; 
            agent.isStopped = true;
            animator.Play("Die");
        }
        if (type == "Hunter")
        {

            releasePlayer();
        }
    }

    void Attack(){
        if(type == "Tank" || type == "Normal"){
            TankAttack();
        }
        else if(type == "Charger"){
            ChargerAttack();
        }

        else if(type == "Spitter"){
            SpitterAttack();
        }
        else if (type == "Jockey")
        {
            JockeyAttack();
        }
        else if (type == "Hunter")
        {
            HunterAttack();
        }
    }


    void JockeyAttack()
    {
        seek();
    }

    void HunterAttack()
    {
        seek();
    }


    void TankAttack(){
        agent.SetDestination(target.position);
            if(distance <= agent.stoppingDistance){
                FaceTarget();
                animator.SetBool("run", false);
                if(attackCountdown <= 0f){
                    // Attack
                    animator.SetBool("attack", true);
                    if(confused){
                        // Attack infected
                        if(target.GetComponent<InfectedController>() != null)
                            target.GetComponent<InfectedController>().TakeDamage(attackDamage);
                    }
                    else{
                        joelMeter.takeDamage(attackDamage);
                        joelAnimator.injured();
                    }

                    attackCountdown = attackSpeed;
                }
                else{
                    animator.SetBool("attack", false);
                } 
            }
            else{
                // Follow player.
                animator.SetBool("run", true);
            }
    }
    
    void SpitterAttack(){
        FaceTarget();
        if(fieldOfView.visibleTargets.Count == 0){
            animator.SetBool("attack", false); 
            animator.SetBool("run",true);
            agent.isStopped = false;
            agent.SetDestination(target.position);
            return;
        }
        else{
            animator.SetBool("run",false);
            agent.isStopped = true;

            if (attackCountdown <= 0f){
                animator.SetBool("attack", true);
                GameObject tempGoop = Instantiate(Goop, transform.position+transform.up*fieldOfView.lineOfSightFactor, Quaternion.identity);
                tempGoop.GetComponent<GoopTransform>().infectedTransform = transform.position+transform.up*fieldOfView.lineOfSightFactor;
                tempGoop.GetComponent<GoopTransform>().target = target;
                tempGoop.GetComponent<GoopTransform>().confused = confused;
                tempGoop.GetComponent<GoopTransform>().Shoot();

                attackCountdown = attackSpeed;
            }
        }
    }

    // Called in as an event in the death animation.
    void Die(){
        if(gameObject.CompareTag("SpecialInfected")){
            SpecialInfectedKilled++;
            if(SpecialInfectedKilled % 10 == 0)
                incraseCompanionClip = true;
        }
        
        Destroy(this.gameObject);
        joelMeter.resetRageLimit();
    }

    // Point towards the player.
	void FaceTarget(){
		Vector3 direction = (target.position - transform.position).normalized;
		Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
		transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
	}

    public void Stun(){
        StartCoroutine(StunCoroutine());
    }

    public void Attract(Transform t){
        StartCoroutine(AttractCoroutine(t));
    }

    IEnumerator AttractCoroutine(Transform t){
        attracted = true;
        agent.SetDestination(t.position);
        yield return new WaitForSeconds(3f);
        attracted = false;
        agent.SetDestination(target.position);
    }

    //Stun the infected
    IEnumerator StunCoroutine(){
        stunned = true;
        animator.SetBool("run", false);
        animator.SetBool("attack", false);
        agent.isStopped = true;
        yield return new WaitForSeconds(3f);
        stunned = false;
        agent.isStopped = false;
    }

    public float GetHealth(){
        return health;
    }

    void ChargerAttack(){
        FaceTarget();
        agent.SetDestination(target.position);
        // Out fieldOfView
        if(fieldOfView.visibleTargets.Count == 0){
             
            animator.SetBool("run",true);
            agent.isStopped = false;
            return;
        }
        // In of fieldOfView
        else{
            animator.SetBool("run",false);

            // Attack!
            if (attackCountdown < 0f){
                agent.isStopped = false;
                if(chargeDone){
                    agent.speed = 15;
                    chargePos = target.position;
                    chargeDone = false;
                    agent.SetDestination(chargePos);
                }
                animator.SetBool("attack", true);
            }
            // Stop when target is reached
            if(distance <= agent.stoppingDistance){
                agent.speed = 7;
                attackCountdown = attackSpeed;
                chargeDone = true;
                joelMeter.takeDamage(attackDamage);
                joelAnimator.injured();
                agent.isStopped = true;
                animator.SetBool("attack", false);
            }
        }
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("jump") && (type=="Jockey" || type == "Hunter") && collision.gameObject.name =="PlayerController" )
        {
            latchOnPlayer();
        }
    }

    void seek()
    {
        FaceTarget();
        if (fieldOfView.visibleTargets.Count == 0)
        {
            animator.SetBool("run", true);
            agent.SetDestination(playerPos);
        }
        else
        {
            animator.SetBool("run", false);
                if (attackCountdown <= 0f)
                {
                    if (!playerController.isStunned()){
                        animator.Play("jump");
                        attackCountdown = attackSpeed;
                    }
                    
                }
        }
    }


    void releasePlayer()
    {
        playerController.unstun();
        stunning = false;
        playerAnimator.SetBool("fall", false);
        if (type != "Charger")
        {
            animator.SetBool("attack", false);
        }
    }

    void latchOnPlayer()
    {
        playerController.stun();
        stunning = true;
        timeSinceStun = 0;
        if (type == "Jockey")
        {
            playerAnimator.SetBool("walk",true);
        }
        else if (type == "Hunter")
        {
            playerAnimator.SetBool("fall", true);
        }
    }


    void jumpTranslate()
    {
        StartCoroutine(jumpRoutine());
    }


    IEnumerator jumpRoutine()
    {
        jumpCurve.DrawBezierCurve();
        int i = 0;
        foreach (Vector3 pos in jumpCurve.positions)
        {
            if (i < 15)
                i++;
            else
                i--;
            yield return new WaitForSeconds(0.01f);
            transform.position =new Vector3(pos.x,transform.position.y+(0.01f*i),pos.z);
        }
    }
}
