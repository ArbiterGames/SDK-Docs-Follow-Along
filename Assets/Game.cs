﻿using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour {
	
	private static int padding = 10;
	private static int buttonHeight = 100;
	private static int boxWidth = Screen.width - padding * 2;
	private static int boxHeight = buttonHeight * 3 + padding * 3;
	private static int boxY = (Screen.height - boxHeight) / 2;
	private static int buttonWidth = boxWidth - padding * 2;
	
	private int score;
	private bool scoreReported;
	private string resultsDescription;
	
	
	void OnGUI() {
		
		GUIStyle buttonStyle = new GUIStyle("button");
		buttonStyle.fontSize = 32;
		GUIStyle boxStyle = new GUIStyle("box");
		boxStyle.fontSize = 38;
		GUIStyle labelStyle = new GUIStyle("label");
		labelStyle.fontSize = 18;
		labelStyle.alignment = TextAnchor.MiddleCenter;
		
		GUI.Box(new Rect(padding, boxY, boxWidth, boxHeight), "The Game", boxStyle);
	
		if ( scoreReported ) {
			GUI.Label(new Rect(padding * 2, boxY, buttonWidth, buttonHeight), resultsDescription, labelStyle);
			if(GUI.Button(new Rect(padding * 2, buttonHeight * 2 + boxY + padding, buttonWidth, buttonHeight), "Back to Main Menu", buttonStyle)) {
				Application.LoadLevel ("MainMenu");
			}
		} else {
			if(GUI.Button(new Rect(padding * 2, buttonHeight + boxY, buttonWidth, buttonHeight), "Play", buttonStyle)) {
				string tournamentId = "9bd167aec7df441e98dce150f2e2ac21";
				score = (int)UnityEngine.Random.Range( 1f, 100f );
				Arbiter.ReportScore( tournamentId, score, OnScoreReported );
			}
		}
	}
	
	void OnScoreReported( Arbiter.Tournament tournament ) {
		Debug.Log("OnScoreReported tournament:" + tournament);
		
		scoreReported = true;
		
		if( tournament.Status == Arbiter.Tournament.StatusType.Complete ) {
			if( tournament.Winner.Id == Arbiter.UserId ) {
				resultsDescription = "You won with a score of " + score + "!";
			} else {
				resultsDescription = "You lost " + tournament.Winner.Score + " to " + score;
			}
		} else if( tournament.Status == Arbiter.Tournament.StatusType.InProgress || tournament.Status == Arbiter.Tournament.StatusType.Initializing ) {
			resultsDescription = "You scored " + score + ".Waiting for opponent to finish.";
		} else {
			Debug.LogError( "Found unexpected game status code ("+tournament.Status+")!" );
		}
	}
}