using UnityEngine;
using System;
using System.Net.Sockets;
using System.Net;

public class NTP_Test : MonoBehaviour
{
    public static DateTime GetNetworkTime()
    {
        // 사용할 NTP 서버 주소 "time.windows.com" = windows의 서버시간
        const string ntpServer = "time.windows.com";

        // NTP 메시지를 저장할 배열 (NTP 메시지는 48바이트로 구성됨)
        var ntpData = new byte[48];

        // NTP 서버 시간에 대한 경고 신호 설정
        ntpData[0] = 0x1B; //LI = 0 (no warning), VN = 3 (IPv4 only), Mode = 3 (Client Mode)

        // 지정된 NTP 서버의 IP 주소 목록을 불러옴
        var addresses = Dns.GetHostEntry(ntpServer).AddressList;

        // NTP는 UDP(User Datagram Protocol)를 사용함
        // NTP에 할당된 UDP 포트 번호 : 123
        var ipEndPoint = new IPEndPoint(addresses[0], 123);

        // 소켓을 NTP 서버에 연결하고, 메시지를 전송한 후 응답을 기다림.
        using (var socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp))
        {
            socket.Connect(ipEndPoint);

            // 수신 타임아웃을 3초로 설정 하여 응답이 없을경우 코드가 멈추지 않도록 함
            socket.ReceiveTimeout = 3000;

            socket.Send(ntpData);
            socket.Receive(ntpData);
            socket.Close();
        }

        // 서버의 응답에서 '전송 타임스탬프' 필드를 추출함.
        // 이 필드는 64비트 타임스탬프 형식으로 되어 있으며, 초와 초의 분수 부분를 따로 가져옴.
        const byte serverReplyTime = 40;
        
        ulong intPart = BitConverter.ToUInt32(ntpData, serverReplyTime);
        ulong fractPart = BitConverter.ToUInt32(ntpData, serverReplyTime + 4);

        // NTP의 시간 형식은 빅 엔디안으로 되어 있으므로, 로컬 시스템의 리틀 엔디안으로 변환.
        intPart = SwapEndianness(intPart);
        fractPart = SwapEndianness(fractPart);

        var milliseconds = (intPart * 1000) + ((fractPart * 1000) / 0x100000000L);

        // 초와 분을 기반으로 밀리초를 계산하고, 1900년 1월 1일을 기준으로 UTC 시간을 생성.
        var networkDateTime = (new DateTime(1900, 1, 1, 0, 0, 0, DateTimeKind.Utc)).AddMilliseconds((long)milliseconds);
        return networkDateTime.ToLocalTime(); // UTC 시간을 로컬 시간으로 변환하여 반환.
    }

    // 정수의 엔디안 형식을 변환하는 함수 (비트 연산 사용)
    static uint SwapEndianness(ulong x)
    {
        return (uint)(((x & 0x000000ff) << 24) +
                      ((x & 0x0000ff00) << 8) +
                      ((x & 0x00ff0000) >> 8) +
                      ((x & 0xff000000) >> 24));
    }
}