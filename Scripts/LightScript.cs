using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightScript : MonoBehaviour
{
    // Start is called before the first frame update
    Light theLight;
    void Start()
    {
        theLight = gameObject.GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void activate(){
        IEnumerator reduce(){
            while(theLight.intensity > 0){
                theLight.intensity = (float)(theLight.intensity - 0.005);
                yield return new WaitForSeconds(0.01f);
            }
        }
        StartCoroutine(reduce());
    }
    
}
