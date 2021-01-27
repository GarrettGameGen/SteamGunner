using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
   public CharacterController2D controller2D;
   
   float horizontalMove = 0f;
   bool jump = false;

   private void Update() 
   {
        horizontalMove = Input.GetAxisRaw("Horizontal");
        if(Input.GetButton("Jump")) 
        { 
           jump = true; 
        } else
        { 
            jump = false; 
        }
   }
   private void FixedUpdate() {
       controller2D.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
   }
}
