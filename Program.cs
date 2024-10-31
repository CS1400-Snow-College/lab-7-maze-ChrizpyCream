// See https://aka.ms/new-console-template for more information
using System.Diagnostics;


string[] mazeRows = File.ReadAllLines("map.txt"); // reads maze layout from the file "map.txt"
Stopwatch stopwatch = new Stopwatch(); // stopwatch to track time taken to complete the maze


// cursor to the start of the maze at coordinates (0,0)
int x = 0, y = 0;
Console.SetCursorPosition(x, y); // initial cursor position
mainMenu(); // display the main menu and instructions


// display the maze on the console
for (int i = 0; i < mazeRows.Length; i++)
{
    Console.WriteLine(mazeRows[i]); // print each row of the maze
}


Console.SetCursorPosition(x, y); // starting position (0,0)


do
{
    // capture key input once per loop iteration
    var key = Console.ReadKey(true).Key;


    if (key == ConsoleKey.Escape) // restart the game if Escape is pressed
    {
        stopwatch.Stop();
        stopwatch.Reset();
        mainMenu(); // show main menu again
        printFileLines("map.txt"); // reload the maze layout
        x = 0;
        y = 0;
        Console.SetCursorPosition(x, y); // reset cursor to the starting position
    }
    else if (key == ConsoleKey.UpArrow && tryMove(x, y, "n", mazeRows)) // Move up
    {
        y--; // move the cursor up by decreasing the y-coordinate
    }
    else if (key == ConsoleKey.DownArrow && tryMove(x, y, "s", mazeRows)) // Move down
    {
        y++; // moves the cursor down by increasing the y-coordinate
    }
    else if (key == ConsoleKey.LeftArrow && tryMove(x, y, "w", mazeRows)) // Move left
    {
        if (x - 1 >= 0) x--; //the cursor stays within maze boundaries
    }
    else if (key == ConsoleKey.RightArrow && tryMove(x, y, "e", mazeRows)) // Move right
    {
        if (x + 1 < mazeRows[y].Length) x++; // ensure the cursor stays within maze boundaries
    }


    Console.SetCursorPosition(x, y); // update cursor position based on movement


} while (!(Console.CursorLeft == 27 && Console.CursorTop == 4)); // Exit loop when player reaches (27,4)


winMenu(); // display winning message when loop exits


// function to print each line of a file, used for reloading the maze
static void printFileLines(string filename)
{
    string[] fileLines = File.ReadAllLines(filename); // Read file contents
    foreach (string line in fileLines)
    {
        Console.WriteLine(line); // print each line to the console
    }
}


// a move in the specified direction is valid
static bool tryMove(int x, int y, string direction, string[] maze)
{
    return direction switch
    {
        "e" => (x + 1 < maze[y].Length && maze[y][x + 1] != '#'), //  right move is possible
        "w" => (x - 1 >= 0 && maze[y][x - 1] != '#'),             // left move is possible
        "s" => (y + 1 < maze.Length && maze[y + 1][x] != '#'),    //  down move is possible
        "n" => (y - 1 >= 0 && maze[y - 1][x] != '#'),             // up move is possible
        _ => false // Invalid direction returns false
    };
}


// main menu and starts the stopwatch
void mainMenu()
{
    Console.Clear();
    Console.WriteLine("This is a maze. Get to '*' to complete the maze.");
    Console.WriteLine("Press any key to start...");
    Console.ReadKey(true);
    Console.Clear();
    stopwatch.Start(); // Start tracking time
}


// winning message and final time
void winMenu()
{
    Console.Clear();
    Console.WriteLine("----------------------------------------------");
    Console.WriteLine("Congratulations! You made it through the maze!");
    Console.WriteLine("----------------------------------------------");
    stopwatch.Stop(); // Stop the timer
    Console.WriteLine($"Your time to complete the maze: {stopwatch.Elapsed}"); // elapsed time
}


