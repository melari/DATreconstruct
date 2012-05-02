using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DATreconstruct
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 2 || args.Length > 3)
            {
                Console.WriteLine("Syntax: DATreconstruct infile.bin outfile.txt <options>");
                Console.WriteLine("Options: -l  : Use little endian instead of big.");
                return;
            }

            if (!System.IO.File.Exists(args[0]))
            {
                Console.WriteLine("Error: File '" + args[0] + "' does not exist.");
                return;
            }

            byte[] bytes = System.IO.File.ReadAllBytes(args[0]);
            

            bool firstHalf = true;
            bool firstByte = true;
            byte first = 0x0;
            StringBuilder sb = new StringBuilder(bytes.Length/2*6+4);
            sb.Append("DAT ");
            foreach (byte b in bytes)
            {
                if (args.Length > 2 && args[2] == "-l")
                {
                    if (firstHalf)
                    {
                        first = b;
                    }
                    else
                    {
                        if (!firstByte) { sb.Append(", "); }
                        else { firstByte = false; }
                        sb.Append("0x");
                        sb.AppendFormat("{0:x2}", b);
                        sb.AppendFormat("{0:x2}", first);
                    }
                    firstHalf = !firstHalf;
                }
                else
                {
                    if (firstHalf)
                    {
                        if (!firstByte) { sb.Append(", "); }
                        else { firstByte = false; }
                        sb.Append("0x");
                        sb.AppendFormat("{0:x2}", b);
                    }
                    else
                    {
                        sb.AppendFormat("{0:x2}", b);
                    }
                    firstHalf = !firstHalf;
                }
            }
            System.IO.File.WriteAllText(args[1], sb.ToString());
        }
    }
}
