//Hey, welcome to my code. I'm sorry for breaking every style rule in existence.

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using KModkit;

public class workingTitleCode : MonoBehaviour 
{
	public KMBombInfo edgework;
	public KMAudio sound;
	
	public KMSelectable button1;
	public KMSelectable button2;
	public KMSelectable button3;
	public TextMesh screenText;
	
	public string[] aText;
	public string[] bText;
	public string[] cText;
	public int buttonToPress;
	public int textUsed;
	
	static int moduleIdCounter = 1;
	int moduleId;
	private bool textPicked = false;
	private bool moduleSolved; 
	
	void Awake ()
	{
		moduleId = moduleIdCounter++;
		button1.OnInteract += delegate () { PressButton1(); return false; };
		button2.OnInteract += delegate () { PressButton2(); return false; };
		button3.OnInteract += delegate () { PressButton3(); return false; };
	}
	
	void Start () 
	{
		if (textPicked == false)
		{
			PickText();
			textPicked = true;
		}
	}
	
	void PickText()
	{
		buttonToPress = UnityEngine.Random.Range(0,3);
		textUsed = UnityEngine.Random.Range(0,25);
		switch (buttonToPress)
		{
			case 0:
				screenText.text = aText[textUsed];
				Debug.LogFormat("[Working Title #{0}] The screen says {1}.", moduleId, aText[textUsed]);
				Debug.LogFormat("[Working Title #{0}] You have to push Button A.", moduleId);
				break;
		
			case 1:
				screenText.text = bText[textUsed];
				Debug.LogFormat("[Working Title #{0}] The screen says {1}.", moduleId, bText[textUsed]);
				Debug.LogFormat("[Working Title #{0}] You have to push Button B.", moduleId);
				break;
		
			case 2:
				screenText.text = cText[textUsed];
				Debug.LogFormat("[Working Title #{0}] The screen says {1}.", moduleId, cText[textUsed]);
				Debug.LogFormat("[Working Title #{0}] You have to push Button C.", moduleId);
				break;
				
			default:
				Debug.LogFormat("[Working Title #{0}] Uh oh, something's wrong! Panic!", moduleId);
				break;
		}
	}
	
	void PressButton1 ()
	{
		Debug.LogFormat("[Working Title #{0}] You pushed Button A.", moduleId);
		button1.AddInteractionPunch();
		GetComponent<KMAudio>().PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
		if (moduleSolved == false)
		{
			if (buttonToPress == 0)
			{
				moduleSolved = true;
				GetComponent<KMBombModule>().HandlePass();
				screenText.text = "solve text";
				Debug.LogFormat("[Working Title #{0}] Correct, module solved.", moduleId);
				
			}
			else
			{
				Debug.LogFormat("[Working Title #{0}] Wrong, module striked.", moduleId);
				GetComponent<KMBombModule>().HandleStrike();
			}
		}
	}
	
	void PressButton2 ()
	{
		Debug.LogFormat("[Working Title #{0}] You pushed Button B.", moduleId);
		button2.AddInteractionPunch();
		GetComponent<KMAudio>().PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
		if (moduleSolved == false)
		{
			if (buttonToPress == 1)
			{
				moduleSolved = true;
				GetComponent<KMBombModule>().HandlePass();
				screenText.text = "solve text";
				Debug.LogFormat("[Working Title #{0}] Correct, module solved.", moduleId);
				
			}
			else
			{
				Debug.LogFormat("[Working Title #{0}] Wrong, module striked.", moduleId);
				GetComponent<KMBombModule>().HandleStrike();
			}
		}
	}
	
	void PressButton3 ()
	{
		Debug.LogFormat("[Working Title #{0}] You pushed Button C.", moduleId);
		button3.AddInteractionPunch();
		GetComponent<KMAudio>().PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
		if (moduleSolved == false)
		{
			if (buttonToPress == 2)
			{
				moduleSolved = true;
				GetComponent<KMBombModule>().HandlePass();
				screenText.text = "solve text";
				Debug.LogFormat("[Working Title #{0}] Correct, module solved.", moduleId);
				
			}
			else
			{
				Debug.LogFormat("[Working Title #{0}] Wrong, module striked.", moduleId);
				GetComponent<KMBombModule>().HandleStrike();
			}
		}
	}
	
	void TwitchHandleForcedSolve()
	{
		Action press = buttonToPress == 0 ? (Action)PressButton1 : buttonToPress == 1 ? (Action)PressButton2 : (Action)PressButton3;
		press();
	}
	
	#pragma warning disable 414
	public string TwitchHelpMessage = "Use '!{0} <button>' to press a button! Button can be 'a; b; c; 1; 2; 3'";
	#pragma warning restore 414
	IEnumerator ProcessTwitchCommand(string command)
	{
		yield return null;
		switch(command.ToLowerInvariant())
		{
			case "1":
			case "a":
				button1.OnInteract();
				break;
			case "2":
			case "b":
				button2.OnInteract();
				break;
			case "3":
			case "c":
				button3.OnInteract();
				break;
			default:
				yield return "sendtochaterror Invalid button!";
				yield break;
		}
	}
}
