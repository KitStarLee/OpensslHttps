using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocket4Net;
public class SocketTest : MonoBehaviour {
    WebSocket m_webSocket;
    string ip = "192.168.1.241";
    int port = 10011;
    // Use this for initialization
    void Start() {
        Initialize(ip, port);
    }

    public void Initialize(string ip, int port) {
        m_webSocket = new WebSocket(string.Format("ws://{0}:{1}", ip, port));
        m_webSocket.MessageReceived += new System.EventHandler<MessageReceivedEventArgs>(OnReceiveMessage);
        m_webSocket.NoDelay = true;
        m_webSocket.AllowUnstrustedCertificate = true;
		m_webSocket.EnableAutoSendPing = true;
		m_webSocket.AutoSendPingInterval = 5;
        m_webSocket.Open();
    }

    public void PBLogoutAndDisconnect() {
        SendStringMessage("Close");
        m_webSocket.Close();
    }

    void SendStringMessage(string message) {
        Debug.Log(1);
        m_webSocket.Send(message);
        Debug.Log(2);
    }
    public void Connect() {
        m_webSocket.Open();
    }

    void OnReceiveMessage(object sender, MessageReceivedEventArgs e) {
        Debug.Log(e.Message);
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Q)) {
            m_webSocket.Open();
        } else if (Input.GetKeyDown(KeyCode.W)) {
            SendStringMessage("{\"type\":\"set\",\"name\":\"lyj\",\"location\":{\"j\":12,\"w\":54}}");
        } else if (Input.GetKeyDown(KeyCode.E)) {
            SendStringMessage("{\"type\":\"get\",\"name\":\"lyj\"}");
        } else if (Input.GetKeyDown(KeyCode.R)) {
            m_webSocket.Close();
        }
    }
}
