using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

//the directory that will contain the directories containing the user data
string baseDir = ".\\";



void GenKeys(string userName) {
	string userDir = $"{baseDir}\\{userName}";
	
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
//returns the hashed input in the form of a byte array. 
byte[] HashPassword(string userName, string inputPassword) {
	string userDir = $"{baseDir}\\{userName}";
	int keySize = 32, IVSize = 16;
	
	byte[] key = new byte[keySize], iv = new byte[IVSize], inputArray, hashedArray;
	
	//makes the inputPassword a byte array. 
	inputArray = UTF8Encoding.UTF8.GetBytes(inputPassword);
	
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
		
		using (ICryptoTransform encryptor = aes.CreateEncryptor()) {
			hashedArray = encryptor.TransformFinalBlock(inputArray, 0, inputArray.Length);
		}
		
		return hashedArray;
		
	}
}

//hashes the input password, and then saves the hashed password. 
void SavePassword(string userName, string inputPassword) {
	string userDir = $"{baseDir}\\{userName}";
	byte[] hashedPassword;
	
	hashedPassword = HashPassword(userName, inputPassword);
	
	//saves the password. 
	using (FileStream stream = File.Create($"{userDir}\\pass.dat")) {
		using (BinaryWriter writer = new BinaryWriter(stream)) {
			writer.Write(hashedPassword);
		}
	}
	
	//Console.WriteLine($"Hash Result: {UTF8Encoding.UTF8.GetString(hashedPassword)}");
	
	return;
		
}

bool CompareToStoredPassword(string userName, string inputPassword) {
	string userDir = $"{baseDir}\\{userName}";
	byte[] inputHash, storedHash; 
	
	
	if (!File.Exists($"{userDir}\\pass.dat")) {
		Console.WriteLine($"There is no password stored for {userName}, (CompareToStoredPassword has been called to compare to a stored password)");
		return false;
	}
	
	//loads the stored password
	using (FileStream stream = File.Open($"{userDir}\\pass.dat", FileMode.Open)) {
		using (BinaryReader reader = new BinaryReader(stream)) {
			storedHash = new byte[stream.Length];
			reader.Read(storedHash, 0, Convert.ToInt32(stream.Length));
		}
	}
	
	//hashes the input string
	inputHash = HashPassword(userName, inputPassword);
	
	//compares the inputs
	if (inputHash.SequenceEqual(storedHash)) {
		return true;
	}
	
	return false;
}

//Test function calls
GenKeys("testUser");
HashPassword("testUser", "password");
HashPassword("testUser", "password");
SavePassword("testUser", "password");
Console.WriteLine(CompareToStoredPassword("testUser", "password"));
Console.WriteLine(CompareToStoredPassword("testUser", "passwod"));