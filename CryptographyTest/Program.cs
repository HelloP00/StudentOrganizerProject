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
	//stores the keys
	using (FileStream stream = File.Create($"{saveDir}\\key.dat")) {
		using (BinaryWriter writer = new BinaryWriter(stream)) {
			writer.Write(key);
		}
	}
	using (FileStream stream = File.Create($"{saveDir}\\IV.dat")) {
		using (BinaryWriter writer = new BinaryWriter(stream)) {
			writer.Write(iv);
		}
	}
}


//GenKeys("testUser");