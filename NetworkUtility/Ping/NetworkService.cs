using NetworkUtility.DNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace NetworkUtility.Ping
{
    public class NetworkService
    {
        private string _ipAddress;
        private readonly IDNS _dNS;

        public NetworkService(IDNS dNS)
        {
            _ipAddress = string.Empty;
            this._dNS = dNS;
        }

        public string SendPing()
        {
            var dnsSuccess = _dNS.SendDNS();
            if (dnsSuccess)
            {
                return "Success: ping sent";
            }
            else
            {
                return "Failed: ping not sent";
            }
            
        }

        public bool PingHost(string nameOrAddress)
        {

            bool pingable = false;
            System.Net.NetworkInformation.Ping? pinger = null;

            try
            {
                pinger = new System.Net.NetworkInformation.Ping();
                PingReply reply = pinger.Send(nameOrAddress);
                pingable = reply.Status == IPStatus.Success;
            }
            catch(PingException)
            {
                // Discard PingExceptions and return false;
            }
            finally
            {
                if(pinger != null)
                {
                    pinger.Dispose();
                }
            }

            return pingable;
        }

        public string myIP()
        {
            string hostName = System.Net.Dns.GetHostName();
            this._ipAddress = string.Empty;
            IPAddress[] addresses = System.Net.Dns.GetHostAddresses(hostName);
            foreach(IPAddress address in addresses)
            {
                // Filtrar solo las direcciones IPv4
                if(address.AddressFamily == AddressFamily.InterNetwork)
                {
                    //Console.WriteLine("IPv4 Address: " + address.ToString());
                    this._ipAddress = address.ToString();
                }
            }
            return this._ipAddress;
        }

        public string setIP(string part1, string part2) {

            return part1 + part2;
        }

        public DateTime LastPingDate()
        {

            return DateTime.Today;
        }

        public PingOptions PingOptions()
        {
            PingOptions pingOptions = new PingOptions();
            pingOptions.DontFragment = true;
            pingOptions.Ttl = 64;
            return pingOptions;
        }

        public IEnumerable<PingOptions> MostRecentPings()
        {
            IEnumerable<PingOptions> pingOptions = new []
            {
                new PingOptions() { DontFragment = true, Ttl = 64 },
                new PingOptions() { DontFragment = false, Ttl = 128 },
                new PingOptions() { DontFragment = true, Ttl = 64 }
            };
            return pingOptions;
        }
    }
}
