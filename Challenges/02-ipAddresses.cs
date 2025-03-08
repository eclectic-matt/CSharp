/*
if ipAddress consists of 4 numbers
and
if each ipAddress number has no leading zeroes
and
if each ipAddress number is in range 0 - 255

then ipAddress is valid

else ipAddress is invalid


void ValidateIPV4Address(string address)
{
	if (ValidateLength() && ValidateZeroes() && ValidateRange()) 
	{
		Console.WriteLine($"ip is a valid IPv4 address");
	} 
	else 
	{
		Console.WriteLine($"ip is an invalid IPv4 address");
	}
}

bool ValidateLength(string ip)
{
	string[] address = ip.Split(".");
	if(address.Length == 4){
		return true;
	}else{
		return false;
	}
}

bool ValidateZeroes(string ip)
{
	string[] address = ip.Split(".");
	foreach(string number in address){
		if(number.Length > 1 && number.StartsWith("0")){
			return false;
		}
	}
	return true;
}

bool ValidateRange(string ip)
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

string ipv4Input = "107.31.1.5";
ValidateIPV4Address(ipv4Input);

*/