using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MainMenu : MonoBehaviour {

	private static int padding = 10;
	private static int buttonHeight = 100;
	private static int boxWidth = Screen.width - padding * 2;
	private static int boxHeight = buttonHeight * 4 + padding * 3;
	private static int boxY = (Screen.height - boxHeight) / 2;
	private static int buttonWidth = boxWidth - padding * 2;
	
	void OnGUI() {
	
		GUIStyle buttonStyle = new GUIStyle("button");
		buttonStyle.fontSize = 32;
		GUIStyle boxStyle = new GUIStyle("box");
		boxStyle.fontSize = 38;
		
		GUIStyle labelStyle = new GUIStyle("label");
		labelStyle.fontSize = 18;
		labelStyle.alignment = TextAnchor.LowerCenter;
		
		GUI.Box(new Rect(padding, boxY, boxWidth, boxHeight), "Main Menu", boxStyle);
		
		if(GUI.Button(new Rect(padding * 2, buttonHeight + boxY, buttonWidth, buttonHeight), "Wallet Dashboard", buttonStyle)) {
			Arbiter.DisplayWalletDashboard( OnWalletDashboardClose );
		}
		
		GUI.Label(new Rect(padding * 2, buttonHeight * 2 + boxY, buttonWidth, buttonHeight), "Entry fee: 50 credits", labelStyle);
		if(GUI.Button(new Rect(padding * 2, buttonHeight * 2 + padding + boxY, buttonWidth, buttonHeight), "Join Tournament", buttonStyle)) {
			string betSize = "50";
			Dictionary<string,string> filters = new Dictionary<string,string>();
			filters.Add("level", "2");
			Arbiter.JoinTournament( betSize, filters, OnTournamentReturned );
		}
		
		if(GUI.Button(new Rect(padding * 2, buttonHeight * 3 + padding * 2 + boxY, buttonWidth, buttonHeight), "Logout", buttonStyle)) {
			Arbiter.Logout( LogoutCallback );
		}
	}
	
	private void OnWalletDashboardClose() {
		Debug.Log ("Dashboard closed");
	}
	
	private void OnTournamentReturned( Arbiter.Tournament tournament ) {
		Debug.Log ("tournament: " + tournament.Id);
	}
	
	private void LogoutCallback() {
		Application.LoadLevel ("Login");
	}
}
