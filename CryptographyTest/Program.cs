using System;
using System.IO;
using System.Security.Cryptography;

//requires a usernername input to run. 



void GenKeys(string userName) {
	string saveDir = $".\\{userName}";
	
	byte[] key, iv;
	
	//makes the keys
	using (Aes aes = Aes.Create()) {
		key = aes.Key;
		iv = aes.IV;
	}
	Console.WriteLine("Keys generated, attempting to store...");
	
	//stores the keys
	//using (FileStream stream = File.OpenOrCreate($"{saveDir}")) {
		using (StreamWriter writer = new StreamWriter($"{saveDir}\\key.txt")) {
			writer.Write(key);
		}
	//}
	//using (FileStream stream = File.OpenOrCreate($"{saveDir}")) {
		using (StreamWriter writer = new StreamWriter($"{saveDir}\\IV.txt")) {
			writer.Write(iv);
		}
	//}
}


GenKeys("testUser");