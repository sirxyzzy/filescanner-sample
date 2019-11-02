using System;
using System.Diagnostics;
using System.IO;

namespace FileScanner
{
    class Program
    {
        static void Main(string[] args)
        {
            string dirName;

            if (args.Length > 0)
            {
                dirName = args[0];
            }
            else
            {
                dirName = @"C:\music\altan";
            }

            if (!Directory.Exists(dirName))
            {
                Console.WriteLine($"{dirName} is not a folder I can scan");
                return;
            }

            // Some metrics
            int fileCount = 0;
            int skippedFiles = 0;
            long bytesRead = 0;

            // we are computing the sum of all bytes, in all files
            long total = 0;

            Stopwatch sw = new Stopwatch();
            sw.Start();

            foreach( var filename in Directory.EnumerateFiles(dirName, "*", SearchOption.AllDirectories))
            {
                Console.WriteLine($"Checking {filename}...");

                fileCount += 1;
                try
                {
                    var bytes = File.ReadAllBytes(filename);
                    bytesRead += bytes.Length;
                    foreach(var b in bytes)
                    {
                        total += b;
                    }
                }
                catch
                {
                    // Track how many failed, and keep going
                    skippedFiles += 1;
                }
            }

            sw.Stop();

            var elapsed = sw.ElapsedMilliseconds;

            Console.WriteLine($"Took {elapsed}ms to scan {dirName}, found {fileCount} files, skipped {skippedFiles}, bytes read {bytesRead} total={total}");

            Console.ReadLine();
        }
    }
}
