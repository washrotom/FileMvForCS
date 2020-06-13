using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.IO;


namespace FileMvForCS
{
 
    class MainApp
    {
        static void Main(string[] args)
        {
            string Tdir;

            int num_mkdir = 0;
            int num_mvfile = 0;

            Stopwatch swh = new Stopwatch();

            //try
            try
            {
                //selet dir and making file list;
                Console.WriteLine("input Taget dir :");
                Tdir = Console.ReadLine();


                swh.Start();

                if (Directory.Exists(Tdir) == true)
                {

                    var Soure_files = (from file in Directory.GetFiles(Tdir)
                                       let info = new FileInfo(file)
                                       select new
                                       {
                                           Name = info.Name,
                                           LastTime = info.LastWriteTimeUtc.ToString("yy.MM.dd")

                                       }).ToList();


                    //move file;
                    foreach (var f in Soure_files)
                    {

                        string MvDirs = $"{f.LastTime}";

                        if (Directory.Exists($"{f.LastTime}") == true)
                        {

                            MvDirs = MvDirs + "\\" + f.Name;

                            Console.WriteLine($"{f.Name}");
                            System.IO.File.Move($"{f.Name}", MvDirs);

                            num_mvfile++;

                        }
                        else
                        {
                            num_mkdir++;

                            DirectoryInfo mkdir = Directory.CreateDirectory(MvDirs);

                            MvDirs = MvDirs + "\\" + f.Name;

                            Console.WriteLine($"{MvDirs}");
                            Console.WriteLine($"{f.Name}");
                            System.IO.File.Move($"{f.Name}", MvDirs);

                            num_mvfile++;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("not exist Directory");
                    return;
                }

            }

            // catch 
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("Need to superuser");
            }
            catch (Exception)
            {
                Console.WriteLine("Unknown Error");
            }

            swh.Stop();

            string time1 = swh.Elapsed.ToString();

            //chek res cmd window;
            while (true)
            {
                Console.WriteLine();

                Console.WriteLine($"run_time is {time1}");

                Console.WriteLine();

                Console.WriteLine("making dir :{0} / move file : {1}", num_mkdir, num_mvfile);

                string Any = null;
                Console.WriteLine("Exit For AnyKey Press And Enter");

                Any = Console.ReadLine();

                if (Any != null) break;

            }


        }

    }
}
