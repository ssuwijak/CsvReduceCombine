using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsvReduceCombine
{
	public class TextFileAppender : IDisposable
	{
		private StreamWriter sw;

		private int _lineCounter;
		private int _lineCounter_Max;
		public int LineCounter_Max { get => _lineCounter_Max; }
		private string _filePath;
		public string FilePath { get => _filePath; private set => _filePath = value.Trim(); }

		public TextFileAppender(string filePath, int limitMaxLine = 0)
		{
			bool ret = false;
			string s;
			(ret, s) = PathExists(filePath);

			if (ret)
			{
				FilePath = filePath;
				sw = new StreamWriter(FilePath, true); // Append mode

				_lineCounter = 0;
				_lineCounter_Max = (limitMaxLine < 10 || limitMaxLine > 1000999) ? 1000 : limitMaxLine;
			}
			else
				throw new FileNotFoundException($"filePath='{filePath}' not found.");
		}
		public static (bool, string) PathExists(string filePath)
		{
			bool ret = false;
			if (string.IsNullOrEmpty(filePath)) return (ret, "");

			string p = filePath.Trim();
			if (string.IsNullOrEmpty(p)) return (ret, p);

			ret = Directory.Exists(Path.GetDirectoryName(p));

			return (ret, p);
		}
		public string FileNameByTime(string fileExt = "")
		{
			if (string.IsNullOrEmpty(fileExt) || fileExt.Trim() == "")
				fileExt = ".txt";
			else
			{
				string s = fileExt.Trim();
				if (!s.StartsWith(".")) s = "." + s;
				if (s.Length < 2)
					s = ".txt";
				else if (s.Length > 4)
					s = s.Substring(4);
				else
					s = s + "";

				fileExt = s;
			}

			return DateTime.Now.ToString("yyyyMMdd_HHmmss") + fileExt;
		}
		public void AppendText(string text)
		{
			if (_lineCounter < _lineCounter_Max)
			{
				sw.WriteLine(text);
				_lineCounter++;
			}
			else
				throw new OverflowException($"the file has reached the max. limit, {_lineCounter_Max} line(s)");
		}
		public void Dispose()
		{
			if (sw != null)
			{
				try { sw.Close(); }
				catch { }
				finally
				{
					sw?.Dispose();
					sw = null;
				}
			}
		}
	}
}
