using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class PlayerBehaviour : MonoBehaviour
{
    [Header("Player Properties")]
    public Rigidbody2D rb;
    private Animator an;

    public float vel = 0.02f;
    public float coolDownSteps;
    public float timeBump;
    
    private Vector2 direction;

    //trocar os passos
    public bool first;
    public bool BumpCntr;
    public bool levantarCadeira = false;
    public bool alternado;
    public bool canMove;
    public bool tooMchIdle;

    //narrator calls
    [Header("Narrador Properties")]

    [SerializeField]float t;
    public float idleTimeTrigger;
    [Header("Checks")]
    public string currentTag;

    [Header("Interact Properties")]
    public AnimateTrigger animateTrigger;
    private ResolvePuzzles currentPuzzle;
    public AudioSource audioSource;
    public bool canInteract = false;

    /*
        0 - concreto
        1 - madeira
        2 - tecido
        3 - bump
    */

    [Header("Audio Settings")]
    [SerializeField]
    private AudioClip[] clips;
    bool controller = false;
    public bool callBumpFunt = false;

    private void Start()
    {
        an = GetComponent<Animator>();
    }
    private void Update()
    {
        Animations();
        if(!controller && direction != Vector2.zero)
        {
            controller = true;
            StartCoroutine(WalkCoroutine());
        }
        if (Input.GetKeyDown(KeyCode.J) && currentPuzzle.level.GetLastStepsState(currentPuzzle.currentStep))
            if(animateTrigger != null)
                Interact();

    }
    //SETANDO INTERAGIR COM OBJETOS
    private void SetCurrentAnimatorInteraction(GameObject target)
    {
        animateTrigger = target.GetComponent<AnimateTrigger>();
    }
    private void SetCurrentArea(GameObject area)
    {
        if(area != null)
            currentPuzzle = area.GetComponent<ResolvePuzzles>();
    }
    private void Interact()
    {
        if (animateTrigger.inInteractArea)
        {
            StartCoroutine(WaitForAnimationDuration(animateTrigger.duration));
            levantarCadeira = true;
            animateTrigger.SetInteract(true);
            transform.localScale = new Vector3(.7f,.7f,.7f);
        }
    }
    //---
    IEnumerator WaitForAnimationDuration(float duration)
    {
        canMove = true;
        yield return new WaitForSeconds(duration);
        canMove = false;
    }
    void FixedUpdate()
    {
        //controle para chamar a coroutine apenas uma vez por tempo
        if (!canMove)
            Movement();
        else
        {
            rb.velocity = Vector2.zero;
        }
        StatesToNarrator();

    }
    void StatesToNarrator()
    {
        if(direction == Vector2.zero)
        {
            t += Time.fixedDeltaTime;
        }
        if (t > idleTimeTrigger)
        {
            tooMchIdle = true;
            t = 0;
        }
    }
    void Movement()
    {
        direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        rb.velocity = vel * direction.normalized;   
    }
    //AUDIO SETUP
    void Walk(AudioClip clip)
    {
        if(clip != audioSource.clip)
            audioSource.clip = clip;
        
        audioSource.Play();
    }
    //SONS DE AUDIO 
    private IEnumerator WalkCoroutine()
    {
        t = 0;
        tooMchIdle = false;
        first = !first;

        if (BumpCntr)
        {
            audioSource.clip = clips[2];
            audioSource.Play();
        }
        else 
        { 
            Walk(first ? clips[0] : clips[1]);
        }     // PASSOS REPETIDOS POS [1 E 2]

        yield return new WaitForSeconds(coolDownSteps);

        controller = false;
    }
    //CREATE SCRIPTABLE OBJECT -- 
    public void Animations()
    {
        if (!levantarCadeira)
        {
            if (direction == Vector2.zero)
            {
                an.SetFloat("x", 0);
            }
            else if (direction.y != 0)
            {
                an.SetFloat("x", 1);
            }
            if (direction.x != 0)
            {
                an.SetFloat("x", -1);
                SetDirectionScale();
            }
        }
        else
        {
            an.Play("empurrarCadeira");
            an.SetBool("exit", true);
            levantarCadeira = false;
        }
    }
    //--
    private void SetDirectionScale()
    {
        transform.localScale = direction.x > 0 ? transform.localScale = new Vector3(-.7f, .7f, 1) : direction.x < 0 ? 
            new Vector3(.7f, .7f, 1) : transform.localScale;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        currentTag = collision.gameObject.tag;
        if (!BumpCntr && collision.transform.tag == "walls" || collision.transform.tag == "bump")
        {
            BumpCntr = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision) 
    {
        BumpCntr = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("interactible"))
            SetCurrentAnimatorInteraction(collision.gameObject);
        if (collision.CompareTag("rooms"))
            SetCurrentArea(collision.gameObject);
    }
}
