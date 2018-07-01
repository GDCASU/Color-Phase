using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinMotion : MonoBehaviour {

	//public float RotationSpeed = 250;
	public float SinSpeed = 0.025f;
	public float SinMagnitude = 0.1f;
    public float offset = 0.0f;
    private Vector3 startPosition;
	public int Axis = 1;

	private float num = 0;

	// Use this for initialization
	void Start () {
        startPosition = transform.localPosition;
		//float xp = transform.position.x;
		//float yp = transform.position.y;
		//float zp = transform.position.z;
		//float old = 0f;
		//float fun = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		//transform.position = position + new Vector3 (0, Mathf.Sin((Time.deltaTime*SinSpeed* Mathf.PI)/180)*Height, 0);
		//count = count + 1;
		//transform.position = new Vector3(0,0,0);

		num = num + SinSpeed* Time.deltaTime;

		switch (Axis) {
		case 1:
                transform.localPosition = new Vector3(startPosition.x, startPosition.y + Mathf.Sin(num + offset) * SinMagnitude, startPosition.z);
			//transform.Rotate(Vector3.up, Time.deltaTime*RotationSpeed);
			//transform.Translate (Vector3.up * Time.deltaTime * Mathf.Sin (num+offset) * SinMagnitude);
                //transform.Rotate(Vector3.up, Time.deltaTime * RotationSpeed);
                // transform.Translate(Vector3.up * Time.deltaTime * Mathf.Sin(num) * SinMagnitude);
                break;
		case 2:
			//transform.Rotate(Vector3.right, Time.deltaTime*RotationSpeed);
			//transform.Translate (Vector3.right * Time.deltaTime * Mathf.Sin (num + offset) * SinMagnitude);
			break;
		case 3:
			//transform.Rotate(Vector3.forward, Time.deltaTime*RotationSpeed);
			//transform.Translate (Vector3.forward * Time.deltaTime * Mathf.Sin (num + offset) * SinMagnitude);
			break;
		}



		//this.transform.position.y = this.transform.position.y + 1; //Mathf.Sin ((Time.deltaTime*SinSpeed * Mathf.PI) / 180) * Height;

		//transform.position += Vector3.up * Mathf.Sin ((Time.deltaTime*SinSpeed * Mathf.PI) / 180) * Height;

		//fun = Mathf.Sin ((Time.deltaTime*SinSpeed * Mathf.PI) / 180) * Height;
		//transform.Translate(new Vector3(0,1,0) * (Mathf.Sin (Time.deltaTime*SinSpeed) * Height), Space.World);
		//transform.Translate(Vector3.up * (Mathf.Sin ((Time.deltaTime*SinSpeed * Mathf.PI) / 180) * Height), Space.World);
		//old = fun;
		//transform.localPosition = new Vector3(0, Mathf.Sin((Time.deltaTime*SinSpeed* Mathf.PI)/180)*Height, 0);
	}
}
