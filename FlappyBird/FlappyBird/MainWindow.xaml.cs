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

        DispatcherTimer gameTimer = new DispatcherTimer();  

        double Score;
        // här sätter jag gravitation till 8 för att den alltid ska falla
        int gravity = 8; 
        bool gameOver;
        // Jag använder "Rect" som hitbox 
        Rect flappyBirdHitBox; 
        public MainWindow()
        {
            InitializeComponent();

            gameTimer.Tick += MainEventTimer;
            // I denna rad bestäms hur snabbt fågeln kommer att flyga. 
            gameTimer.Interval = TimeSpan.FromMilliseconds(20); 
            StartGame();
        }

        public void MainEventTimer(object sender, EventArgs e)
        {
            // här uppdateras poängen
            txtScore.Content = "Score: " + Score;  
            // här används rect för att skapa fågelns hitbox
            flappyBirdHitBox = new Rect(Canvas.GetLeft(flappyBird), Canvas.GetTop(flappyBird), flappyBird.Width - 5, flappyBird.Height);  

            // här läggs gravitationen till. 
            Canvas.SetTop(flappyBird, Canvas.GetTop(flappyBird) + gravity);  

            // Här sätts det så att du förlorar om fågelns hitbox träffas.
            if (Canvas.GetTop(flappyBird) < -10 || Canvas.GetTop(flappyBird) > 458)  
            {
                EndGame();
            }

            //nether
            // denna foreach loop får rören och molnen att röra på sig
            foreach (var x in MyCanvas.Children.OfType<Image>()) 
            {

                // om dessa tags matchar med bilderna i canvasen kan 
                // vi lägga till alla funktioner...
                if ((string)x.Tag == "obs1" || (string)x.Tag == "obs2" || (string)x.Tag == "obs3")  
                {
                    //här får jag rören att röra sig (även farten)
                    Canvas.SetLeft(x, Canvas.GetLeft(x) - 8);  

                    //om den gått förbi den vänstra sidan av canvasen...
                    if (Canvas.GetLeft(x) < -100) 
                    {
                        // flyttas den till den högra sidan.
                        Canvas.SetLeft(x, 800);  
                        // Och när detta sker ska ett poäng läggas till. 0.5 eftersom att det är två rör man passerar. 
                        Score += .5;     
                    
                    }
                    //nether


                    // här ger jag rören deras hitbox
                    Rect pipeHitBox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);  

                    // Här avslutas spelet ifall flappybird interagerar med rörets hitbox.
                    if (flappyBirdHitBox.IntersectsWith( pipeHitBox))  
                    {
                        EndGame();
                    }
                    
                }

                if ((string)x.Tag == "coin")
                {
                    //här får jag coinen att röra sig (även farten)
                    Canvas.SetLeft(x, Canvas.GetLeft(x) - 8);

                    //om den gått förbi den vänstra sidan av canvasen...
                    if (Canvas.GetLeft(x) < -100)
                    {
                        // flyttas den till den högra sidan.
                        Canvas.SetLeft(x, 800);
                         
                        
                        // här sätter jag hitbox för min coin
                    }
                    Rect CoinHitBox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);
                    if (flappyBirdHitBox.IntersectsWith(CoinHitBox))
                    {
                        Score += 5;

                        // Den flyttas tillbaka bak i canvasen 
                        Canvas.SetLeft(x, 750);
                    }
                }


                    if ((string)x.Tag == "cloud")
                {
                    // här sätts farten för molnen
                    Canvas.SetLeft(x, Canvas.GetLeft(x) - 10);   

                    //om den gått förbi den vänstra sidan av canvasen...
                    if (Canvas.GetLeft(x) < -250)  
                    {
                        // flyttas den till den högra sidan...
                        Canvas.SetLeft(x, 550); 
                    }
                }
                
                
            }

        }

        /// <summary>
        /// // Denna metoden ska få fågeln att flyga upp när man håller ner space. 
        /// </summary>
        
        public void KeyIsDown(object sender, KeyEventArgs e)  
        {
            // om space trycks ner ska fågeln rotera 20 grader upp och få -8 i gravitation som ska förestla att den flyger
            if (e.Key == Key.Space)  
            {
                flappyBird.RenderTransform = new RotateTransform(-20, flappyBird.Width / 2, flappyBird.Height /2);
                gravity = -8;
            }
            //Om gameover är sant kan man starta om genom att trycka på "R"
            if (e.Key == Key.R && gameOver == true) 
            {
                StartGame();
            }
        }
        /// <summary>
        /// I denna metod kommer det vara tvärtemot. Alltså kommer fågeln falla när knappen inte trycks ner
        /// </summary>
        public void KeyIsUp(object sender, KeyEventArgs e) 
                                                            // Och vridas 5 grder nedåt.
        {
            flappyBird.RenderTransform = new RotateTransform(5, flappyBird.Width / 2, flappyBird.Height / 2);
            gravity = 8;
        }

        /// <summary>
        /// I denna metod sätts alla värden om till start värden för att man ska kunna börja om.
        /// </summary>
        public void StartGame() 
        {
            // focus håller canvasen i focus medan spelet körs.

            MyCanvas.Focus();  
            int temp = 300;  
            // 0 är startpoänget.
            Score = 0;     

            gameOver = false;
            // detta kommer att vara fågelns första position när man startar spelet. 
            Canvas.SetTop(flappyBird, 190);  

            //denna foreach loop kommer göra det möjligt att flytta på 
            //images med hjäp av tagsen
            foreach (var x in MyCanvas.Children.OfType<Image>())   
                                                                   
            {
                if ((string)x.Tag == "obs1")
                {
                    Canvas.SetLeft(x, 500);  
                }
                //här sätts avstånden så att det är bra avstånd mellan rören. 
                if ((string)x.Tag == "obs2")
                {
                    Canvas.SetLeft(x, 800);  
                }
                //här sätts avstånden såatt det är jämnt avstånd mellan rören.
                if ((string)x.Tag == "obs3")
                {
                    //här sätts avstånden så att det är jämnt avstånd mellan rören.
                    Canvas.SetLeft(x, 1100);  
                }

                if ((string)x.Tag == "cloud")
                {
                    // här sätts molnenns startposition
                    Canvas.SetLeft(x, 300 + temp);    
                    temp = 800;   
                }

            }

            gameTimer.Start();


        }

        /// <summary>
        /// Denna metod ska avsluta spelet. 
        /// </summary>
        public void EndGame()  
        {
            gameTimer.Stop();   
            gameOver = true;
            txtScore.Content += "        Game Over! Press R to try again";
        }

       
    }
}
