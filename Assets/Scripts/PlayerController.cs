using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRB;
    public float gravityModifier;
    public float jumpForce;
    public bool gameOver;
    private Animator animationController;
    public ParticleSystem explosionParticles;
    public ParticleSystem dirtParticles;
    public AudioClip jumpSound;
    public AudioClip crashSound;
    private AudioSource audioSource;
    private int jumpCounter;
    private PropMovement background;
    public float skillDurationTime;
    public float skillCooldown;
    private float defaultAnimationSpeed;
    private float defaultBackgroudSpeed;
    public bool skillReady;
    public GameObject[] props;
    private Vector3 startVector;
    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;
        gameOver = false;
        jumpCounter = 2;
        skillDurationTime = 3;
        skillCooldown = 0;
        skillReady = true;
        animationController = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        background = GameObject.Find("Background").GetComponent<PropMovement>();
        defaultBackgroudSpeed = background.speed;
        defaultAnimationSpeed = animationController.GetFloat("Speed_Multiplier");
        startVector = new Vector3(-6, 0, 0);
        transform.position = startVector;
        StartCoroutine(StartAnimationLerp());
    }

    // Update is called once per frame
    void Update()
    {
        props = GameObject.FindGameObjectsWithTag("Obstacle");
        if (Input.GetKeyDown(KeyCode.Space) && jumpCounter > 0 && !gameOver)
        {
            animationController.SetTrigger("Jump_trig");
            audioSource.PlayOneShot(jumpSound, 0.5f);
            playerRB.AddForce(jumpForce * Vector3.up, ForceMode.Impulse);
            if (dirtParticles.isPlaying)
            {
                dirtParticles.Stop();
            }
            jumpCounter -= 1;
        }

        if (Input.GetKeyDown(KeyCode.C) && skillCooldown <= 0 && skillReady)
        {
            background.speed = 10;
            animationController.SetFloat("Speed_Multiplier", 2.0f);
            skillReady = false;
            skillCooldown = 8;
        }
        
        if (!skillReady && skillDurationTime > 0)
        {
            skillDurationTime -= Time.deltaTime;
            foreach (var item in props)
            {
                item.GetComponent<PropMovement>().speed = 8;
            }
        }

        if(skillDurationTime <= 0)
        {
            background.speed = defaultBackgroudSpeed;
            animationController.SetFloat("Speed_Multiplier", defaultAnimationSpeed);
            foreach (var item in props)
            {
                item.GetComponent<PropMovement>().speed = 5;
            }
        }
        
        if (skillCooldown > 0)
        {
            skillCooldown -= Time.deltaTime;
        }
        else if (skillCooldown <= 0 && !skillReady)
        {
            skillReady = true;
            skillDurationTime = 3;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            dirtParticles.Play();
            jumpCounter = 2;
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            gameOver = true;
            Debug.Log("Game Over!");
            animationController.SetBool("Death_b", true);
            animationController.SetInteger("DeathType_int", 1);
            explosionParticles.Play();
            audioSource.PlayOneShot(crashSound, 0.5f);
            dirtParticles.Stop();
        }
    }

    IEnumerator StartAnimationLerp()
    {
        float timer = 0;
        while (timer < 1)
        {
            transform.position = Vector3.Lerp(startVector, Vector3.zero, timer);
            timer += Time.deltaTime;
            yield return null;
        }
        transform.position = Vector3.zero;
        animationController.SetFloat("Speed_f", 1.0f);
        
    }
}
