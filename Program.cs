using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandoQuiz
{
    class Program
    {
        static void Main(string[] args)
        {
            string file;
            //OpenFileDialog d = new OpenFileDialog();
            //if (!(d.ShowDialog() ?? false))
            //    return;
            //file=d.FileName
            string[] cmd = Environment.CommandLine.Replace("\"", "").Split(new[] { ' ' }, 2);
            if (cmd.Length == 1)
            {
                Console.Write("Enter path to file: ");
                file = Console.ReadLine();
            }
            else
                file = cmd[1];
            Console.Write("Loading questions...");
            string[] lines = File.ReadAllLines(file);
            Tuple<string, string>[] q = new Tuple<string, string>[lines.Length];
            for (int i = 0; i < lines.Length; ++i)
                q[i] = new Tuple<string, string>(lines[i].Split('|')[0], lines[i].Split('|')[1]);
            Console.WriteLine("done.");
        hmq:
            Console.Write("How many questions? ");
            int n;
            if (!int.TryParse(Console.ReadLine(), out n))
                goto hmq;
            Console.Write("Press any key to start.");
            Console.ReadKey(true);
            Console.Clear();
            Random r = new Random();
            DateTime dt = DateTime.Now;
            int score = 0;
            for(int i=0;i<n;++i)
            {
                int qn = r.Next(q.Length);
                Console.Write("Question #{0}:\n\n    {1}\n    Answer: \n\nTime: {2:T}", i + 1, q[qn].Item1, DateTime.Now - dt);
                Console.SetCursorPosition(12, 3);
                score += Console.ReadLine() == q[qn].Item2 ? 1 : 0;
                Console.Clear();
            }
            Console.Write("Quiz complete.\n\n    Score: {0}/{1} ({2:p})\n    Total time: {3}\n    Time per question: {4}", score, n, score * 1.0 / n, DateTime.Now - dt, new TimeSpan((DateTime.Now - dt).Ticks / n));
            Console.ReadKey(true);
        }
    }
}
