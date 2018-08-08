using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour {

	private Player[] _players;
    private Player _newPlayer = new Player();

    [SerializeField] RectTransform panel;

    [SerializeField] RectTransform panelScore;
    [SerializeField] Text textScoreboard;
	//[SerializeField] Text textPrefab;

	[SerializeField] InputField nameInput;
	[SerializeField] InputField scoreInput;
    [SerializeField] InputField passwordInput;

    [SerializeField] Text errorText;
    [SerializeField] Text scoreSentText;

    void Start() {
        errorText.enabled = false;
        panelScore.gameObject.SetActive(false);
    }

	public void UpdatePanel(){
        ResetScoreboardText();

		_players = Player.GetPlayerData ();

        int i = 1;

        foreach(Player p in _players)
        {
            var line = i + " - " + p.name + " - " + p.score;
            textScoreboard.text += line + "\n";
            i++;
        }
	}

    public void ResetScoreboardText()
    {
        textScoreboard.text = "";
    }

    public void Login()
    {
        _newPlayer.name = nameInput.text;
        _newPlayer.password = passwordInput.text;

        string toJson = JsonUtility.ToJson(_newPlayer);

        //string loginReturn = Player.LoginPlayer(toJson);

        if (!Player.LoginPlayer(toJson))
        {
            errorText.enabled = true;
        }
        else
        {
            //_newPlayer.name = loginReturn;
            errorText.enabled = false;
            panelScore.gameObject.SetActive(true);
        }
    }

    public void SendScore(){
        _newPlayer.password = "0";
        _newPlayer.score = scoreInput.text;

		string toJson = JsonUtility.ToJson (_newPlayer);
        Debug.Log(toJson);

		if(!Player.CreatePlayer(toJson))
        {
            scoreSentText.text = "Erro ao enviar pontuação!";
        }
        else
        {
            scoreSentText.text = "Pontuação enviada com sucesso!";
        }
        scoreSentText.enabled = true;
	}


}
