using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MainMenu : MonoBehaviour {

	private static int padding = 10;
	private static int buttonHeight = 100;
	private static int boxWidth = Screen.width - padding * 2;
	private static int boxHeight = buttonHeight * 6 + padding * 3;
	private static int boxY = (Screen.height - boxHeight) / 2;
	private static int buttonWidth = boxWidth - padding * 2;
	
	void Awake() {
		if ( GameObject.Find("GameState") == null ) {
			GameObject go = new GameObject( "GameState" );
			go.AddComponent<GameState>();
		}
	}
	
	void OnGUI() {
	
		GUIStyle buttonStyle = new GUIStyle("button");
		buttonStyle.fontSize = 32;
		GUIStyle boxStyle = new GUIStyle("box");
		boxStyle.fontSize = 38;
		
		GUIStyle labelStyle = new GUIStyle("label");
		labelStyle.fontSize = 18;
		labelStyle.alignment = TextAnchor.LowerCenter;
		
		GUI.Box(new Rect(padding, boxY, boxWidth, boxHeight), "Main Menu", boxStyle);
		GUI.Label(new Rect(padding * 2, boxY - padding, buttonWidth, buttonHeight), "Arbiter User Id:" + Arbiter.UserId, labelStyle);
		
		if(GUI.Button(new Rect(padding * 2, buttonHeight + boxY, buttonWidth, buttonHeight), "Wallet Dashboard", buttonStyle)) {
			Arbiter.DisplayWalletDashboard( OnWalletDashboardClose );
		}
		
		GUI.Label(new Rect(padding * 2, buttonHeight * 2 + boxY, buttonWidth, buttonHeight), "Entry fee: 0 credits", labelStyle);
		if(GUI.Button(new Rect(padding * 2, buttonHeight * 2 + padding + boxY, buttonWidth, buttonHeight), "Join Free Tournament", buttonStyle)) {
			string betSize = "0";
			Dictionary<string,string> filters = new Dictionary<string,string>();
			filters.Add("level", "2");
			Arbiter.JoinTournament( betSize, filters, OnTournamentReturned );
		}
		
		GUI.Label(new Rect(padding * 2, buttonHeight * 3 + boxY + padding, buttonWidth, buttonHeight), "Entry fee: 50 credits", labelStyle);
		if(GUI.Button(new Rect(padding * 2, buttonHeight * 3 + padding * 2 + boxY, buttonWidth, buttonHeight), "Join Cash Tournament", buttonStyle)) {
			string betSize = "50";
			Dictionary<string,string> filters = new Dictionary<string,string>();
			filters.Add("level", "2");
			Arbiter.JoinTournament( betSize, filters, OnTournamentReturned );
		}
		
		if(GUI.Button(new Rect(padding * 2, buttonHeight * 4 + padding * 3 + boxY, buttonWidth, buttonHeight), "Previous Tournaments", buttonStyle)) {
			Arbiter.ViewPreviousTournaments( OnViewPreviousTournamentsClosed );
		}
		
		if(GUI.Button(new Rect(padding * 2, buttonHeight * 5 + padding * 4 + boxY, buttonWidth, buttonHeight), "Logout", buttonStyle)) {
			Arbiter.Logout( LogoutCallback );
		}
	}
	
	private void OnWalletDashboardClose() {
		Debug.Log ("Dashboard closed");
	}
	
	private void OnTournamentReturned( Arbiter.Tournament tournament ) {
		GameObject go = GameObject.Find("GameState");
		GameState gameState = go.GetComponent<GameState>();
		gameState.CurrentTournamentId = tournament.Id;
		Application.LoadLevel("Game");
	}
	
	private void OnViewPreviousTournamentsClosed() {
		Debug.Log ("ViewPreviousTournaments closed");
	}
	
	private void LogoutCallback() {
		Application.LoadLevel ("Login");
	}
}
