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
    public partial class FlappyBird : Window
    {
        private static object flappyBird;
        public int gravity = 8;




        /// <summary>
        /// // Denna metoden ska få fågeln att flyga upp när man håller ner space. 
        /// </summary>

        public static void KeyIsDown(object sender, KeyEventArgs e)
        {
            // om space trycks ner ska fågeln rotera 20 grader upp och få -8 i gravitation som ska förestla att den flyger
            if (e.Key == Key.Space)
            {
                flappyBird.RenderTransform = new RotateTransform(-20, flappyBird.Width / 2, flappyBird.Height / 2);
                gravity = -8;
            }
            //Om gameover är sant kan man starta om genom att trycka på "R"
            if (e.Key == Key.R && gameOver == true)
            {
                StartGame();
            }
        }


        /// <summary>
        /// I denna metod k ommer det vara tvärtemot. Alltså kommer fågeln falla när knappen inte trycks ner
        /// </summary>
        public static void KeyIsUp(object sender, KeyEventArgs e)
        // Och vridas 5 grder nedåt.
        {
            flappyBird.RenderTransform = new RotateTransform(5, flappyBird.Width / 2, flappyBird.Height / 2);
            gravity = 8;
        }
    }
}
