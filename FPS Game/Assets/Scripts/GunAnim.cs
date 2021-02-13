using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunAnim : MonoBehaviour
{
    private Animator anim;
 
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
       
    }

    // Update is called once per frame

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
           
            anim.SetBool("IsFire", true);
            
        }
    }
    void FixedUpdate()
    {
        AnimatorStateInfo info = anim.GetCurrentAnimatorStateInfo(0);

        if (info.IsName("gun_shoot")) anim.SetBool("IsFire", false);
    }
}
