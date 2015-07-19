using System;
using NLog;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;

namespace nlogbm
{
	class MainClass
	{
		static ILogger m_Logger = LogManager.GetCurrentClassLogger();

		static void nlogtest()
		{
			var sw = new Stopwatch();
			sw.Start ();
			Task.WhenAll (Enumerable.Range (0, 100)
				.Select (async (arg) => {
					foreach(var j in Enumerable.Range(0,10000))
					{
						await Task.Run(()=>{
							m_Logger.Info("abc{0}:{1}",arg,j);
						});
					}
				})).Wait();
			sw.Stop ();
			Console.WriteLine ("elapsed:{0}", sw.Elapsed);
		}
		
		public static void Main (string[] args)
		{
			nlogtest ();
			Console.WriteLine ("Hello World!");
		}
	}
}
