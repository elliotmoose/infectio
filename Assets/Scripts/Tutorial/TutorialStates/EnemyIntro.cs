﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyIntro: TutorialState{
	public string instructionText = "BEWARE OF THESE ENEMIES:\n";
	public string finishingText = "GREAT! TOUCH THE ARROW TO MOVE TO THE NEXT TUTORIAL";

	public static string e1Desciption = "Badteria\nBacteria enemy, shoots out nasty stuff.\nAntibioitcs Launcher works wonders against 'em.";
	public static string e2Desciption = "Bossteria\nThe boss.\nYou'll never want to see him. But when you do, look out for its nasty taser! ";
	public static string e3Desciption = "Vivi-rus\nThat flu virus.\n Antiviral Flamethrower will demolish this being.";

	public EnemyIntro(TutorialManager tutorialManager) : base(tutorialManager){}

	public GameObject[] enemySprite = new GameObject[3];
	public GameObject[] enemyDescription = new GameObject[3];
	public string[] enemyDescriptionText = new string[] {e1Desciption, e2Desciption, e3Desciption}; 

	public override void Update(){
		if(this.pressNumber == 1){
			TutorialManager.SetState(new Bars(TutorialManager));
		}
	}

	public override void StateStart(){

		TutorialManager.SetInstruction(instructionText); 

		enemySprite[0]  = GameObject.Instantiate(TutorialManager.spriteTemplate, TutorialManager.TutorialSprite.transform.position + new Vector3(-(Screen.width/3) - 30 ,-150,0) ,  TutorialManager.Overlay.transform.rotation, TutorialManager.TutorialSprite.transform) as GameObject;	
		enemySprite[1]  = GameObject.Instantiate(TutorialManager.spriteTemplate, TutorialManager.TutorialSprite.transform.position + new Vector3((Screen.width/3 - 30),-150,0),  TutorialManager.Overlay.transform.rotation, TutorialManager.TutorialSprite.transform) as GameObject;	
		enemySprite[2]  = GameObject.Instantiate(TutorialManager.spriteTemplate, TutorialManager.TutorialSprite.transform.position + new Vector3(0 - 30,-150,0),  TutorialManager.Overlay.transform.rotation, TutorialManager.TutorialSprite.transform) as GameObject;	

		for( int i = 0; i < 3; i++){
			Image image = enemySprite[i].GetComponent<Image>();
			if (i == 2){ //to scale the virus 
				image.gameObject.transform.localScale += new Vector3 (2f, 2f,0);
			}

			else if (i == 1){
				image.gameObject.transform.localScale += new Vector3 (4f, 4f,0);
			}

			else {
				image.gameObject.transform.localScale += new Vector3 (3f, 3f,0);
			}
			
			image.sprite = Resources.Load<Sprite>("Sprites/Tutorial/e" + i);

			enemyDescription[i] = GameObject.Instantiate(TutorialManager.InstructionTextBottom,  new Vector3(enemySprite[i].transform.position.x,TutorialManager.InstructionTextBottom.transform.position.y , 0 ),  enemySprite[0].transform.rotation, TutorialManager.TutorialSprite.transform) as GameObject;	
			enemyDescription[i].GetComponent<RectTransform>().sizeDelta =  new Vector2(290, 190);
			enemyDescription[i].GetComponent<Text>().text = enemyDescriptionText[i];
			enemyDescription[i].GetComponent<Text>().fontSize = 30;
		}
	}

}