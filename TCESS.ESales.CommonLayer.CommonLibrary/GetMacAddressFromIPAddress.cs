using System;
using System.Diagnostics;
using System.Net;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Text;

namespace TCESS.ESales.CommonLayer.CommonLibrary
{
	// *************************************************************************
	/// <summary>
	/// A class to get the MAC address from IP address. The same class can be
	/// modified a little to get the MAC address from the specified hostname.
	/// </summary>
	public class GetMacAddressFromIPAddress
	{
		/// <summary> Ping timeout (in ms) </summary>
		private const int PING_TIMEOUT = 1000;

		// *********************************************************************
		/// <summary>
		/// Initializes a new instance of <see cref="GetMacAddressFromIPAddress"/>.
		/// </summary>
		public GetMacAddressFromIPAddress()
		{

		}

		// *********************************************************************
		/// <summary>
		/// Gets the MAC address from specified <paramref name="IPAddress"/>
		/// using nbtstat in hyphen (-) separated format.
		/// </summary>
		/// <remarks>
		/// <para>
		/// The same class can be modified to accept hostname and then resolve
		/// the hostname to Ip address to get the MAC address or just pass "-a"
		/// argument to "nbtstat" to get the mac address from hostname. If you
		/// want to find the MAC address from only the IP address change the
		/// switch to "-A".
		/// </para>
		/// <para>
		/// The current program can resolve both hostname as well as IP address
		/// to MAC address. The "-a" argument can actually accept both IP address
		/// as well as hostname.
		/// </para>
		/// </remarks>
		/// <param name="ipAddress">The IP address or hostname for the machine
		/// for which the MAC address is desired.</param>
		/// <returns>A string containing the MAC address if MAC address could be
		/// found.An empty or null string otherwise.</returns>
		public string GetMacAddress(string ipAddress)
		{
			string macAddress = string.Empty;


			try
			{
				ProcessStartInfo processStartInfo = new ProcessStartInfo();
				Process process = new Process();
				processStartInfo.FileName = "nbtstat";
				processStartInfo.RedirectStandardInput = false;
				processStartInfo.RedirectStandardOutput = true;
				processStartInfo.Arguments = "-a " + ipAddress;
				processStartInfo.UseShellExecute = false;
				process = Process.Start(processStartInfo);

				int Counter = -1;

				while (Counter <= -1)
				{
					// Look for the words "mac address" in the output.
					// The output usually looks likes this:

					// Local Area Connection:
					//Node IpAddress: [13.15.111.222] Scope Id: []

					//           NetBIOS Remote Machine Name Table

					//       Name               Type        Status
					//    --------------------------------------------
					//    SAMGLS0790W    <00>  UNIQUE      Registered
					//    SAMPLS0790W    <20>  UNIQUE      Registered
					//    NA            <00>  GROUP       Registered

					//    MAC Address = 00-21-70-2B-A5-43

					Counter =
						macAddress.Trim().ToLower().IndexOf("mac address", 0);
					if (Counter > -1)
					{
						break;
					}
					macAddress = process.StandardOutput.ReadLine();
				}
				process.WaitForExit();
				macAddress = macAddress.Trim();
			}
			catch (Exception e)
			{
				// Something unexpected happened? Inform the user
				// The possibilities are:
				// 1.That the machine is not on the network currently
				// 2. The IP address/hostname supplied are not on the same network
				// 3. The host was not found on the same subnet or could not be
				//    resolved
				Console.WriteLine("Failed because:" + e.ToString());
			}

			return macAddress;
		}

		#region Getting MAC from ARP

		[DllImport("iphlpapi.dll", ExactSpelling = true)]
		static extern int SendARP(int DestIP, int SrcIP, byte[] pMacAddr,
			ref uint PhyAddrLen);

		// *********************************************************************
		/// <summary>
		/// Gets the MAC address from ARP table in colon (:) separated format.
		/// </summary>
		/// <param name="hostNameOrAddress">Host name or IP address of the
		/// remote host for which MAC address is desired.</param>
		/// <returns>A string containing MAC address; null if MAC address could
		/// not be found.</returns>
		public string GetMACAddressFromARP(string hostNameOrAddress)
		{

			try
			{

				IPHostEntry hostEntry = Dns.GetHostEntry(hostNameOrAddress);
				if (hostEntry.AddressList.Length == 0)
					return null;

				byte[] macAddr = new byte[6];
				uint macAddrLen = (uint)macAddr.Length;

				long hostAddress = 0;
				foreach (IPAddress address in hostEntry.AddressList)
				{
					if (address.Address != null)
					{
						hostAddress = address.Address;
					}
				}

				if (SendARP((int)hostAddress, 0, macAddr, ref macAddrLen) != 0)
					return null;

				StringBuilder macAddressString = new StringBuilder();
				for (int i = 0; i < macAddr.Length; i++)
				{
					if (macAddressString.Length > 0)
						macAddressString.Append(":");

					macAddressString.AppendFormat("{0:x2}", macAddr[i]);
				}

				return macAddressString.ToString();
			}
			catch (Exception ex)
			{
				return null;
			}
		} // end GetMACAddressFromARP

		#endregion Getting MAC from ARP

	} // end class GetMacAddressFromIPAddress
}