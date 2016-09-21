using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace IC.NotificationHandler
{
    public class PushAPNSMessage
    {
        //    public void pushMessage(string deviceID)
        //    {
        //        int port = 2195;
        //        String hostname = "gateway.push.apple.com";
        //        String certificatePath = System.Web.Hosting.HostingEnvironment.MapPath("pushkey.p12");
        //        X509Certificate2 clientCertificate = new X509Certificate2(System.IO.File.ReadAllBytes(certificatePath), "yourPassword");
        //        X509Certificate2Collection certificatesCollection = new X509Certificate2Collection(clientCertificate);

        //        TcpClient client = new TcpClient(hostname, port);
        //        SslStream sslStream = new SslStream(client.GetStream(), false, new RemoteCertificateValidationCallback(ValidateServerCertificate), null);

        //        try
        //        {
        //            sslStream.AuthenticateAsClient(hostname, certificatesCollection, SslProtocols.Tls, false);
        //            MemoryStream memoryStream = new MemoryStream();
        //            BinaryWriter writer = new BinaryWriter(memoryStream);
        //            writer.Write((byte)0);
        //            writer.Write((byte)0);
        //            writer.Write((byte)32);

        //            writer.Write(HexStringToByteArray(deviceID.ToUpper()));
        //            String payload = "{\"aps\":{\"alert\":\"" + "Hi,, This Is a Sample Push Notification For IPhone.." + "\",\"badge\":1,\"sound\":\"default\"}}";
        //            writer.Write((byte)0);
        //            writer.Write((byte)payload.Length);
        //            byte[] b1 = System.Text.Encoding.UTF8.GetBytes(payload);
        //            writer.Write(b1);
        //            writer.Flush();
        //            byte[] array = memoryStream.ToArray();
        //            sslStream.Write(array);
        //            sslStream.Flush();
        //            client.Close();
        //        }
        //        catch (System.Security.Authentication.AuthenticationException ex)
        //        {
        //            client.Close();
        //        }
        //        catch (Exception e)
        //        {
        //            client.Close();
        //        }
        //    }

        //    public byte[] HexStringToByteArray(String s)
        //    {
        //        int len = s.Length();
        //        byte[] data = new byte[len / 2];
        //        for (int i = 0; i < len; i += 2)
        //        {
        //            data[i / 2] = (byte)((Character.digit(s.charAt(i), 16) << 4) + Character.digit(s.charAt(i + 1), 16));
        //        }
        //        return data;
        //    }

        //    public bool ValidateServerCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        //    {
        //        if (sslPolicyErrors == SslPolicyErrors.None)
        //            return true;

        //        // Do not allow this client to communicate with unauthenticated servers.
        //        return false;
        //    }
        //}
    }
}
