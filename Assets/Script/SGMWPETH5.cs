using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Net;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Xml;
using System.Net.Security;

public class SGMWPETH5 : MonoBehaviour {

    public Text text;

    string filePath = null;


    X509Certificate2 adminClient;
    string clientCertPath = "";
    static string baseUrl = "https://192.168.1.69:10010";


    // Use this for initialization
    void Start () {

        filePath =
#if UNITY_ANDROID
 Application.persistentDataPath;
#elif UNITY_EDITOR
 Application.dataPath + "/StreamingAssets" ;
#elif UNITY_IPHONE
 Application.persistentDataPath;
#endif

        clientCertPath = filePath + "/client.p12";

        text.text += clientCertPath;
        Debug.Log(clientCertPath);
        StartCoroutine(GetCert());

    }
	
    public void OnButtonClientToServer()
    {
        text.text = "";
        text.text += "  ; @" + "-4";
        StartCoroutine(GetCert());
    }
    private IEnumerator GetCert()
    {
        if (File.Exists(clientCertPath))
        {
            print("I have the file");
            text.text += "  ; @" + "-3";
            MakeRestCall();
        }
        else
        {
            text.text += "  ; @" + "-2";
            WWW download = new WWW("http://192.168.1.69/client.p12");
            yield return download;

            text.text += "  ; @" + "-1";

            text.text += "  ; @" + download.error;

            if (download.error != null)
            {
                print("Error downloading: " + download.error);
            }
            else
            {
                File.WriteAllBytes(clientCertPath, download.bytes);

            }
        }
    }

    public XmlDocument MakeRestCall()
    {
        text.text += "  ; @" + "0";
        try
        {
            ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(ValidateServerCertificate);

            text.text += "  ; @" + "1";
            adminClient = new X509Certificate2(clientCertPath, "123456");
            text.text += "  ; @" + "2";

            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(baseUrl);

            text.text += "  ; @" + "3";

            request.AuthenticationLevel = AuthenticationLevel.MutualAuthRequested;

            text.text += "  ; @" + "4";

            request.ClientCertificates.Add(adminClient);

            text.text += "  ; @" + "5";

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            text.text += "  ; @" + "6";

            Debug.Log(response.StatusDescription);

            text.text += "  ; @" + response.Server;
            text.text += "  ; @" + response.StatusDescription;

            XmlDocument xmlDoc = new XmlDocument();

            //  byte[] reqDaata = new byte[(int)response.GetResponseStream().Length];

            
            // xmlDoc.Load(response.GetResponseStream());

            return xmlDoc;
        }
        catch(WebException ex)
        {
            text.text += "  ; @" + "7";
            text.text += "  ; @" + ex;
            XmlDocument xmlDoc = new XmlDocument();
            return xmlDoc;
        }

    }

    public static bool ValidateServerCertificate(
        object sender,
        X509Certificate certificate,
        X509Chain chain,
        SslPolicyErrors sslPolicyErrors)
        {
            //TODO Make this more secure
            return true;
        }





}
