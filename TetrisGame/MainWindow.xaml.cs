﻿using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TetrisGame
{
    //code behind to control UI
    public partial class MainWindow : Window
    {
        //set up an array containing the tile images
        //the order is NOT random - at entry 0 we have an empty tile 
        //the order of the remaining tiles matches the block ids
        private readonly ImageSource[] tileImages = new ImageSource[]
        {
            new BitmapImage(new Uri("Assets/TileEmpty.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/TileCyan.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/TileBlue.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/TileOrange.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/TileYellow.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/TileGreen.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/TilePurple.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/TileRed.png", UriKind.Relative))
        };

        //similar array for the block images - to show the held block and the next block
        //again the order matches the ids
        private readonly ImageSource[] blockImages = new ImageSource[]
        {
            new BitmapImage(new Uri("Assets/Block-Empty.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Block-I.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Block-J.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Block-L.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Block-O.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Block-S.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Block-T.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Block-Z.png", UriKind.Relative))
        };
        //declare a 2-dimensional array of image controls
        private readonly Image[,] imageControls;
        //the idea is that there is one image control for every cell in the game grid

        //we need a game state object
        private GameState gameState = new GameState();

        //Method to set up the image controls correctly in the canvas 
        private Image[,] SetupGameCanvas(GameGrid grid)
        {
            //the image controls array will have 22 rows and 10 columns - just like the game grid
            Image[,] imageControls = new Image[grid.Rows, grid.Columns];
            //create a variable for the width and height of each cell
            int cellSize = 25;
            //recall that we set the canvas width to 250 and the canvas height to 500 
            //this gives us 25 pixels for each visible cell 
            //Loop through every row and column in the game grid 
            for (int r = 0; r < grid.Rows; r++)
            {
                for (int c = 0; c < grid.Columns; c++)
                {
                    //for each position we create a new image control with 25 pixels width and height 
                    Image imageControl = new Image
                    {
                        Width = cellSize,
                        Height = cellSize,
                    };
                    //then we have to position this image control correctly 
                    //recall that we count rows from top to bottom and columns from left to right
                    //so we set the distance from the top of the canvas to the top of the image equal to (r - 2) * cell size
                    Canvas.SetTop(imageControl, (r - 2) * cellSize);
                    //the (-2) is to push the top hidden rows up so they are NOT inside the canvas
                    //similarly the distance from the left side of the canvas to the left side of the image should be (c * cell size)
                    Canvas.SetLeft(imageControl, c * cellSize);
                    //next we make the image a chart of the canvas and add it to the array which will be returned outside the loop
                    GameCanvas.Children.Add(imageControl);
                    imageControls[r,c] = imageControl;
                }
            }
            return imageControls;
            //now we have a 2-dimensional array with one image for every cell in the game grid 
        }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void GameCanvas_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void PlayAgain_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}