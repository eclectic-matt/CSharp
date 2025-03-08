//IMPORT CODE FROM NAMESPACES (challenge)
using IPAddresses;
using ReformatMethod;

namespace Challenges;

class ChallengesOutput {

	//============================
	// CHALLENGE - REFORMAT TIMES
	//============================

	public static void MedicineTimes()
	{
		ReformatMedicineChallenge.ReformatTimes();
	}

	//============================
	// CHALLENGE - VALIDATE IPv4
	//============================
	public static void IPAddressValidation()
	{
		//Source: /challenges/02-ipAddresses.cs
		Console.WriteLine("Validate an IP Address:");
		string ipv4Input = "107.31.1.5";
		IPAddresses.IPValidator.ValidateIPV4Address(ipv4Input);

		string[] addresses = ["106.129.22.1", "191.229.239.22", "242.256.32.22"];
		foreach(string address in addresses){
			IPAddresses.IPValidator.ValidateIPV4Address(address);
		}

		string[] tests = {"107.31.1.5", "255.0.0.255", "555..0.555", "255...255"};
		foreach(string address in tests){
			IPAddresses.IPValidator.ValidateIPV4Address(address);
		}
	}
}