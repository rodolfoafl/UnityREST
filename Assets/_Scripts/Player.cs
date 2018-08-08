using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.IO;

[System.Serializable]
public class Player {

	public int id;
	public string name;
    public string password;
	public string score;

	//public Player[] _players;

	public static Player[] GetPlayerData(){
		HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://localhost/php_rest/api/player/read.php");
		HttpWebResponse response = (HttpWebResponse)request.GetResponse();
		StreamReader reader = new StreamReader(response.GetResponseStream());
		string jsonResponse = reader.ReadToEnd();

		Player[] players = JsonHelper.FromJson<Player> (jsonResponse);
		return players;

		//Player p = JsonUtility.FromJson<Player>(jsonResponse);
		//return p;

		/*foreach (Player p in _players) {
			Debug.Log ("Player Name: " + p.name);
			Debug.Log ("Player Score: " + p.score);
		}*/
	}

	public static bool CreatePlayer(string json){
		HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://localhost/php_rest/api/player/create.php");
		request.Method = "POST";
		request.ContentType = "application/json";

		var streamWriter = new StreamWriter (request.GetRequestStream ());
		streamWriter.Write(json.ToCharArray());
		streamWriter.Close ();

		HttpWebResponse response = (HttpWebResponse)request.GetResponse();
		var reader = new StreamReader(response.GetResponseStream());
		var textResponse = reader.ReadToEnd ();

        Debug.Log(textResponse);
        if (textResponse.Contains("Not"))
        {
            return false;
        }
        else
        {
            return true;
        }
		
	}

    public static bool LoginPlayer(string json)
    {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://localhost/php_rest/api/player/login.php");
        request.Method = "POST";
        request.ContentType = "application/json";

        var streamWriter = new StreamWriter(request.GetRequestStream());
        streamWriter.Write(json.ToCharArray());
        streamWriter.Close();

        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        var reader = new StreamReader(response.GetResponseStream());
        var textResponse = reader.ReadToEnd();

        Debug.Log(textResponse);
        
        if (textResponse.Contains("error"))
        {
            Debug.Log("Erro no Login");
            return false;
        }
        else
        {
            return true;
        }
    }

    /*static string GetFullName(string response) {
        var begin = response.LastIndexOf("contactFullName\":\"");
        var end = response.IndexOf("\",\"contactLID");
        var text = response.Substring(begin, end - begin);

        begin = text.LastIndexOf("\"");
        begin++;
       
        var name = text.Substring(begin, text.Length - begin);
        return name;
        //Debug.Log(name);
    }*/




}
