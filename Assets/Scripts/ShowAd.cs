using UnityEngine;
using System.Collections;
using System.IO;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class ShowAd : MonoBehaviour {

	public static int siteID = 5;
	public string url = "http://10.5.89.10/transbidder?s=" + siteID;
	private string bidString = "{"
		+ "\"id\":1,"
		+ "\"site\":{"
		+ "\"page\":\"ajaxpub.com\","         // Site URL
		+ "\"domain\":\"ajaxpub.com\","       // Site domain
		+ "\"ref\":\"referrerPage.com\","             // Referrer
		+ "\"publisher\":{"
		+ "\"id\":23"
		+ "},"
		+ "\"cat\":[\"IAB19\"]"                       // Site category
		+ "},"
		+ "\"device\":{"
		+ "\"ua\":\"Mozilla/5.0 (X11; Linux i686) AppleWebKit/537.11 (KHTML, like Gecko) Chrome/23.0.1271.97 Safari/537.11\","        // User agent string
		+ "\"ip\":\"69.77.169\","     // IP address
		+ "\"language\":\"en\","      // Language
		+ "\"devicetype\":2,"       // Device type: 1 mobile, 2 PC, 3 TV
		+ "\"flashver\":\"9\""        // Flash version
		+ "},"
		+ "\"user\":{"
		+ "\"buyeruid\":\"cmid&cmps\""        // External user ID, populated above based on cookies
		+ "},"
		+ "\"imp\": [{"
		+ "\"id\":1,"
		+ "\"banner\":{"
		+ "\"w\":728,"              // Requested ad width
		+ "\"h\":90,"               // Requested ad height
		+ "\"pos\":1,"              // Requested ad position: 0 unknown, 1 Above-the-Fold, 2 depends, 3 Below-the-Fold
		+ "\"topframe\":0,"         // In topframe: 0 ad is in iframe, 1 ad is in top frame
		+ "\"battr\":[]"            // Blocked creative attributes
		+ "},"
		+ "\"pmp\": {"
		+ "\"private_auction\":0,"  // Private auction flag
		+ "\"deals\": []"           // Direct deals
		+ "},"
		+ "\"bidfloor\":0,"         // Bid floor
		+ "\"bidflorcur\":\"USD\""    // Bid floor currency
		+ "}],"
		+ "\"badv\":[\"blockedAdvertiser.com\"],"     // Blocked advertisers
		+ "\"bcat\":[\"IAB17\"],"                     // Blocked site categories
		+ "\"regs\": {"
		+ "\"coppa\":0"     // Request falls under COPPA regulations
		+ "},"
		+ "\"ext\": {"
		+ "\"ssl\":0"       // SSL request flag
		+ "}"
		+ "}";

	public GameObject player;
	private Renderer rend;
	private bool isViewed = false;
	private bool notificationSent = false;
	private bool responseReceived = false;
	private String callbackURL;

	public void Start()
	{

		rend = GetComponent<Renderer>();
		WWWForm form = new WWWForm();
		form.AddField("id", "1");
		Dictionary<string, string> postHeader = new Dictionary<string, string>();
		postHeader.Add("Content-Type", "text/json");
		postHeader.Add("Content-Length", bidString.Length.ToString());

		WWW www = new WWW(url, System.Text.Encoding.UTF8.GetBytes(bidString), postHeader);

		StartCoroutine(WaitForRequest(www));


	}

	// Update is called once per frame
	IEnumerator WaitForRequest(WWW www)
	{
		yield return www;
		WWW imageTexture;
		string creative = getResponse(www.text);
		imageTexture = new WWW(creative);
		yield return imageTexture;
		Renderer renderer = GetComponent<Renderer>();
		renderer.material.mainTexture = imageTexture.texture;
		responseReceived = true;

	}

	public void Update()
	{
		if (!notificationSent && responseReceived)
		{
			if (isInView())
			{				
				sendNotification();
			}
		}
	}

	private string getResponse(string responseString)
	{
		string[] fieldList = responseString.Split(',');
		string creative = "0";
		foreach (string field in fieldList)
		{
			string[] val = field.Split(':');
			if (val[0] == "\"adm\"")
			{
				Debug.Log (field);			
				int startPos = field.LastIndexOf("img src=\\\"") + "img src=\\\"".Length;
				int length = field.IndexOf("\\\"><img width") - startPos;
				Debug.Log (length);
				Debug.Log (startPos);
				creative = field.Substring(startPos, length);
				startPos = field.LastIndexOf("iframe src=\\\"") + "iframe src=\\\"".Length;
				length = field.IndexOf("\\\" width") - startPos;
				callbackURL = field.Substring(startPos, length);
				Debug.Log (callbackURL);
				return creative;
			}                        
		}

		if (creative != "0")
		{
			return creative;
		} else
		{
			return null;
		}
	}

	private void sendNotification()
	{		
		WWW www = new WWW(callbackURL);
		StartCoroutine(sendWWWNotification(www));
		notificationSent = true;
	}

	IEnumerator sendWWWNotification(WWW www)
	{
		yield return www;
	}  

	private bool isInView()
	{		
		if (!isViewed && rend.isVisible) {
			return true;
		} else {
			return false;
		}
	}
}
