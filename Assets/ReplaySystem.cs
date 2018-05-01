using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class ReplaySystem : MonoBehaviour {

	private const int bufferFrames = 750;
	private MyKeyFrame[] keyFrames = new MyKeyFrame [bufferFrames];
	private Rigidbody rigidBody;
	private GameManager manager;
	private int frameStop = 0;

	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody> ();
		manager = GameObject.FindObjectOfType<GameManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (manager.recording) {

			Record ();
		} else {
			PlayBack ();
		}
		if(CrossPlatformInputManager.GetButton("Fire1") ){
			frameStop = Time.frameCount;
		}
	}

	void PlayBack () {

		rigidBody.isKinematic = true;
		print ("Frame stop " + frameStop);

		//if (Time.frameCount < bufferFrames) {
			while (Time.frameCount < frameStop) {
				int frame = Time.frameCount % bufferFrames; 
				transform.position = keyFrames [frame].position;
				transform.rotation = keyFrames [frame].rotation;
				print ("Reading frame " + frame);
				print ("Frame stop " + frameStop);
			}
		//}else{
		//	int frame = Time.frameCount % bufferFrames; 
		//	print ("Reading frame " + frame);
		//	transform.position = keyFrames [frame].position;
		//	transform.rotation = keyFrames [frame].rotation;
		//}

	}

	void Record ()
	{
		rigidBody.isKinematic = false;
		int frame = Time.frameCount % bufferFrames;
		float time = Time.time;
		print ("Writing frame " + frame);
		keyFrames [frame] = new MyKeyFrame (time, transform.position, transform.rotation);
	}




}

/// <summary>
/// A structure for storing time, rotation and position
/// </summary>
public struct MyKeyFrame {
	public float frameTime;
	public Vector3 position;
	public Quaternion rotation;

	public MyKeyFrame (float aTime, Vector3 aPostion, Quaternion aRotation) {
	frameTime = aTime;
	position = aPostion;
	rotation= aRotation;
	}
}