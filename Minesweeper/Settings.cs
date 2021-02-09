using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Minesweeper
{
    public class Settings
    {
        [Serializable]
        struct SettingsFile
        {
            public HighScore easyTime;
            public HighScore mediumTime;
            public HighScore advancedTime;
            public int scale;
            public GameModes mode;
            public GameMode advanced;
        }

        const string filename = "minesweeper.dat";
        SettingsFile settingsFile;

        public Settings ()
        {
            Load();
        }

        public int getScale() { return settingsFile.scale; }
        public GameModes getGameMode() { return settingsFile.mode; }

        public void SetGameMode(GameModes gameMode)
        {
            settingsFile.mode = gameMode;
            Save();
        }
        public void updateScore(string level, HighScore hs)
        {
            switch (level)
            {
                case "easy":
                    settingsFile.easyTime  = hs;
                    break;
                case "medium":
                    settingsFile.mediumTime = hs;
                    break;
                case "advanced":
                    settingsFile.advancedTime = hs;
                    break;
            }
        }

        public HighScore[] GetScores()
        {
            HighScore[] scores = {settingsFile.easyTime, settingsFile.mediumTime, settingsFile.advancedTime};

            return scores;
        }

        public HighScore GetScore(GameModes mode)
        {
            HighScore score = null;
            switch(mode)
            {
                case GameModes.EASY:
                    score = settingsFile.easyTime;
                    break;
                case GameModes.INTERMEDIATE:
                    score = settingsFile.mediumTime;
                    break;
                case GameModes.ADVANCED:
                    score = settingsFile.advancedTime;
                    break;
            }

            return score;
        }

        public void SetScore(GameModes mode, int time, string name)
        {
            switch (mode)
            {
                case GameModes.EASY:
                    settingsFile.easyTime = new HighScore(time, name);
                    break;
                case GameModes.INTERMEDIATE:
                    settingsFile.mediumTime = new HighScore(time, name);
                    break;
                case GameModes.ADVANCED:
                    settingsFile.advancedTime = new HighScore(time, name);
                    break;
            }

            Save();
        }

        public void Load()
        {
            FileStream fs;
            try
            {
                fs = new FileStream(filename, FileMode.Open);

                try
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    settingsFile = (SettingsFile)bf.Deserialize(fs);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                fs.Close();
            }
            catch (FileNotFoundException f)
            {
                settingsFile = new SettingsFile();
                
                settingsFile.easyTime = new HighScore(999, "Anonymous");
                settingsFile.mediumTime = new HighScore(999, "Anonymous");
                settingsFile.advancedTime = new HighScore(999, "Anonymous");
                settingsFile.scale = 2;
                settingsFile.mode = GameModes.EASY;
                // settingsFile.advanced = ;
                Save();
                return;
            }            
        }

        public void Save()
        {
            FileStream fs = new FileStream(filename, FileMode.Create);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(fs, settingsFile);
            fs.Close();
        }

        public void Reset()
        {
            settingsFile.easyTime = new HighScore(999, "Anonymous");
            settingsFile.mediumTime = new HighScore(999, "Anonymous");
            settingsFile.advancedTime = new HighScore(999, "Anonymous");

            Save();
        }
    }
}
