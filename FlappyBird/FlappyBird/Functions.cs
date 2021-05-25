using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Canvas;

namespace FlappyBird
{
    public partial class Functions
    {

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
        private void EndGame()
        {
            gameTimer.Stop();
            gameOver = true;
            txtScore.Content += "        Game Over! Press R to try again";
        }


    }
}
