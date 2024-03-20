using SharpDX.DirectWrite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeteorManeuver
{
    public class ReadWriteFile
    {
        // Initialize the path to the folder:
        private static string projectPathway = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
        private static string folderPathway = System.IO.Path.Combine(projectPathway, "MeteorManeuver", "HiScores");

        // Initialize Lists used to maintain hi scores:
        List<string> easyHiScore;
        List<string> mediumHiScore;
        List<string> hardHiScore;

        // Initialize FileName Constants:
        private const string easyFile = "EasyHiScores.txt";
        private const string mediumFile = "MediumHiScores.txt";
        private const string hardFile = "HardHiScores.txt";

        // Initialize Array for Witting to New Files:
        string[] difficulties = { "Easy", "Medium", "Hard" };

        public ReadWriteFile() 
        {
            // Check if Folder/File Exists:
            CheckAndCreateFile();

            // initialize and fill list with data:
            easyHiScore = GetListData(easyFile);
            mediumHiScore = GetListData(mediumFile);
            hardHiScore = GetListData(hardFile);

        }

        /// <summary>
        /// Method checks if file exists, and creates it (and seeds it) if it does not exist:
        /// </summary>
        private void CheckAndCreateFile()
        {
            // Check if directory exists:
            if (!Directory.Exists(folderPathway))
            {
                // If not, create the directory (MeteorManeuver/HiScores):
                Directory.CreateDirectory(folderPathway);
            }

            // Initialize array of fileNames:
            string[] hiScoreFiles = { easyFile, mediumFile, hardFile };
            
            // Initialize iterator:
            int i = 0;

            // Iteratr though array:
            foreach (string s in hiScoreFiles)
            {
                // Create a filepath using each of the file names:
                string filePath = Path.Combine(folderPathway, s);

                // Check if the file exists:
                if(!File.Exists(filePath))
                {
                    // If not, create file, write text, and close file:
                    File.Create(filePath).Close();
                    File.WriteAllText(filePath, difficulties[i]);
                }
                // Increment I:
                i++;
            }
        }

        /// <summary>
        /// Add New HiScore will add a high score to the designated difficulty
        /// </summary>
        /// <param name="difficulty">0, 1 or 2, Contains Difficulty Selection</param>
        /// <param name="name">Name of the user that attained high score</param>
        /// <param name="score">The Total score count</param>
        public void AddNewHiScore(int difficulty, string name, int score)
        {
            // Check difficulty:
            if(difficulty == 0)
            {
                // Add to list:
                easyHiScore.Add($"{name},{score}");
                // order list:
                easyHiScore = OrderListData(easyHiScore);
                // Update File:
                UpdateFileContents(Path.Combine(folderPathway, easyFile), easyHiScore);
            }
            else if(difficulty == 1)
            {
                // Add to list:
                mediumHiScore.Add($"{name},{score}");
                // order list:
                mediumHiScore = OrderListData(mediumHiScore);
                // Update File:
                UpdateFileContents(Path.Combine(folderPathway, mediumFile), mediumHiScore);

            }
            else if( difficulty == 2)
            {
                // Add to list:
                hardHiScore.Add($"{name},{score}");
                // order list:
                hardHiScore = OrderListData(hardHiScore);
                // Update File:
                UpdateFileContents(Path.Combine(folderPathway, hardFile), hardHiScore);

            }
        }
            
        /// <summary>
        /// Gets the data found within the specified file:
        /// </summary>
        /// <param name="fileName">name of the file</param>
        /// <returns>returns a string list containing the data</returns>
        public List<string> GetListData(string fileName)
        {
            // Create Relative Path to Folder:
            string pathway = System.IO.Path.Combine(folderPathway, fileName);
            
            // Initialize a new StreamReader, Read Contents:
            StreamReader reader = new StreamReader(pathway);

            List<string> tempList = new List<string>();

            // Read File Contents, add to TempList (removing empty entries):
            foreach (string line in reader.ReadToEnd().Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries))
            {
                tempList.Add(line); 
            }

            reader.Close();

            // Then, send data to be ordered appropriately:
            if (tempList.Count > 1) 
            {
                List<string> orderedList = OrderListData(tempList);
                return orderedList;
            }
            else
            {
                return tempList;
            }
        }

        /// <summary>
        /// Gets a list of data, and orderes it based on score count
        /// </summary>
        /// <param name="tempData">The list to be ordered</param>
        /// <returns>Returns the same list, but ordered</returns>
        public List<string> OrderListData(List<string> tempData)
        {
            // Initialize Two lists:
            List<int> scores = new List<int>();
            List<string> names = new List<string>();

            // Skipping the first index, split each subsequent line into a stringarray
            for (int i = 1; i < tempData.Count; i++)
            {
                // Split the line into two via comma:
                string[] parts = tempData[i].Split(',');
                // Add parts to their prospective array:
                names.Add(parts[0]);
                scores.Add(int.Parse(parts[1]));
            }

            // Order the scores from high to low:
            for (int i = 0; i < scores.Count; i++)
            {
                // Second loop is initialized as 1 above 'i':
                for (int j = i + 1; j < scores.Count; j++)
                {
                    // Check and swap scores if 'j' is greater:
                    if (scores[j] > scores[i])
                    {
                        // Swap Scores:
                        int tempScore = scores[i];
                        scores[i] = scores[j];
                        scores[j] = tempScore;

                        // Swap the Corresponding Name:
                        string tempName = names[i];
                        names[i] = names[j];
                        names[j] = tempName;
                    }
                }
            }

            // Finally, Create a new list and reassign the first line:
            List<string> orderedList = new List<string> { tempData[0] };


            // Loop through the two lists, reassign variables:
            for (int i = 0; i < scores.Count; i++)
            {
                orderedList.Add($"{names[i]},{scores[i]}");
            }

            // Check if List contains more than 6 entries:
            if (orderedList.Count > 6)
            {
                for (int i = 6; i < orderedList.Count; i++)
                {
                    // Remove each element after the 6th index:
                    orderedList.RemoveAt(i);
                }
            }

            return orderedList;
        }

        /// <summary>
        /// Updates the fileContents based on parameters passed
        /// </summary>
        /// <param name="fileName">The Name of the File</param>
        /// <param name="updatedData">The New Data to be written over the existing data</param>
        private void UpdateFileContents(string fileName, List<string> updatedData)
        {
            // Initialize new StreamWriter, Set 'Append' value to 'false':
            using(StreamWriter writer =  new StreamWriter(fileName, false)) 
            {
                foreach(string index in updatedData)
                {
                    writer.WriteLine(index);
                }
            }
        }

    }
}
