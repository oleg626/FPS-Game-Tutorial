using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticGun : SingleShotGun
{
    // Start is called before the first frame update
    [SerializeField] float shoot_speed;
    private float last_shoot = 0;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Use()
    {
        if (Time.time - last_shoot > shoot_speed)
        {
            Shoot();
            last_shoot = Time.time;
        }
    }

    public override void Release()
    {

    }


}
