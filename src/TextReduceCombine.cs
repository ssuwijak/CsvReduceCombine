using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace CsvReduceCombine
{
	public class Headers
	{
		public const int COLUMN_LENGTH_LEAST = 24;
		public const int COLUMN_STATUS_INDEX = 3;
		public const string COLUMN_STATUS_KEYWORD = "completed";

		protected string _separator;
		public string Separator
		{
			get => _separator;
			set { _separator = string.IsNullOrEmpty(value) ? "," : value; }
		}

		protected int _length;
		public int Length { get => _length; }
		public Headers(string separator = ",")
		{
			_length = 0;
			Separator = separator;
		}
		public bool Check(string line)
		{
			bool ret = false;
			string[] headers = line.Split(_separator);

			_length = headers.Length;

			if (headers.Length >= COLUMN_LENGTH_LEAST)
				ret = headers[COLUMN_STATUS_INDEX].ToLower() == "status";

			return ret;
		}

	}
	public class TextReduceCombine
	{
		public Headers header;
		public string Separator { get => header.Separator; }
		public TextReduceCombine(string separator = ",") => header = new Headers(separator);

		public (bool, string) CheckFileExist(string path)
		{
			bool ret = false;
			string s = path.Trim();

			if (!string.IsNullOrEmpty(s))
				try
				{
					ret = File.Exists(s);
				}
				catch (Exception ex)
				{
					string errMsg = ex.Message;
				}

			return (ret, s);
		}
		public string GetFirst24Columns(string text, bool onlyStatusCompleted)
		{
			string ret = "";

			if (string.IsNullOrEmpty(text.Trim())) return ret;

			string[] ss = text.Split(header.Separator).Select(s => s.Trim()).ToArray();

			if (ss.Length == header.Length)
				if (onlyStatusCompleted)
				{
					if (ss[Headers.COLUMN_STATUS_INDEX].ToLower() == Headers.COLUMN_STATUS_KEYWORD)
						ret = string.Join(header.Separator, ss.Take(24));
				}
				else
					ret = string.Join(header.Separator, ss.Take(24));

			return ret;
		}
		public string Read(string filepath, string separator = ",")
		{
			bool ret;
			string s;

			(ret, s) = CheckFileExist(filepath);
			if (!ret) return "";

			StringBuilder sb = new StringBuilder();
			header.Separator = separator;
			sb.Clear();

			try
			{
				using (StreamReader sr = new StreamReader(s))
				{
					int i = 0;
					string line = "";

					while ((line = sr.ReadLine()) != null && ret)
					{
						i++;

						if (i == 1)
						{
							ret = header.Check(line);
							if (ret)
								line = GetFirst24Columns(line, false);
						}
						else
						{
							line = GetFirst24Columns(line, true);
						}

						if (!string.IsNullOrEmpty(line) && ret)
							sb.Append(line + "\n");
					}
				}
			}
			catch (Exception ex)
			{
				string errMsg = ex.Message;
			}

			return sb.ToString();
		}


		public void Write(string filepath)
		{



		}
	}
}
