using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header ("Player Attributes")]
    [SerializeField]
    private CharacterController mainCharacterController;
    [SerializeField]
    private Animator mainAnimator;
    public float mainWalkSpeed;
    public float mainRotationSpeed;

    private bool isHealing = false;
    private bool isAttacking = false;
    private bool isDead = false;
    private bool isDef = false;
    private bool isHit = false;
    public bool mFollowCameraForward = false;

    //multiplayer complient changes
    private PhotonView mPhotonView;

    void Start()
    {
        mPhotonView = gameObject.GetComponent<PhotonView>();
    }

    void Update()
    {
        if (mPhotonView == null)
        {
            GenInputs();
            return;
        }
        else
        {
            // Chnages made to port this script to be multiplayer compliant.
            if (!mPhotonView.IsMine) return;
            GenInputs();
        }
    }

    void GenInputs()
    {
        if (!isHealing && !isAttacking && !isDead && !isDef && !isHit)
        {
            Move();
        }
        HandleInputs();
    }


    void HandleInputs()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(!isDead)
            Jump();
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Attack1();
        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            StopAttack1();
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            Attack2();
        }
        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            StopAttack2();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(Heal());
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            if(!isDead)
            Die();
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            if(isDead)
            StartCoroutine(Recover());
        }
        if (Input.GetKeyDown(KeyCode.Mouse2))
        {
            Defend();
        }
        if (Input.GetKeyUp(KeyCode.Mouse2))
        {
            StopDefending();
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            Hit();
        }
        if (Input.GetKeyUp(KeyCode.I))
        {
            StopHit();
        }
    }

    private void Move()
    {
        float hInput = Input.GetAxis("Horizontal");
        float vInput = Input.GetAxis("Vertical");

        float speed = mainWalkSpeed;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = mainWalkSpeed * 2.0f;
        }

        if (mainAnimator == null) return;

        transform.Rotate(0.0f, hInput * mainRotationSpeed * Time.deltaTime, 0.0f);

        Vector3 forward =
            transform.TransformDirection(Vector3.forward).normalized;
        forward.y = 0.0f;

        mainCharacterController.Move(forward * vInput * speed * Time.deltaTime);


        mainAnimator.SetFloat("Horizontal", 0);
        mainAnimator.SetFloat("BacknForth", vInput * speed / 2.0f * mainWalkSpeed);
    }
    //Animator Parameters
    private void Jump()
    {
        mainAnimator.SetTrigger("Jump");
    }
    private void Attack1()
    {
        mainAnimator.SetBool("Attack1", true);
        isAttacking = true;
    }
    private void Attack2()
    {
        mainAnimator.SetBool("Attack2", true);
        isAttacking = true;
    }
    private void StopAttack1()
    {
        mainAnimator.SetBool("Attack1", false);
        isAttacking = false;
    }
    private void StopAttack2()
    {
        mainAnimator.SetBool("Attack2", false);
        isAttacking = false;
    }
    private void Die()
    {
        mainAnimator.SetTrigger("Die");
        isDead = true;
    }
    private void Defend()
    {
        mainAnimator.SetBool("Defend", true);
        isDef = true;
    }
    private void StopDefending()
    {
        mainAnimator.SetBool("Defend", false);
        isDef = false;
    }
    private void Hit()
    {
        mainAnimator.SetBool("Hit",true);
        isHit = true;
    }
    private void StopHit()
    {
        mainAnimator.SetBool("Hit", false);
        isHit = false;
    }
    IEnumerator Recover()
    {
        mainAnimator.SetTrigger("Recover");
        yield return new WaitForSeconds(1.15f);
        isDead = false;
    }
    IEnumerator Heal()
    {
        isHealing = true;
        mainAnimator.SetTrigger("Heal");
        yield return new WaitForSeconds(2.4f);
        isHealing = false;
    }
}
