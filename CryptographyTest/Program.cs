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
	//stores the keys (if a file is not already present)
	if (!File.Exists($"{saveDir}\\key.dat")) {
		using (FileStream stream = File.Create($"{saveDir}\\key.dat")) {
			using (BinaryWriter writer = new BinaryWriter(stream)) {
				writer.Write(key);
			}
		}
	}
	if (!File.Exists($"{saveDir}\\IV.dat")) {
		using (FileStream stream = File.Create($"{saveDir}\\IV.dat")) {
			using (BinaryWriter writer = new BinaryWriter(stream)) {
				writer.Write(iv);
			}
		}
	}
}

//hashes the string to the keys stored under the username
void HashPassword(string userName) {
	
}

GenKeys("testUser");