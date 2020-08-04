using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class SwitchCharacterScript : MonoBehaviour {
	
	[Header("Settings")]
	public GameObject avatar1;
	public GameObject avatar2;
	public Animator animatorNormal;
	public Animator animatorFight;
	public CinemachineVirtualCamera cam;
	
	private int _whichAvatarIsOn = 1;
	
	void Start () {
		var cam = GetComponent<CinemachineVirtualCamera>();
		avatar1.gameObject.SetActive (true);
		avatar2.gameObject.SetActive (false);
	}

	private void Update()
	{
		if(Input.GetKeyDown(KeyCode.M))
		{
			SwitchAvatar();
		}
	}

	private void SwitchAvatar()
	{
		Vector2 positionTemp;
		switch (_whichAvatarIsOn) {
			
		case 1:
			_whichAvatarIsOn = 2;
			
			avatar1.gameObject.SetActive (false);
			avatar2.gameObject.SetActive (true);
			
			
			positionTemp = new Vector2(avatar1.transform.position.x, avatar1.transform.position.y);
			avatar2.transform.position = positionTemp;
			cam.Follow = avatar2.transform;
			animatorFight.SetTrigger("Transform");
			break;
		
		case 2:
			_whichAvatarIsOn = 1;
			
			avatar1.gameObject.SetActive (true);
			avatar2.gameObject.SetActive (false);
			
			
			positionTemp = new Vector2(avatar2.transform.position.x, avatar2.transform.position.y);
			avatar1.transform.position = positionTemp;
			cam.Follow = avatar1.transform;
			animatorNormal.SetTrigger("Transform");
			break;
		
		}
			
	}
}
