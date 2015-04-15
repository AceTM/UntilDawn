public class Constants{
	public static float PLAYTIME = 60;

	// Because I don't like numbers
	static public int ZERO = 0;
	static public int ONE = 1;

	// CSV Related Constants
	static public string EXAMPLE_CSV = "CSV/CSVExample8";
	
	static public string SPLIT_RE = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";
	static public string LINE_SPLIT_RE = @"\r\n|\n\r|\n|\r";
	static public char[] TRIM_CHARS = { '\"' };

	// Messages Related Constants
	static public int MES_DISPLAY_COUNTER = 20;

	static public string MES_TEXT_BOX = "MessageBox";
	static public string MES_SCENE = "Scene";
	static public string MES_CHARACTER = "Character";
	static public string MES_SKIPABLE = "Skipable";
	static public string MES_VOICED = "Voiced";
	static public string MES_JPTEXT = "JPText";
	static public string MES_ENTEXT = "ENText";
}
