using System;
using Serilog;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

namespace serilogbm
{
	
	class MainClass
	{
		static void FileRoll()
		{
			Log.Logger = new LoggerConfiguration ()
				.WriteTo
				.RollingFile ("serilog-{Date}.log",
				             fileSizeLimitBytes: null)
				.CreateLogger ();
			var sw = new Stopwatch ();
			sw.Start ();
			Task.WhenAll(Enumerable.Range(0,100)
				.Select(async (i)=>{
					foreach(var j in Enumerable.Range(0,10000))
					{
						await Task.Run(()=>{
							Log.Logger.Information("abc{0}:{1}",i,j);
						});
					}
				})).Wait();
			sw.Stop ();
			Console.WriteLine ("elapsed {0}", sw.Elapsed);
		}
		
		public static void Main (string[] args)
		{
			FileRoll ();
			Console.WriteLine ("Hello World!");
		}
	}
}
