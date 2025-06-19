using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class NetworkTest : MonoBehaviour
{
    private Socket clientSocket;
    [SerializeField]
    private string serverIP = "172.26.90.110";  // Ŀ���������IP��ַ
    [SerializeField]
    private int serverPort = 8888;         // Ŀ��������Ķ˿�

    void Start()
    {
        // �������ӵ�������
        ConnectToServer();
    }

    // ���ӵ�������
    void ConnectToServer()
    {
        try
        {
            // ����һ��Socket��ָ����ַ�塢�׽������ͺ�Э������
            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            // ��������IP��ַ�Ͷ˿�
            IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Parse(serverIP), serverPort);

            // �������ӵ�������
            clientSocket.Connect(serverEndPoint);
            Debug.Log("Connected to server!");

            // ������Ϣ
            SendMessageToServer("Explain_goal");

            // ��ʼ������Ϣ
            ReceiveMessageFromServer();
        }
        catch (Exception ex)
        {
            Debug.LogError("Error connecting to server: " + ex.Message);
        }
    }

    // ������Ϣ��������
    void SendMessageToServer(string message)
    {
        try
        {
            byte[] data = Encoding.UTF8.GetBytes(message);
            clientSocket.Send(data);
            Debug.Log("Message sent to server: " + message);
        }
        catch (Exception ex)
        {
            Debug.LogError("Error sending message: " + ex.Message);
        }
    }

    // ���մӷ��������ص���Ϣ
    void ReceiveMessageFromServer()
    {
        try
        {
            byte[] buffer = new byte[1024]; // �������ջ�����
            int receivedBytes = clientSocket.Receive(buffer); // ��������

            if (receivedBytes > 0)
            {
                string receivedMessage = Encoding.UTF8.GetString(buffer, 0, receivedBytes);
                Debug.Log("Message received from server: " + receivedMessage);
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("Error receiving message: " + ex.Message);
        }
    }

    // �ر�Socket����
    void OnApplicationQuit()
    {
        if (clientSocket != null)
        {
            clientSocket.Close();
            Debug.Log("Connection closed.");
        }
    }
}