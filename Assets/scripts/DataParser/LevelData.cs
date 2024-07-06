

public class LevelData{
    public string levelName;
    public string levelCode;
    public string cityName;
    public LevelbuttonManager.LevelButtonState state;
    public int stars;
    public string description;

    public LevelData(string levelName, string levelCode,string cityName,int strs, LevelbuttonManager.LevelButtonState state, string description = ""){
        this.levelName = levelName;
        this.levelCode = levelCode;
        this.cityName = cityName;
        this.state = state;
        this.stars = strs;
        this.description = description;
    }

    public static LevelData DefaultSandBox(string levelName,string cityName, string levelCode, int stars){
        return new LevelData(levelName, levelCode, cityName,stars, LevelbuttonManager.LevelButtonState.SandBox);
    }

    public static LevelData DefaultLevel(string levelName,string cityName, string levelCode, int stars, string description){
        return new LevelData(levelName, levelCode, cityName,stars, LevelbuttonManager.LevelButtonState.Unlocked, description);
    }

    public static LevelData DefaultCompleted(string levelName,string cityName, string levelCode, int stars, string description){
        return new LevelData(levelName, levelCode, cityName,stars, LevelbuttonManager.LevelButtonState.Completed, description);
    }
}