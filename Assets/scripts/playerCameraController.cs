using UnityEngine;
using System.Collections;

public class playerCameraController : MonoBehaviour {
    private Transform cameraTrans;
    public float rotateSpeed = 30F;
    private Vector3 cameraDirection = Vector3.zero;
    
    // Use this for initialization
    void Start ()
    {
        cameraTrans = transform.Find("PlayerCamera");
        transform.Find("Sphere").gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
		if (!Menu.showMenu) {
			cameraDirection = (new Vector3 (0, Input.GetAxis ("Mouse X"), 0)) * Time.deltaTime * rotateSpeed;
			transform.rotation *= Quaternion.Euler (cameraDirection);
			//face player towards camera
			cameraDirection = new Vector3 (-Input.GetAxis ("Mouse Y") * Time.deltaTime * rotateSpeed, 0, 0);
			//set camera rotation to current transform rotation
			cameraTrans.rotation *= Quaternion.Euler (cameraDirection);

			//keep player from flipping camera upside down
			if (cameraTrans.rotation.eulerAngles.x > 60 && cameraTrans.rotation.eulerAngles.x < 90) {
				cameraTrans.rotation = Quaternion.Euler (new Vector3 (60, cameraTrans.rotation.eulerAngles.y, 0));
			}
			if (cameraTrans.rotation.eulerAngles.x < 300 && cameraTrans.rotation.eulerAngles.x > 180) {
				cameraTrans.rotation = Quaternion.Euler (new Vector3 (300, cameraTrans.rotation.eulerAngles.y, 0));
			}
		}
    }
}
