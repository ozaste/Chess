using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace FlappyBird
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        DispatcherTimer gameTimer = new DispatcherTimer();  // 

        double Score;       
        int gravity = 8;  // här sätter jag gravitation till 8 för att den alltid ska falla
        bool gameOver;      
        Rect flappyBirdHitBox; // Jag använder "Rect" som hitbox 
        public MainWindow()
        {
            InitializeComponent();

            gameTimer.Tick += MainEventTimer; 
            gameTimer.Interval = TimeSpan.FromMilliseconds(20); // I denna rad bestäms hur snabbt fågeln kommer att flyga. 
            StartGame();
        }

        private void MainEventTimer(object sender, EventArgs e)
        {

            txtScore.Content = "Score: " + Score;  // här uppdateras poängen

            flappyBirdHitBox = new Rect(Canvas.GetLeft(flappyBird), Canvas.GetTop(flappyBird), flappyBird.Width - 5, flappyBird.Height);  // här används rect för att skapa fågelns hitbox


            Canvas.SetTop(flappyBird, Canvas.GetTop(flappyBird) + gravity);  // här läggs gravitationen till. 

            if (Canvas.GetTop(flappyBird) < -10 || Canvas.GetTop(flappyBird) > 458)  // Här sätts det så att du förlorar om fågelns hitbox träffas. 
            {
                EndGame();
            }

            foreach (var x in MyCanvas.Children.OfType<Image>())  // denna foreach loop får rören och molnen att röra på sig
            {
                if ((string)x.Tag == "obs1" || (string)x.Tag == "obs2" || (string)x.Tag == "obs3")  // om dessa tags matchar med bilderna i canvasen kan 
                                                                                                    // vi lägga till alla funktioner...
                {
                    Canvas.SetLeft(x, Canvas.GetLeft(x) - 8);   //här får jag rören att röra sig (även farten)

                    if(Canvas.GetLeft(x) < -100) //om den gått förbi den vänstra sidan av canvasen...
                    {
                        Canvas.SetLeft(x, 800);  // flyttas den till den högra sidan.

                        Score += .5;     // Och när detta sker ska ett poäng läggas till. 0.5 eftersom att det är två rör man passerar. 
                    
                    }

                    Rect pipeHitBox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);  // här ger jag rören deras hitbox

                    if (flappyBirdHitBox.IntersectsWith( pipeHitBox))  // Här avslutas spelet ifall flappybird interagerar med rörets hitbox.
                    {
                        EndGame();
                    }
                }


                if ((string)x.Tag == "cloud")
                {
                    Canvas.SetLeft(x, Canvas.GetLeft(x) - 10);   // här sätts farten för molnen

                    if (Canvas.GetLeft(x) < -250)  //om den gått förbi den vänstra sidan av canvasen...
                    {
                        Canvas.SetLeft(x, 550); // flyttas den till den högra sidan...
                    }
                }
            }
        }

        private void KeyIsDown(object sender, KeyEventArgs e)  // Denna metoden ska få fågeln att flyga upp när man håller ner space. 
        {
            if (e.Key == Key.Space)  // om space trycks ner ska fågeln rotera 20 grader upp och få -8 i gravitation som ska förestla att den flyger
            {
                flappyBird.RenderTransform = new RotateTransform(-20, flappyBird.Width / 2, flappyBird.Height /2);
                gravity = -8;
            }

            if (e.Key == Key.R && gameOver == true) //Om gameover är sant kan man starta om genom att trycka på "R"
            {
                StartGame();
            }
        }

        private void KeyIsUp(object sender, KeyEventArgs e) // I denna metod kommer det vara tvärtemot. Alltså kommer fågeln falla när knappen inte trycks ner
                                                            // Och vridas 5 grder nedåt.
        {
            flappyBird.RenderTransform = new RotateTransform(5, flappyBird.Width / 2, flappyBird.Height / 2);
            gravity = 8;
        }

        private void StartGame()  // I denna metod sätts alla värden om till start värden för att man ska kunna börja om.
        {
            MyCanvas.Focus();  // focus håller canvasen i focus medan spelet körs.

            int temp = 300;   //...

            Score = 0;     // 0 är startpoänget.

            gameOver = false;
            Canvas.SetTop(flappyBird, 190);  // detta kommer att vara fågelns första position när man startar spelet. 


            foreach (var x in MyCanvas.Children.OfType<Image>())   //denna foreach loop kommer göra det möjligt att flytta på 
                                                                   //images med hjäp av tagsen
                                                                   
            {
                if ((string)x.Tag == "obs1")
                {
                    Canvas.SetLeft(x, 300);  //här sätts avstånden så att det är bra avstånd mellan rören. 
                }
                if ((string)x.Tag == "obs2")
                {
                    Canvas.SetLeft(x, 800);  //här sätts avstånden såatt det är jämnt avstånd mellan rören.
                }
                if ((string)x.Tag == "obs3")
                {
                    Canvas.SetLeft(x, 1100);  //här sätts avstånden såatt det är jämnt avstånd mellan rören.
                }

                if ((string)x.Tag == "cloud")
                {
                    Canvas.SetLeft(x, 300 + temp);    // här sätts molnenns startposition
                    temp = 800;   
                }

            }

            gameTimer.Start();


        }

        private void EndGame()  // När metoden EndGame kallas ska spelet stoppas och det kommer stå att spelaren har förlorat. 
        {
            gameTimer.Stop();   
            gameOver = true;
            txtScore.Content += "        Game Over! Press R to try again";
        }


    }
}
