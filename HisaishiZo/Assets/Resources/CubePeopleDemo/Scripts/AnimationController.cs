using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CubePeople
{
    public class AnimationController : MonoBehaviour
    {

        Animator anim;
        public bool run;
        public Aimovemove aimove;

        void Start()
        {
            anim = GetComponent<Animator>();
            if (run) run = false;
        }


        void Update()
        {

            if (aimove.isMove == false)
            {
                run = false;
            }
            else
            {
                run = true;
            }           
            anim.SetBool("Run", run);          
        }
    }
}
