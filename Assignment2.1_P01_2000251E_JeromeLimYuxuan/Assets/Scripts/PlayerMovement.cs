using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
   [Header("Player Attributes")]
   [SerializeField]
   private CharacterController _mainCharacterController;
   [SerializeField]
   private Animator _mainAnimator;
   public float mainWalkSpeed;
   public float mainRotationSpeed;

   private bool _isHealing = false;
   private bool _isAttacking = false;
   private bool _isDead = false;
   private bool _isDefending = false;
   private bool _isHit = false;
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
      if (!_isHealing && !_isAttacking && !_isDead && !_isDefending && !_isHit)
      {
         Movement();
      }
      SpecialInputs();
   }


   void SpecialInputs()
   {
      if (Input.GetKeyDown(KeyCode.Space))
      {
         if (!_isDead)
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
         if (!_isDead)
            Die();
      }
      if (Input.GetKeyDown(KeyCode.O))
      {
         if (_isDead)
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

   private void Movement()
   {
      float hInput = Input.GetAxis("Horizontal");
      float vInput = Input.GetAxis("Vertical");

      float speed = mainWalkSpeed;

      if (Input.GetKey(KeyCode.LeftShift))
      {
         speed = mainWalkSpeed * 2.0f;
      }

      if (_mainAnimator == null) return;

      transform.Rotate(0.0f, hInput * mainRotationSpeed * Time.deltaTime, 0.0f);

      Vector3 forward =
          transform.TransformDirection(Vector3.forward).normalized;
      forward.y = 0.0f;

      _mainCharacterController.Move(forward * vInput * speed * Time.deltaTime);

      _mainAnimator.SetFloat("Horizontal", 0);
      _mainAnimator.SetFloat("BacknForth", vInput * speed / 2.0f * mainWalkSpeed);
   }

   //Animator Functions
   private void Jump()
   {
      _mainAnimator.SetTrigger("Jump");
   }
   private void Attack1()
   {
      _mainAnimator.SetBool("Attack1", true);
      _isAttacking = true;
   }
   private void Attack2()
   {
      _mainAnimator.SetBool("Attack2", true);
      _isAttacking = true;
   }
   private void StopAttack1()
   {
      _mainAnimator.SetBool("Attack1", false);
      _isAttacking = false;
   }
   private void StopAttack2()
   {
      _mainAnimator.SetBool("Attack2", false);
      _isAttacking = false;
   }
   private void Die()
   {
      _mainAnimator.SetTrigger("Die");
      _isDead = true;
   }
   private void Defend()
   {
      _mainAnimator.SetBool("Defend", true);
      _isDefending = true;
   }
   private void StopDefending()
   {
      _mainAnimator.SetBool("Defend", false);
      _isDefending = false;
   }
   private void Hit()
   {
      _mainAnimator.SetBool("Hit", true);
      _isHit = true;
   }
   private void StopHit()
   {
      _mainAnimator.SetBool("Hit", false);
      _isHit = false;
   }
   IEnumerator Recover()
   {
      _mainAnimator.SetTrigger("Recover");
      yield return new WaitForSeconds(1.15f);
      _isDead = false;
   }
   IEnumerator Heal()
   {
      _isHealing = true;
      _mainAnimator.SetTrigger("Heal");
      yield return new WaitForSeconds(2.4f);
      _isHealing = false;
   }
}
