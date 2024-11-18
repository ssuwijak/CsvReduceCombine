using System.Linq.Expressions;

namespace CsvReduceCombine
{
	public partial class Form1 : Form
	{
		MyINISettings ini = new MyINISettings();
		private string ini_FilePath;

		private int lineMax;
		private bool flagExit;

		public Form1()
		{
			InitializeComponent();
		}
		private void Form1_Load(object sender, EventArgs e)
		{
			ini_FilePath = Path.Combine(Environment.CurrentDirectory, "settings.ini");
			ini.Load(ini_FilePath);

			textBox1.Text = ini.InputPath == "" ? Environment.CurrentDirectory : ini.InputPath;
			textBox2.Text = ini.OutputPath;
			textBox3.Text = ini.LineMax == "" ? "1000000" : ini.LineMax;

			this.Text = Application.ProductName;

			flagExit = false;
			label4.Text = "";
		}
		private void Form1_Closing(object sender, FormClosingEventArgs e)
		{
			if (button3.Text == "Stop")
			{
				e.Cancel = true;
				MessageBox.Show("press 'Stop' button before Exit", "Exit while running", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else
			{
				ini.InputPath = textBox1.Text;
				ini.OutputPath = textBox2.Text;
				ini.LineMax = textBox3.Text;
				ini.Save(ini_FilePath);
			}
		}
		private void Form1_Closed(object sender, FormClosedEventArgs e)
		{

		}
		private void button1_Click(object sender, EventArgs e)
		{
			string s = folderBrowserDialog();
			if (!string.IsNullOrEmpty(s)) textBox1.Text = s;
		}
		private void button2_Click(object sender, EventArgs e)
		{
			string s = folderBrowserDialog(textBox1.Text);
			if (!string.IsNullOrEmpty(s)) textBox2.Text = s;
		}
		private void button4_Click(object sender, EventArgs e)
		{
			textBox2.Text = "";
			textBox3.Text = "";
		}
		private void button3_Click(object sender, EventArgs e)
		{
			if (button3.Text == "Stop")
				flagExit = true;
			else
			{
				flagExit = false;

				groupBox1.Enabled = false;
				groupBox2.Enabled = false;
				button3.Text = "Stop";
				Application.DoEvents();

				verifyInput();

				scanFiles();
				mergeFiles();

				button3.Text = "Start";
				groupBox1.Enabled = true;
				groupBox2.Enabled = true;
			}
		}
		private string folderBrowserDialog(string initPath = "")
		{
			string selectedPath = "";

			FolderBrowserDialog folderDialog = new FolderBrowserDialog();
			// folderDialog.RootFolder = Environment.SpecialFolder.MyDocuments;
			folderDialog.InitialDirectory = string.IsNullOrEmpty(initPath.Trim()) ? Environment.CurrentDirectory : initPath.Trim();
			folderDialog.Description = "Select a folder";

			DialogResult result = folderDialog.ShowDialog();

			if (result == DialogResult.OK)
				selectedPath = folderDialog.SelectedPath;

			return selectedPath;
		}
		private void verifyInput()
		{
			textBox2.Text = textBox2.Text.Trim();
			if (string.IsNullOrEmpty(textBox2.Text))
			{
				textBox2.Text = Path.Combine(textBox1.Text, "output");
			}
			else
			{
				bool ret = false;
				string s;

				(ret, s) = checkPathExist(Path.GetDirectoryName(textBox2.Text));

				if (!ret)
					textBox2.Text = Path.Combine(textBox1.Text, "output");
			}

			(_, lineMax) = isInteger(textBox3.Text, 100);
			textBox3.Text = lineMax.ToString();

			Application.DoEvents();
		}
		private (bool, string) checkPathExist(string path)
		{
			bool ret = false;
			string s = path.Trim();

			if (!string.IsNullOrEmpty(s))
				try
				{
					ret = Path.Exists(s);
				}
				catch (Exception ex)
				{
					s = ex.Message;
				}

			return (ret, s);
		}
		private (bool, int) isInteger(string value, int notIntReturn = 0)
		{
			bool ret = false;
			int i = notIntReturn;

			value = value.Trim();
			if (string.IsNullOrEmpty(value)) return (ret, notIntReturn);

			ret = int.TryParse(value, out i);

			return (ret, i);
		}
		private void scanFiles()
		{
			bool ret;
			string[] workPath = { "", "" };
			string[] fileName = { "", "" };

			(ret, workPath[0]) = checkPathExist(textBox1.Text);
			if (ret) (ret, workPath[1]) = prepareOutputFolder(textBox2.Text);
			if (ret)
			{
				var di = new DirectoryInfo(workPath[0]);
				string filter = "*.*";

				int fileCount = di.GetFiles(filter).Length;

				progressBar1.Value = 0;
				progressBar1.Minimum = 0;
				progressBar1.Maximum = fileCount;
				Application.DoEvents();

				var csv = new TextReduceCombine();

				foreach (var f in di.GetFiles(filter))
				{
					if (flagExit) break;

					progressBar1.Value++;
					label4.Text = $"{f.Name} .. ({progressBar1.Value}/{progressBar1.Maximum})";
					Application.DoEvents();

					fileName[0] = Path.GetFileNameWithoutExtension(f.FullName);
					fileName[1] = Path.Combine(workPath[1], fileName[0] + ".tmp");
					try
					{
						if (File.Exists(fileName[1])) File.Delete(fileName[1]);
						File.WriteAllText(fileName[1], csv.Read(f.FullName));
						label4.Text += " .. reduced";
					}
					catch (Exception ex)
					{
						label4.Text = ex.Message;
					}
					Application.DoEvents();
				}
			}
			else
			{
				label4.Text = $"error, Path not found.";
			}
		}
		private (bool, string) prepareOutputFolder(string path)
		{
			bool ret = false;
			var output = new DirectoryInfo(path);
			try
			{
				if (output.Exists)
					foreach (var f in output.GetFiles())
						f.Delete();
				else
					output.Create();

				ret = true;
			}
			catch (Exception ex)
			{
				string errMsg = ex.Message;
			}
			return (ret, output.FullName);
		}

		private void mergeFiles()
		{
			var di = new DirectoryInfo(textBox2.Text);
			var files = di.GetFiles("*.tmp");

			if (files.Length < 1) return;

			progressBar1.Value = 0;
			progressBar1.Minimum = 0;
			progressBar1.Maximum = files.Length;
			Application.DoEvents();


			TextFileAppender appender = null;
			TextFileReader reader = null;
			string header, line, line_pending;

			header = "";
			line_pending = "";

			foreach (var f in files)
			{
				if (flagExit) break;

				progressBar1.Value++;
				label4.Text = $"combining {f.Name} .. ({progressBar1.Value}/{progressBar1.Maximum})";
				Application.DoEvents();

				if (reader == null)
				{
					reader = new TextFileReader(f.FullName);
					header = "";
				}

				while ((line = reader.ReadLine()) != null)
				{
					if (header == "")
					{
						header = line;
					}
					else
					{
						if (appender == null)
						{
							string filePath = Path.Combine(di.FullName, getRandomFileName());
							appender = new TextFileAppender(filePath, lineMax);
							appender.AppendText(header);
							if (line_pending != "")
							{
								appender.AppendText(line_pending);
								line_pending = "";
							}
						}

						try
						{
							appender.AppendText(line);
						}
						catch (Exception ex)
						{
							line_pending = line;
							appender.Dispose();
							appender = null;
						}
						finally { }
					}
				}
				reader.Dispose();
				reader = null;

				f.Delete();
			}

			if (appender != null)
			{
				appender.Dispose();
				appender = null;
			}
			label4.Text = "done .. " + DateTime.Now.ToString("d-MMM-yy HH:mm:ss");
		}

		private string getRandomFileName()
		{
			var rand = new Random();
			var randNumber = rand.Next(99999);
			return DateTime.Now.ToString("yyMMdd_HHmm_") + $"{randNumber:00000}" + ".csv";
		}
	}
}
