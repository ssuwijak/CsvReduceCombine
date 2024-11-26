using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsvReduceCombine
{
	internal class MyINISettings
	{
		private Dictionary<string, string> _dict;

		private string[] _keys = { "InputPath", "OutputPath", "LineMax" };
		public string InputPath { get => getKey(0); set => setKey(0, value); }
		public string OutputPath { get => getKey(1); set => setKey(1, value); }
		public string LineMax { get => getKey(2); set => setKey(2, value); }

		public MyINISettings()
		{
			_dict = new Dictionary<string, string>();
			_dict.Clear();
			foreach (var key in _keys)
				_dict.Add(key, "");
		}
		private string getKey(int index)
		{
			if (index >= 0 && index < _keys.Length)
				return _dict[_keys[index]];
			else
				return "";
		}
		private void setKey(int index, string value)
		{
			if (index >= 0 && index < _keys.Length)
			{
				if (_dict.ContainsKey(_keys[index]))
					_dict.Remove(_keys[index]);

				_dict.Add(_keys[index], value.Trim());
			}
		}
		public void Save(string path)
		{
			bool ret = false;
			string s = path.Trim();
			(ret, s) = checkFileExist(s);

			try
			{
				if (ret)
					File.Delete(s);
				else
				{
					string ss = Path.GetDirectoryName(s);
					ret = Directory.Exists(ss);
				}
			}
			catch (Exception ex)
			{
				ret = false;
			}

			if (ret)
				using (StreamWriter sw = new StreamWriter(s))
				{
					sw.WriteLine("[Settings]");

					foreach (var kv in _dict)
						sw.WriteLine(kv.Key + "=" + kv.Value);
				}
		}
		public void Load(string path)
		{
			bool ret = false;
			string s = path.Trim();
			(ret, s) = checkFileExist(s);

			if (ret)
				using (StreamReader sr = new StreamReader(s))
				{
					string line = "";

					while ((line = sr.ReadLine()) != null)
					{
						var ss = line.Split("=");

						if (ss.Length > 0)
						{
							s = (ss.Length < 2) ? "" : ss[1];

							if (_dict.ContainsKey(ss[0]))
							{
								_dict.Remove(ss[0]);
								_dict.Add(ss[0], s);
							}
						}
					}
				}
		}
		private (bool, string) checkFileExist(string path)
		{
			bool ret = false;
			string s = path.Trim();

			if (!string.IsNullOrEmpty(s))
				ret = File.Exists(s);

			return (ret, s);
		}

	}
}
