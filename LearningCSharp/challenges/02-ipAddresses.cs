/*
if ipAddress consists of 4 numbers
and
if each ipAddress number has no leading zeroes
and
if each ipAddress number is in range 0 - 255

then ipAddress is valid

else ipAddress is invalid
*/

using System;

namespace IPAddresses
{
	class IPValidator 
	{
		public static void ValidateIPV4Address(string address)
		{
			if (ValidateLength(address) && ValidateZeroes(address) && ValidateRange(address)) 
			{
				Console.WriteLine($"ip {address} is a valid IPv4 address");
			} 
			else 
			{
				Console.WriteLine($"ip {address} is an invalid IPv4 address");
			}
		}

		public static bool ValidateLength(string ip)
		{
			string[] address = ip.Split(".");
			if(address.Length == 4){
				return true;
			}else{
				return false;
			}
		}

		public static bool ValidateZeroes(string ip)
		{
			string[] address = ip.Split(".");
			foreach(string number in address){
				if(number.Length > 1 && number.StartsWith("0")){
					return false;
				}
			}
			return true;
		}

		public static bool ValidateRange(string ip)
		{
			string[] address = ip.Split(".", StringSplitOptions.RemoveEmptyEntries);
			foreach(string number in address){
				int value = int.Parse(number);
				if(value < 0 || value > 255){
					return false;
				}
			}
			return true;
		}
	}
}