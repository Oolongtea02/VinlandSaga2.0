using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D col){
        GameObject Light = GameObject.Find("Light");
        Light.GetComponent<LightScript>().activate();
        GameObject Player = GameObject.Find("Player");
        Player.GetComponent<PlayerController>().hasWeapon = true;
        Destroy(gameObject);
    }
}
