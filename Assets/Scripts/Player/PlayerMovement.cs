using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

  private Rigidbody rigidbody;
  private string moveXAxis = "Horizontal";
  private string moveYAxis = "Vertical";
  public float speed = 10f;
  public Animator animator;
  private void Start() {
    rigidbody = GetComponent<Rigidbody>();
    rigidbody.centerOfMass = Vector3.zero;
    rigidbody.inertiaTensorRotation = Quaternion.identity;
  }

  private void FixedUpdate() {
    float inputX = Input.GetAxisRaw(moveXAxis);
    float inputY = Input.GetAxisRaw(moveYAxis);

    RunForces(inputX, inputY);
  }

  void RunForces (float inputX, float inputY) {
    MoveX(inputX);
    MoveY(inputY);

    if (Mathf.Abs(inputX) != 0 || Mathf.Abs(inputY) != 0)  animator.SetFloat("Speed", 1);    
    else  animator.SetFloat("Speed", 0);

  } 

  void MoveX(float input) {
    rigidbody.AddForce(new Vector3(1, 0, 0) * speed * input, ForceMode.Force);
    if(transform.eulerAngles.y != 180 && input < 0) transform.eulerAngles = new Vector3( transform.eulerAngles.x, 180, transform.eulerAngles.z);
    if(transform.eulerAngles.y != 0 && input > 0) transform.eulerAngles = new Vector3( transform.eulerAngles.x, 0, transform.eulerAngles.z);

  }

  void MoveY(float input) {
    rigidbody.AddForce(new Vector3(0, 1, 0) * speed * input, ForceMode.Force);
  }
}