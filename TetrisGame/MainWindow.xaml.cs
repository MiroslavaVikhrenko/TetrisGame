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

        //add a few constants to control the delay between moving the block down 
        private readonly int maxDelay = 1000;
        private readonly int minDelay = 75;
        private readonly int delayDecrease = 25;

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

                    //when we position the images vertically we will add 10 pixels

                    Canvas.SetTop(imageControl, (r - 2) * cellSize + 10);
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
            //the 2 top rows which were used for spawning are placed above the canvas => so they are hidden
        }

        public MainWindow()
        {
            InitializeComponent();
            //initiallize an image control array by calling SetupGameCanvas method
            imageControls = SetupGameCanvas(gameState.GameGrid);
        }

        //Method to draw a game grid
        private void DrawGrid(GameGrid grid)
        {
            //Loop through all positions 
            for (int r = 0;r < grid.Rows; r++)
            {
                for (int c = 0;c < grid.Columns; c++)
                {
                    //for each position we get the start id 
                    int id = grid[r,c];
                    //reset opacity we set for ghost block
                    imageControls[r,c].Opacity = 1;
                    //set the source of the image at this position using the id
                    imageControls[r,c].Source = tileImages[id];
                }
            }
        }

        //Method to draw the current block
        private void DrawBlock(Block block)
        {
            //Loop through the tile positions and update the image sources in the same way as above
            foreach (Position p in block.TilePosition())
            {
                //reset opacity we set for ghost block
                imageControls[p.Row, p.Column].Opacity = 1;
                imageControls[p.Row, p.Column].Source = tileImages[block.Id];
            }
        }

        //Method to preview the next block
        private void DrawNextBlock(BlockQueue blockQueue)
        {
            Block next = blockQueue.NextBlock;
            NextImage.Source = blockImages[next.Id];
        }

        //Method to show the held block
        private void DrawHeldBlock(Block heldBlock)
        {
            if (heldBlock == null)
            {
                HoldImage.Source = blockImages[0];
            }
            else
            {
                HoldImage.Source = blockImages[heldBlock.Id];
            }
        }

        //Mathod to draw a ghost block to show the player where the block would land
        private void DrawGhostBlock(Block block)
        {
            int dropDistance = gameState.BlockDropDistance();
            foreach (Position p in block.TilePosition())
            {
                //the cells where the block will land are found by adding the drop distance to the tile positions of the current block
                //then we set opacity of the corresponding image controls to 0.25 and update the source
                //we need to remmber to reset the opacity when we re-draw the grid and the current block
                imageControls[p.Row + dropDistance, p.Column].Opacity = 0.25;
                imageControls[p.Row + dropDistance, p.Column].Source = tileImages[block.Id]; 
            }
        }

        //Method to draw both the grid and the current block
        private void Draw(GameState gameState)
        {
            DrawGrid(gameState.GameGrid);
            //DrawGhostBlock() method should be called BEFORE a DrawBlock() method
            DrawGhostBlock(gameState.CurrentBlock);
            DrawBlock(gameState.CurrentBlock);
            DrawNextBlock(gameState.BlockQueue);
            DrawHeldBlock(gameState.HeldBlock);
            //show the score
            ScoreText.Text = $"Score: {gameState.Score}";

            //we will call the Draw method when the game canvas is loaded
        }

        //Method for game loop
        //it has to be async because we want to wait without blocking the UI
        private async Task GameLoop()
        {
            //draw the game state
            Draw(gameState);
            //loop which runs until the game is over 
            //start the game loop when the canvas has loaded
            while (!gameState.GameOver)
            {
                //wait for the delay which is counted based on the current Score and set up minDelay, maxDelay and delayDecrease values
                //when the game starts the delay will be maxDelay, for each point the player gets the delay is decreased by delayDecrease
                //but it can never go below minDelay
                int delay = Math.Max(minDelay, maxDelay - (gameState.Score * delayDecrease));
                await Task.Delay(delay);
                //move the block down and re-draw
                gameState.MoveBlockDown();
                Draw(gameState);
            }
            
            //when we step out fromn the loop it means that the game is over
            //make the hidden Game Over menu visible
            GameOverMenu.Visibility = Visibility.Visible;
            //show final score in the Game Over menu
            FinalScoreText.Text = $"Score: {gameState.Score}";

        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            //if the game is ended => then pressing a key should not do anything 
            if (gameState.GameOver)
            {
                return;
            }
            //we will use the arrow keys for movement
            switch (e.Key)
            {
                case Key.Left:
                    gameState.MoveBlockLeft();
                    break;
                case Key.Right: 
                    gameState.MoveBlockRight();
                    break;
                case Key.Down: 
                    gameState.MoveBlockDown();
                    break;
                //the up arrow will rotate the block clockwise and the Z button will rotate counter-clockwise
                case Key.Up:
                    gameState.RotateBlockCW();
                    break;
                case Key.Z:
                    gameState.RotateBlockCCW();
                    break;
                //the C button will hold the block
                case Key.C:
                    gameState.HoldBlock();
                    break;
                //the space bar will hard drop the block
                case Key.Space:
                    gameState.DropBlock();
                    break;
                //add a default case where we simply return
                default:
                    return;
            }
            //outside the switch we call the Draw method
            //the default case ensures that we only re-draw if the player presses the key that actually does something
            Draw(gameState);
        }

        private async void GameCanvas_Loaded(object sender, RoutedEventArgs e)
        {
            //start game loop
            await GameLoop();
        }

        //Method bellow is called when a player presses the 'Play Again' button
        private async void PlayAgain_Click(object sender, RoutedEventArgs e)
        {
            //create a fresh game state, hide the game over menu and restart the game loop
            gameState = new GameState();
            GameOverMenu.Visibility = Visibility.Hidden;
            await GameLoop();
        }
    }
}