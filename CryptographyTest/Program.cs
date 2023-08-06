using System;
using System.IO;
using System.Security.Cryptography;

//requires a usernername input to run. 



void GenKeys(string userName) {
	string userDir = $".\\{userName}";
	
	byte[] key, iv;
	
	//makes the keys
	using (Aes aes = Aes.Create()) {
		key = aes.Key;
		iv = aes.IV;
	}
	//stores the keys (if a file is not already present)
	if (!File.Exists($"{userDir}\\key.dat")) {
		using (FileStream stream = File.Create($"{userDir}\\key.dat")) {
			using (BinaryWriter writer = new BinaryWriter(stream)) {
				writer.Write(key);
			}
		}
	}
	if (!File.Exists($"{userDir}\\IV.dat")) {
		using (FileStream stream = File.Create($"{userDir}\\IV.dat")) {
			using (BinaryWriter writer = new BinaryWriter(stream)) {
				writer.Write(iv);
			}
		}
	}
}

/*
//hashes the string to the keys stored under the username
void HashPassword(string userName) {
	string userDir = $".\\{userName}";
	
	byte[] key, iv;
	
	using (Aes aes = Aes.Create) {
		
		
		using (FileStream stream = File.Open($"{userDir}\\key.dat")) {
			using (BinaryReader reader = new BinaryReader(stream)) {
				
			}
		}
		
	}
}
*/
GenKeys("testUser");