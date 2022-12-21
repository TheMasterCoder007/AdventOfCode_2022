using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2022;

public static class AocDay7
{
    public static void LowDeviceSpace()
    {
        // pull input into string and then split the string based on delimiter
        string input = System.IO.File.ReadAllText(@"..\..\..\inputs\input7.txt");
        string[] inputSplit = input.Split("\r\n");
        
        // create variables for containing and parsing file structure
        FileStruct root = new FileStruct("root", null!);
        FileStruct[] subdirectory = new FileStruct[200];
        FileStruct currentDir = root;
        int currentSubdirectory = 0;
        bool listDirectory = false;

        // parse folder and file structure
        foreach (string line in inputSplit)
        {
            
            // if command is found, the list of the folder contents has ended
            if (line.Contains('$'))
            {
                listDirectory = false;

                // list directory command found
                if (line.Contains("$ ls"))
                {
                    listDirectory = true;
                }
                // if back directory command is found
                else if (line.Contains("cd .."))
                {
                    currentDir = currentDir.Parent;
                }
                // change directory command
                else
                {
                    string[] directory = line.Split(' ');

                    // root directory
                    if (directory[2] == "/")
                    {
                        currentDir = root;
                    }
                    // all other subdirectories
                    else
                    {
                        // find subdirectory 
                        foreach (FileStruct subDir in currentDir.Subdirectories)
                        {
                            if (subDir.Name == directory[2])
                            {
                                currentDir = subDir;
                                break;
                            }
                        }
                    }
                }
            }
            else if (listDirectory)
            {
                if (line.Contains("dir"))
                {
                    string[] directory = line.Split(' ');
                    
                    // initialize and add subdirectory
                    subdirectory[currentSubdirectory] = new FileStruct(directory[1], currentDir);
                    currentDir.Subdirectories.Add(subdirectory[currentSubdirectory]);
                    currentSubdirectory++;
                }
                else
                {
                    string[] fileSize = line.Split(' ');

                    // add file size to FileSize list
                    currentDir.FileSize.Add(Convert.ToInt32(fileSize[0]));
                }
            }
        }

        // create a list of all the folders and subfolders in root
        List<FileStruct> folders = GetAllFolders(root)
            .ToList();
        
        //********//
        // part 1 //
        //********//
        {
            // find the sum of all folders less then 100k
            int folderSize = folders
                .Select(TotalFolderSize)
                .Where(x => (x <= 100000))
                .Sum();

            // write the answer to the console
            Console.WriteLine("The sum of the directories with a total size of at most 100000 is " + folderSize);   
        }
        
        //********//
        // part 2 //
        //********//
        {
            // add root to a list so it can by used by Linq to create a query
            List<FileStruct> rootSize = new List<FileStruct>();
            rootSize.Add(root);
            
            // find the total amount of free space and calculate how much needs to be freed
            int totalUsedSpace = rootSize
                .Select(TotalFolderSize)
                .Sum();
            int freeSpace = 70_000_000 - totalUsedSpace;
            int freeSpaceNeeded = 30_000_000 - freeSpace;
            
            // find the smallest folder that needs to be deleted, and get its size
            int folderSize = folders
                .Select(TotalFolderSize)
                .OrderBy(x => Math.Abs(x - freeSpaceNeeded))
                .FirstOrDefault();

            // write the answer to the console
            Console.WriteLine("The smallest file that will free up enough space has a size of " + folderSize);
        }
    }

    private static IEnumerable<T> Traverse<T>(this IEnumerable<T> source, Func<T, IEnumerable<T>> childrenSelector)
    {
        foreach (T item in source)
        {
            yield return item;

            IEnumerable<T> children = childrenSelector(item);
            foreach (T child in Traverse(children, childrenSelector))
            {
                yield return child;
            }
        }
    }
    
    private static IEnumerable<FileStruct> GetAllFolders(FileStruct root)
    {
        return root.Subdirectories
            .Traverse(x => x.Subdirectories);
    }
    
    private static int TotalFolderSize(FileStruct folder)
    {
        int totalSize = folder.FileSize.Sum();

        foreach (var subdirectory in folder.Subdirectories)
        {
            totalSize += TotalFolderSize(subdirectory);
        }

        return totalSize;
    }

}

public class FileStruct
{
    public string Name { get; set; }
    public FileStruct Parent { get; set; } 
    public List<int> FileSize { get; set; }
    public List<FileStruct> Subdirectories { get; set; }

    public FileStruct(string name, FileStruct parent)
    {
        Name = name;
        Parent = parent;
        FileSize = new List<int>();
        Subdirectories = new List<FileStruct>();
    }
}
