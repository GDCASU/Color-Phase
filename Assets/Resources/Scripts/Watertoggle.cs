using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterToggle : MonoBehaviour
{

    public GameObject abovewater, belowwater, invertedsphere, simpleWater;
    public int waterQuality = 1;
    public int gravity = 0;

    private int mode = 0;
    private float waterHeight;


    // Use this for initialization
    void Start()
    {

        //RenderSettings.fog = true;
        //RenderSettings.fogDensity = 0.01f;

        //RenderSettings.fogColor = new Color(171f/255f, 190f / 255f, 214f / 255f); // Day Fog
        //RenderSettings.fogColor = new Color(17f / 255f, 19f / 255f, 21f / 255f); // Night Fog

        waterHeight = abovewater.transform.position.y;

        if (waterQuality == 1)
        {
            abovewater.SetActive(true);
            belowwater.SetActive(false);
            invertedsphere.SetActive(false);
            //simpleWater.SetActive(false);
        }
        else
        {
            //abovewater.SetActive(false);
            //belowwater.SetActive(false);
            //invertedsphere.SetActive(false);
            //simpleWater.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {


        if (transform.position.y < waterHeight)
        {
            GetComponent<Rigidbody>().velocity.Set(0, 2, 0);
            GetComponent<Rigidbody>().useGravity = false;
        }
        else
        {
            if (gravity == 1) GetComponent<Rigidbody>().useGravity = true;
            //GetComponent<Rigidbody>().setA
        }


        //transform.position.x
        if (waterQuality == 1)
        {
            if (transform.position.y < waterHeight && mode == 0)
            {
                abovewater.SetActive(false);
                belowwater.SetActive(true);
                invertedsphere.SetActive(true);
                mode = 1;
                //GetComponent<Rigidbody>().useGravity = false;

                //RenderSettings.fog = true;
                //RenderSettings.fogDensity = 0.1f;
                //RenderSettings.fogColor = new Color(43f / 255f, 52f / 255f, 63f / 255f);



                //GetComponent<Rigidbody>().velocity.Set(0, 2, 0);
            }
            if (transform.position.y >= waterHeight && mode == 1)
            {
                abovewater.SetActive(true);
                belowwater.SetActive(false);
                invertedsphere.SetActive(false);
                //if (gravity == 1) GetComponent<Rigidbody>().useGravity = true;

                //RenderSettings.fog = true;
                //RenderSettings.fogDensity = 0.01f;
                //RenderSettings.fogColor = new Color(171f/255f, 190f / 255f, 214f / 255f); // Day Fog
                //RenderSettings.fogColor = new Color(17f / 255f, 19f / 255f, 21f / 255f); // Night Fog

                //GetComponent<Rigidbody>().velocity.Set(0, 2, 0);
                mode = 0;
            }
        }
        else


        {
            /*
            if (transform.position.y < 24.14 && mode == 0)
            {
                //abovewater.SetActive(false);
                //belowwater.SetActive(true);
                invertedsphere.SetActive(true);
                GetComponent<Rigidbody>().useGravity = false;
                //GetComponent<Rigidbody>().velocity.Set(0, 2, 0);
                mode = 1;
            }
            if (transform.position.y > 24.14 && mode == 1)
            {
                //abovewater.SetActive(true);
                //belowwater.SetActive(false);
                invertedsphere.SetActive(false);
                if (gravity == 1) GetComponent<Rigidbody>().useGravity = true;
                //GetComponent<Rigidbody>().velocity.Set(0, 2, 0);
                mode = 0;
            }
            */
        }


    }
}
