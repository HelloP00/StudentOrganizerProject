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


//hashes the string to the keys stored under the username
void HashPassword(string userName, string inputPassword) {
	string userDir = $".\\{userName}";
	int keySize = 32, IVSize = 16;
	
	byte[] key = new byte[keySize], iv = new byte[IVSize];
	
	using (Aes aes = Aes.Create()) {
		
		//loading the keys
		using (FileStream stream = File.Open($"{userDir}\\key.dat", FileMode.Open)) {
			using (BinaryReader reader = new BinaryReader(stream)) {
				reader.Read(key, 0, keySize);
				aes.Key = key;
			}
		}
		using (FileStream stream = File.Open($"{userDir}\\IV.dat", FileMode.Open)) {
			using (BinaryReader reader = new BinaryReader(stream)) {
				reader.Read(iv, 0, IVSize);
				aes.IV = iv;
			}
		}
	}
}

GenKeys("testUser");
HashPassword("testUser", "password");