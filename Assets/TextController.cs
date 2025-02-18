﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class TextController : MonoBehaviour {

// List of states. Each state is a textwall describing current in-game situation and presentig player his options.
	public enum States {
		intro, wolf, wolf_heal, wolf_calming, wolf_calm, wolf_inspect1, wolf_inspect2, wolf_leave, wolf_kill, transition_0, wolf_warning, wolf_fight, fight_bow, fight_magic,
		fight_calm, omen_0, omen_1, tbc, death
	};
	private States myState;
	
	public RawImage Background;		// Class used to adjust background. And background constants.
	public Texture forest3;
	public Texture forest2;

	public Text text;					// Text object to display current in-game state. Also, definition of stats variables.
	private int Health;
	private int Mana;
	private int Courage;
	
	public Text stat;					// Text object to display current stats. Also, string variables required for .ToString conversion of stat integers.
	private string hp_string;
	private string mn_string;
	private string cr_string;
	
	public Text Jebane_HP;
	private string ChHP_string;
	
	public Text Jebane_MP;
	private string ChMN_string;
	
	public Text Jebane_CR;
	private string ChCR_string;
	
	bool wolf_help;						// Bools used to store in-game decisions.
	bool wolf_calmed;
	bool wolf_killed;
	
	
	
	
//	To do:
//	1. Finish the fucking story
//	2. Make decisive keybinding more coherent (or make decisions as fucking UI buttons)
//	3. Check spelling & grammar. Or make someone do it for you.

	// Use this for initialization
	void Start () {
		wolf_help		=	false;
		wolf_calmed		=	false;
		wolf_killed		=	false;
	
		Health 			=	10;
		Mana			=	10;
		Courage			=	0;
			
		Background = GetComponent<RawImage>();
		Background.texture = forest3;
		
		UpdateState(States.intro, 0, 0, 0);
	}
	
	// Update is called once per frame
	void Update () {

		UpdateStat();
		
		print (myState);
		if 		(myState==States.intro) 			{intro();}
		else if (myState==States.wolf)				{wolf();}
		else if (myState==States.wolf_calm)			{wolf_calm();}
		else if (myState==States.wolf_calming)		{wolf_calming();}
		else if (myState==States.wolf_inspect1)		{wolf_inspect1();}
		else if (myState==States.wolf_inspect2)		{wolf_inspect2();}
		else if (myState==States.wolf_heal)			{wolf_heal();}
		else if (myState==States.wolf_kill)			{wolf_kill();}
		else if (myState==States.wolf_leave)		{wolf_leave();}
		else if (myState==States.transition_0)		{transition_0();}
		else if (myState==States.wolf_warning)		{wolf_warning();}
		else if (myState==States.wolf_fight)		{wolf_fight();}
		else if (myState==States.fight_bow)			{fight_bow();}
		else if (myState==States.fight_magic)		{fight_magic();}
		else if (myState==States.fight_calm)		{fight_calm();}
		else if (myState==States.omen_0)			{omen_0();}
		else if (myState==States.omen_1)			{omen_1();}
		else if (myState==States.tbc)				{tbc();}
		else if (myState==States.death)				{death();}

		
	}
	
//	void credits() {
//	
//	}
	
#region Prologue methods
	void intro() {
		Background = GetComponent<RawImage>();
		Background.texture = forest3;
	
		Health 	= 10;
		Mana	= 10;
		Courage	= 0;
		
		wolf_help		=	false;
		wolf_calmed		=	false;
		wolf_killed		=	false;
		
		
		text.text = "You are a druid of Sacred Forest, sworn guardian of nature. Only recently you have been accepted into the Outer Circle, where you are " +
					"supposed to slowly learn secret druidic knowledge and gain wisdom of the Elders. Though only an apprentice, you already have learned enough " +
					"to know how to call upon powers of nature and had access to very basic druidic magic. Before all that you were a hunter, so you also know " +
					"your way around with a bow, though your skills may be a bit rusty after few years of neglect.\n\n" +
					"Right now you are on patrol, wandering through forest and seeking out dangers to Harmony and Balance, or creatures in need. It's a tedious " +
					"job, but most of your apprenticeship was pretty tedious so far. Nevertheless, you vowed to obey the will of Elders and that's a task you " +
					"have been assigned to.\n\n" +
					"Press Spacebar to continue.";

		if 		(Input.GetKeyDown(KeyCode.Space)) 	{UpdateState(States.wolf,			0,		0,		0);}
	}
	
	void wolf() {
		text.text = "Suddenly, you hear a distant howl. Sounds like a wounded wolf, if you can trust your hearing. As far as you know, there are no poacher " +
					"camps in this part of the Forest. Out of curiosity and concern, you decide to follow the howl to learn more.\n\n" +
					"After a few minutes, you find the Wounded Wolf. Poor animal lies by a tree, forest litter around him red with it's blood. Wolf turns " +
					"his head towards you and bares his teeth, growling warningly.\n\n" +
					"Press I to approach the wolf and Inspect his wounds.\n" +
					"Press H to Heal his wounds from distance, using you magic. (Mana 4)\n" +
					"Press K to Kill the wolf, ending his suffering.\n" +
					"Press L to Leave the wolf to his fate.\n" +
					"Press C to try to Calm the animal. (Mana 1)";

		if 		(Input.GetKeyDown(KeyCode.I)) 		{UpdateState(States.wolf_inspect1, 	-2,		-2, 	0);}
		else if (Input.GetKeyDown(KeyCode.H))		{UpdateState(States.wolf_heal, 		1, 		-4, 	2);}
		else if (Input.GetKeyDown(KeyCode.K))		{UpdateState(States.wolf_kill, 		0, 		0, 		0);}
		else if (Input.GetKeyDown(KeyCode.L))		{UpdateState(States.wolf_leave, 	0, 		0, 		-1);}
		else if (Input.GetKeyDown(KeyCode.C))		{UpdateState(States.wolf_calming, 	0, 		-1, 	+1);}
	}
	
	void wolf_calm() {
		text.text = "You still stand among the old trees, wounded wolf lying by one of them. He is calm now, no longer baring his teeth. He stares at you " +
					"with his brown eyes, now seemingly begging for your help.\n\n" +
					"Press I to approach the wolf and Inspect his wounds.\n" +
					"Press H to Heal his wounds from distance, using you magic. (Mana 4)\n" +
					"Press K to Kill the wolf, ending his suffering.\n" +
					"Press L to Leave the wolf to his fate.";
		
		if 		(Input.GetKeyDown(KeyCode.I)) 		{UpdateState(States.wolf_inspect2,	0,		0,		0);}
		else if (Input.GetKeyDown(KeyCode.H))		{UpdateState(States.wolf_heal,		0,		-4,		2);}
		else if (Input.GetKeyDown(KeyCode.K))		{UpdateState(States.wolf_kill,		0,		0,		0);}
		else if (Input.GetKeyDown(KeyCode.L))		{UpdateState(States.wolf_leave,		0,		0,		-1);}
	}
	
	void wolf_calming() {
		text.text = "You concentrate on the forest that surrounds you. You gather slowly the energies needed, and then channel calming feelings into Wolf's " +
					"mind. Animal is soothing slowly, hiding it's teeth and ceasing to growl. As your mind touches wolf's however, you feel some unnatural " +
					"presence invanding animal's thoughts. Then it's suddenly gone, as soon as It felt that it has been discovered. " +
					"You are sure that you can now safely approach the Wolf.\n\n" +
					"Press Spacebar to continue.";
					
		wolf_calmed = true;
		
		if 		(Input.GetKeyDown(KeyCode.Space))	{UpdateState(States.wolf_calm,		0,		0,		0);}		
	}
	
	void wolf_inspect1() {
		text.text = "As you approach the Wolf, his growl intensifies. When you come too close, wounded animal bites your hand! " +
					"You hastily concentrate on the forest around you and draw upon the power of nature, easing beast's fears and forcing it to lay still. It " +
					"hides it's teeth and ceases to growl, allowing you to inspect it.\n\n" +
					"Wolf's wounds are not as severe as they looked in a first place. You are pretty sure that only with minimal magical effort you will " +
					"be able to heal those. There is something concerning to these wounds though. Claws and teeth which inflicted them did not belong to any " +
					"kind of animal you know. Also, animals do not hunt for sports. What kind of animal would have left wounded and vulnerable prey?\n\n" +
					"Press H to Heal wolf's wounds with magic. (Mana 2)\n" +
					"Press K to Kill the wolf, ending his suffering.\n" +
					"Press L to Leave the wolf to his fate.";

		if 		(Input.GetKeyDown(KeyCode.H))		{UpdateState(States.wolf_heal,		0,		-2,		2);}
		else if (Input.GetKeyDown(KeyCode.K))		{UpdateState(States.wolf_kill,		0,		0,		0);}
		else if (Input.GetKeyDown(KeyCode.L))		{UpdateState(States.wolf_leave,		0,		0,		-1);}
		
	}
	
	void wolf_inspect2() {
		text.text = "You approach the calmed Wolf. Animal lies on a ground quietly. You are glad that you managed to calm it. 'Wounded animals can be " +
					"unpredictable' - you think, remembering teachings of Master Olkhan, one of the Elders.\n\n"+
					"Wolf's wounds are not as severe as they looked in a first place. You are pretty sure that only with minimal magical effort you will " +
					"be able to heal those. There is something concerning to these wounds though. Claws and teeth which inflicted them did not belong to any " +
					"kind of animal you know. Also, animals do not hunt for sports. What kind of animal would have left wounded and vulnerable prey? Maybe " +
					"it has something to do with the presence you have felt before in Wolf's mind?\n\n" +
					"Press H to Heal wolf's wounds with magic. (Mana 2)\n" +
					"Press K to Kill the wolf, ending his suffering.\n" +
					"Press L to Leave the wolf to his fate.";
		
		if 		(Input.GetKeyDown(KeyCode.H))		{UpdateState(States.wolf_heal,		0,		-2,		2);}
		else if (Input.GetKeyDown(KeyCode.K))		{UpdateState(States.wolf_kill,		0,		0,		0);}
		else if (Input.GetKeyDown(KeyCode.L))		{UpdateState(States.wolf_leave,		0,		0,		-1);}
	}
	
	void wolf_heal() {
		text.text = "Drawing upon energies of nature, you concentrate on Wolf's wounds. Then, you channel gathered magical power into them, closing them " +
					"and easing animal's pain. Wolf whimpers quietly during the process, but magic works swiftly. Then it rises on his own, staring at you " +
					"thankfully for a brief moment, only to leave you and wander deeper into the Sacred Forest, probably in search of his pack. Soon you do " +
					"the same and return to roaming the Sacred Forest.\n\n" +
					"Press Space to continue";
					
		wolf_help = true;
		
		if 		(Input.GetKeyDown (KeyCode.Space)) 	{UpdateState(States.transition_0,	0,		0,		0);}
	}
	
	void wolf_kill() {
		text.text = "You decide to end this animal's torment and grab your bow, until now resting safely in your quiver along with arrows. You pull the string and aim your weapon, " +
					"then releasing arrow into wolf's chest. Animal whimpers briefly and then dies, glancing at you one last time with reproach. You then walk away, resuming your " +
					"patrol of Sacred Forest.\n\n" +
					"Press Space to continue";
					
		wolf_killed = true;

		if 		(Input.GetKeyDown (KeyCode.Space)) 	{UpdateState(States.transition_0,	0,		0,		0);}
	}
	
	void wolf_leave() {
		text.text = "You decide not to disturb this wounded animal. That's the Circle of Life - one must die so the others may live. Nature is sometimes brutal and unforgiving. You " +
					"walk away, and wounded wolf's howl slowly fades into the distance.\n\n" +
					"Press Space to continue";

		if 		(Input.GetKeyDown (KeyCode.Space)) 	{UpdateState(States.transition_0,	0,		0,		0);}
	}
#endregion
	
	void transition_0() {
		text.text = "As you continue to roam the Sacred Forest, situation with the wolf still bothers you. What could possibly attack an animal, and then simply leave it " +
					"to die? Something's not right here. As soon as you finish your patrol, you should report this to the Elders. Nevertheless, you still have many miles to travel.\n\n" +
					"After some time, maybe an hour or two since your encounter with the wolf, you begin to feel tired. There's a sanctuary pond nearby, place blessed by the Elders, " +
					"where every living creature can rest safely. You decide to head there and do exactly that.\n\n" +
					"Press Space to continue.";
			
		if 		(Input.GetKeyDown(KeyCode.Space)) {
			if 		(wolf_calmed&&wolf_help) 		{UpdateState(States.wolf_warning,	0,		0,		2);}
			else if (wolf_help)						{UpdateState(States.omen_0,			0,		0,		0);}
			else 									{UpdateState(States.wolf_fight,		-2,		0,		0);}
		
		Background = GetComponent<RawImage>();	
		Background.texture = forest2;			// Changing background texture.
		}
	}
	
	void wolf_warning() {
		text.text = "You arrive at the pond when sun begins to set. Glade where the pond lies is already shrouded in shadows. There's something wrong though. " +
					"You are alone and place is suprisingly quiet. You expected to find here lots of creatures seeking refuge for a night. Yet there's no-one but You.  " +
					"As you try to understand reasons behind this, you hear some animal aproaching you rapidly from behind.\n\n" +
					"It's the wolf, one you helped earlier. You recognize him by characteristic spot on his forehead. Animal looks really scared. " +
					" " +
					"Press H to Heed the warning.\n" +
					"Press I to Ignore the warning.";

		if 		(Input.GetKeyDown(KeyCode.H)) 		{UpdateState(States.omen_1,			0,		0,		0);}
		else if (Input.GetKeyDown(KeyCode.I))		{UpdateState(States.omen_0,			0,		0,		0);}
	}
	
	void omen_0() {
		text.text = "OMEN_0!\n\n" +
					"Press Space to continue.";

		if 		(Input.GetKeyDown(KeyCode.Space)) 	{UpdateState(States.tbc,			0,		0,		0);}
	}
	
	void omen_1() {
		text.text = "OMEN_1!\n\n" +
					"Press Space to continue.";

		if 		(Input.GetKeyDown(KeyCode.Space)) 	{UpdateState(States.tbc,			0,		0,		0);}
	}
	
	void tbc() {
		text.text = "TBC\n\n" +
					"Press P to play again.";

		if 		(Input.GetKeyDown(KeyCode.P)) 		{UpdateState(States.intro,			0,		0,		0);}
	}
	
	void wolf_fight() {
		if (wolf_killed) {
		text.text = "wolf_fight_killed\n\n" +
					"Press B to fight with Bow.\n" +
					"Press M to fight with Magic. (Mana 4)\n" +
					"Press C to try to Calm the creature.\n";
		
		if 		(Input.GetKeyDown(KeyCode.B)) 		{UpdateState(States.fight_bow,		-6,		0,		3);}
		if 		(Input.GetKeyDown(KeyCode.M)) 		{UpdateState(States.fight_magic,	-2,		-4,		2);}
		if 		(Input.GetKeyDown(KeyCode.C)) 		{UpdateState(States.fight_calm,		-10,	0,		0);}
		}
		
		else {
		text.text = "wolf_fight\n\n" +
					"Press B to fight with Bow.\n" +
					"Press M to fight with Magic. (Mana 4)\n" +
					"Press C to try to Calm the creature.\n";

		if 		(Input.GetKeyDown(KeyCode.B)) 		{UpdateState(States.fight_bow,		-6,		0,		3);}
		if 		(Input.GetKeyDown(KeyCode.M)) 		{UpdateState(States.fight_magic,	-2,		-4,		2);}
		if 		(Input.GetKeyDown(KeyCode.C)) 		{UpdateState(States.fight_calm,		-10,	0,		0);}
		}
	}
	
	void fight_bow() {
		text.text = "fight_bow\n\n" +
					"Press Space to continue.";

		if 		(Input.GetKeyDown(KeyCode.Space)) 	{UpdateState(States.omen_0,			0,		0,		0);}
	}
	
	void fight_magic() {
		text.text = "fight_magic\n\n" +
					"Press Space to continue.";
			
		if 		(Input.GetKeyDown(KeyCode.Space)) 	{UpdateState(States.omen_0,			0,		0,		0);}
	}
	
	void fight_calm() {
		text.text = "fight_calm\n\n" +
					"Press Space to continue.";

		if 		(Input.GetKeyDown(KeyCode.Space)) 	{UpdateState(States.omen_0,			0,		0,		0);}
	}
	
	void death() {
		text.text = "YOU DIED!\n\n" +
					"Press P to Play again.";
		if 		(Input.GetKeyDown(KeyCode.P)) 		{UpdateState(States.intro,			0,		0,		0);}
	}
#region custom methods	

	// Method to keep stats up to date. Converts stat integers to string and sets stat.text to display them.
	
	public void UpdateStat () {
		hp_string = Health.ToString();
		mn_string = Mana.ToString();
		cr_string = Courage.ToString();
		stat.text = "Health: " +hp_string +
					"\nMana: " +mn_string +
					"\nCourage: " +cr_string;	
	}
	
	// This method checks if player's dead. Obviously.
	
	public void DeathCheck () {
		if 		(Health<=0)		{myState=States.death;}
	}
	
	// Custom state updating method. Takes name of new state and stat adjustments as arguments. 
		
	public void UpdateState (States newState, int HPad,  int MNad, int CRad) {
		Health	=	Health+HPad;		// Here we have adjusting stats. We do it first for reasons.
		Mana	=	Mana+MNad;
		Courage	=	Courage+CRad;
		
		// Stupid piece of code to display stats adjustments like gaining/losing health or other stuff.
		#region Stat adjusments indicators
		Jebane_HP.text	=	null;
		Jebane_MP.text	=	null;
		Jebane_CR.text	=	null;
		
			if (HPad != 0) {
				ChHP_string = HPad.ToString();
				if (HPad<0) {
					Jebane_HP.color	=	Color.red;
				}
				else {
					Jebane_HP.color	=	Color.green;
					ChHP_string			=	"+" + ChHP_string;
				}
				Jebane_HP.text	=	ChHP_string;
			}
			
			if (MNad != 0) {
				ChMN_string = MNad.ToString();
				if (MNad<0) {
					Jebane_MP.color	=	Color.red;
				}
				else {
					Jebane_MP.color	=	Color.green;
					ChMN_string			=	"+" + ChMN_string;
				}
				Jebane_MP.text		=	ChMN_string;
			}
			
			if (CRad != 0) {
				ChCR_string = CRad.ToString();
				if (CRad<0) {
					Jebane_CR.color	=	Color.red;
				}
				else {
					Jebane_CR.color	=	Color.green;
					ChCR_string			=	"+" + ChCR_string;
				}
				Jebane_CR.text		= ChCR_string;
			}
		#endregion

	
		myState = newState;
	
	// Insert facepalm.
		
		DeathCheck();					// Check if health is 0 or lower. We do it last so it overwrites any other state in case player DIED.
	}

#endregion
	
}
