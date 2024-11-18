using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsvReduceCombine
{
	public class TextFileReader : IDisposable
	{
		private StreamReader sr;

		private int _currentLineIndex;
		public int CurrentLineIndex
		{
			get => _currentLineIndex;
			private set
			{
				_currentLineIndex = value;
				if (_currentLineIndex > _lineIndexMax) _lineIndexMax = _currentLineIndex;
			}
		}
		private int _lineIndexMax;
		public int LineIndex_Max { get => _lineIndexMax; }

		private string _filePath;
		public string FilePath { get => _filePath; }

		public TextFileReader(string filePath)
		{
			bool ret = false;
			(ret, _filePath) = FileExists(filePath);

			if (ret)
			{
				sr = new StreamReader(_filePath);
				_currentLineIndex = 0;
				_lineIndexMax = 0;
			}
			else
				throw new FileNotFoundException($"filePath='{filePath}' not found.");
		}
		public static (bool, string) FileExists(string filePath)
		{
			bool ret = false;

			if (string.IsNullOrEmpty(filePath)) return (ret, "");

			string p = filePath.Trim();
			if (string.IsNullOrEmpty(p)) return (ret, p);

			ret = File.Exists(p);

			return (ret, p);
		}
		public string ReadLine()
		{
			if (sr.EndOfStream) return null;

			string line = sr.ReadLine();
			CurrentLineIndex++;

			return line;
		}
		public string ReadLine(int lineNumber)
		{
			if (lineNumber < 1)
				throw new ArgumentOutOfRangeException("lineNumber", "Invalid line number.");

			sr.BaseStream.Seek(0, SeekOrigin.Begin);
			CurrentLineIndex = 0;

			string line;

			while ((line = sr.ReadLine()) != null)
			{
				CurrentLineIndex++;
				if (_currentLineIndex == lineNumber) return line;
			}

			if (_currentLineIndex < lineNumber)
				throw new ArgumentOutOfRangeException("lineNumber", "Invalid line number.");

			return null;
		}

		public void Dispose()
		{
			if (sr != null)
			{
				try { sr.Close(); }
				catch { }
				finally
				{
					sr?.Dispose();
				}
			}
		}


	
	}


}




