using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovingAndShooting: TutorialState{
	public string instructionText = "Move the character to the <color=#00AEFF>Squares</color> and attack the <color=#83FF00>Spheres</color>!";
	public string instructionTextAttack = "\n\n\nThis is the attack joystick.\nUse it to aim and shoot!";
	public string instructionTextMoving = "\n\n\nThis is the moving joystick.\nUse it to move Immunio!";

	public string finishingText = "GREAT! TOUCH THE ARROW TO MOVE TO THE NEXT TUTORIAL";
	public GameObject[] tutorialPlane = new GameObject[2];
	public GameObject[] tutorialTarget = new GameObject[2];

	public GameObject movingSprite;
	public GameObject attackSprite;
	public float cooldown = 1.5f;
	// public Vector3 spritePosition = new Vector3(130, 130, 0);

	public MovingAndShooting(TutorialManager tutorialManager) : base(tutorialManager){}

	public override void Update(){
		if(this.pressNumber == 1){
			TutorialManager.SetInstruction(instructionTextAttack);
			movingSprite.SetActive(false);
			attackSprite.SetActive(true);
		}
		if(this.pressNumber == 2){
			// Destroy(clone)
			attackSprite.SetActive(false);
			TutorialManager.SetInstruction(instructionText);
		}
		if(this.pressNumber == 3){
			// Destroy(clone)
			this.setOverlay(false);
			TutorialManager.tutorialTask.GetComponent<Text>().text = "Task:\n<size=30>-Destroy the spheres\n-Move to the squares</size>";
			StateMain();
		}

	}

	public override void StateStart(){
		TutorialManager.movingJoystick.SetActive(true);
		TutorialManager.attackJoystick.SetActive(true);

		TutorialManager.SetInstruction(instructionTextMoving);

		movingSprite = GameObject.Instantiate(TutorialManager.movingJoystick, TutorialManager.movingJoystick.transform.position, TutorialManager.movingJoystick.transform.rotation, TutorialManager.TutorialSprite.transform) as GameObject;
		movingSprite.GetComponent<Joystick>().enabled = false;

		attackSprite = GameObject.Instantiate(TutorialManager.attackJoystick, TutorialManager.attackJoystick.transform.position, TutorialManager.attackJoystick.transform.rotation, TutorialManager.TutorialSprite.transform) as GameObject;
		attackSprite.GetComponent<Joystick>().enabled = false;
		attackSprite.SetActive(false);

		// Instantiate planes
		tutorialPlane[0]  = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Tutorial/TutorialPlane"), this.player.transform.position + new Vector3(0, 0,5), this.player.transform.rotation) as GameObject;	
		tutorialPlane[1]  = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Tutorial/TutorialPlane"), this.player.transform.position + new Vector3(0, 0,-5), this.player.transform.rotation) as GameObject;	
		// tutorialPlane[2]  = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Tutorial/TutorialPlane"), this.player.transform.position + new Vector3(5, 0,0), this.player.transform.rotation) as GameObject;	
		// tutorialPlane[3]  = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Tutorial/TutorialPlane"), this.player.transform.position + new Vector3(-5, 0,0), this.player.transform.rotation) as GameObject;	
	
		tutorialTarget[0]  = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Tutorial/TutorialTarget"), this.player.transform.position + new Vector3(5, 0.5f,0), this.player.transform.rotation) as GameObject;	
		tutorialTarget[1]  = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Tutorial/TutorialTarget"), this.player.transform.position + new Vector3(-5, 0.5f,0), this.player.transform.rotation) as GameObject;	
		// tutorialTarget[2]  = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Tutorial/TutorialTarget"), this.player.transform.position + new Vector3(-5, 0.5f,5), this.player.transform.rotation) as GameObject;	
		// tutorialTarget[3]  = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Tutorial/TutorialTarget"), this.player.transform.position + new Vector3(-5, 0.5f,-5), this.player.transform.rotation) as GameObject;	


	}

	public void StateMain(){
		for(int i= 0; i < 2; i ++){
			if(tutorialPlane[i] != null) return;
		}
		for(int i= 0; i < 2; i ++){
			if(tutorialTarget[i] != null) return;
		}
		cooldown -= Time.deltaTime;
        if(cooldown <= 0){
           	StateEnd();
        }
		
	}

	public void StateEnd(){
		Time.timeScale = 0;
		this.setOverlay(true);
		TutorialManager.SetState(new EnemyIntro(TutorialManager));
	}

}