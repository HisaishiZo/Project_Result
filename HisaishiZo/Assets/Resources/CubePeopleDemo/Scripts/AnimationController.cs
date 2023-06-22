using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CubePeople
{
    public class AnimationController : MonoBehaviour
    {
        public Aimovemove aimove;
        Animator anim;
        public bool run;

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
