String activeTheme = "default";
Dictionary<String, Dictionary<String, Color>> themesDictionary;

//current themes
Dictionary<String, Color> defaultColours;


//contents of the themes. 
defaultColours = new Dictionary<string, Color>()
	{
		
		{ "primary1", Color.FromArgb(255,255,255) },
		{ "primary2", Color.FromArgb(240,240,240) },
		{ "primary3", Color.FromArgb(225,225,225) },
			
	};


//adds the themes to the themesDictionary
themesDictionary = new Dictionary<string, Dictionary<string, Color>>()
{
	
	{"default", defaultColours },
	
};

Color getColour(String colour)
{
	//check if the theme exists. 
	if (themesDictionary.ContainsKey(activeTheme))
	{
		//check if the requested colour exists
		if (themesDictionary[activeTheme].ContainsKey(colour))
		{
			return themesDictionary[activeTheme][colour];
		}
		Console.Write($"Requested colour \"{colour}\" in \"{activeTheme}\" does not exist\n");
		return Color.FromArgb(0, 0, 0);
	}
	Console.Write($"Requested dictionary \"{activeTheme}\" is not present\n");
	return Color.FromArgb(0, 0, 0);
}