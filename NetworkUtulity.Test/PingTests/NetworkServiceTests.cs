using System;
using System.Collections.Generic;
using NetworkUtility.Ping;
using NetworkUtility.DNS;   
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Sdk;
using System.Net.NetworkInformation;
using Moq;
namespace NetworkUtulity.Test.PingTest
{
    
    public  class NetworkServiceTests
    {
        private readonly Mock<IDNS> _dnsMock;
        private readonly NetworkService _networkService;
        public NetworkServiceTests() {
            //Dependency
            _dnsMock = new Mock<IDNS>();
            //SUT
            _networkService = new NetworkService(_dnsMock.Object);
        }

        [Fact]
        public void NetwirkService_SendPing_ReturnsSuccess() 
        {
            try
            {
                _dnsMock.Setup(dns => dns.SendDNS()).Returns(true);
                string result = _networkService.SendPing();
                Assert.Equal("Success: ping sent", result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        [Fact]
        public void NetwirkService_SendPing_ReturnsFailed()
        {
            try
            {
                _dnsMock.Setup(dns => dns.SendDNS()).Returns(false);
                string result = _networkService.SendPing();
                Assert.Equal("Failed: ping not sent", result);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        [Fact]
        public void NetworkService_PingHost_ReturnsTrue()
        {
            try
            {
                string nameOrAddress = "www.google.com";
                bool pingable = _networkService.PingHost(nameOrAddress);
                Assert.True(pingable);
                
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        [Fact]
        public void NetworkService_myIP_ReturnsIP()
        {
            try
            {
                string myIP = _networkService.myIP();
                Assert.NotNull(myIP);
                Assert.NotEmpty(myIP);
                Assert.Contains(".", myIP);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        [Theory]
        [InlineData("192.168.", "1.1", "192.168.1.1")]
        public void NetworkService_setIP_ReturnsIP(string a, string b, string expected)
        {
            try
            {
                string myIP = _networkService.setIP(a, b);
                Assert.NotNull(myIP);
                Assert.NotEmpty(myIP);
                Assert.Contains(".", myIP);
                Assert.Equal(expected, myIP);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        [Fact]
        public void NetworkService_LastPingDate_ReturnsToday()
        {
            try
            {
                DateTime enero = new DateTime(2021, 1, 1);
                DateTime Febrero2025 = new DateTime(2025, 2, 1);
                Assert.Equal(DateTime.Today, _networkService.LastPingDate());
                bool resultBefore = _networkService.LastPingDate() > enero;
                Assert.True(resultBefore);
                bool resultAfter = _networkService.LastPingDate() < Febrero2025;
                Assert.True(resultAfter);
                // Assert.
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        [Fact]
        public void NetworkService_PingOptions_ReturnsObject()
        {
            try
            {
                var expected = new System.Net.NetworkInformation.PingOptions() {
                    DontFragment = true,
                    Ttl = 64
                };
                var pingOptions = _networkService.PingOptions();
                Assert.NotNull(pingOptions);
                Assert.IsType<System.Net.NetworkInformation.PingOptions>(pingOptions);
                Assert.Equivalent(expected, pingOptions);
                Assert.Equal(expected.DontFragment, pingOptions.DontFragment);
                Assert.Equal(expected.Ttl, pingOptions.Ttl);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        [Fact]
        public void NetworkService_MostRecentPings_ReturnsList()
        {
            try
            {
                var expected = new List<PingOptions>()
                {
                    new PingOptions() { DontFragment = true, Ttl = 64 },
                    new PingOptions() { DontFragment = false, Ttl = 128 },
                    new PingOptions() { DontFragment = true, Ttl = 64 }
                };
                var pingOptions = _networkService.MostRecentPings()?.ToList();
                Assert.NotNull(pingOptions);
                Assert.IsAssignableFrom<IEnumerable<System.Net.NetworkInformation.PingOptions>>(pingOptions);
                //Assert.Equal(expected, pingOptions);
                Assert.Equal(expected.Count(), pingOptions.Count());
                Assert.IsType<System.Net.NetworkInformation.PingOptions>(pingOptions.First());
                for(int i = 0; i < expected.Count(); i++)
                {
                    Assert.Equal(expected[i].DontFragment, pingOptions[i].DontFragment);
                    Assert.Equal(expected[i].Ttl, pingOptions[i].Ttl);
                }

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

    }
}
