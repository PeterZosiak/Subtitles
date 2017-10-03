using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;

namespace Subtitles
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }

        private void DownloadSubtitle(string sub_name, string hash, string lang = "cze")
        {
            XmlDocument doc = new XmlDocument();
            doc.Load("https://www.opensubtitles.org/en/search/sublanguageid-cze/moviehash-" + hash + "/xml");
            XmlNodeList elemList = doc.GetElementsByTagName("IDSubtitle");
            txtInfo.Text += "Stahujem titulky pre" + sub_name + "/n/r";
            if (elemList.Count > 0)
            {
                for (int i = 0; i < elemList.Count; i++)
                {

                    using (var client = new System.Net.WebClient())
                    {
                        string id = elemList[i].InnerXml;
                        string subpage2 = client.DownloadString("http://osdownloader.org/en/osdownloader.subtitles-download/subtitles/" + id);

                        Regex regex = new Regex("href\\s*=\\s*(?:\"(?<1>[^\"]*)\"|(?<1>\\S+))", RegexOptions.IgnoreCase);
                        Match match;
                        for (match = regex.Match(subpage2); match.Success; match = match.NextMatch())
                        {

                            foreach (Group group in match.Groups)
                            {
                                if (group.Value.Contains("/sub/") && !group.Value.Contains("href"))
                                {
                                    Console.WriteLine("Group value: {0}", group.Value);
                                    client.DownloadFile(group.Value, sub_name + ".zip");
                                    ZipFile.ExtractToDirectory(sub_name + ".zip", sub_name + "-zip");
                                    DeleteJunk(sub_name + "-zip");
                                    string[] fileEntries = Directory.GetFiles(sub_name + "-zip");
                                    foreach (string fileName in fileEntries)
                                    {
                                        if (fileName.EndsWith("srt") || fileName.EndsWith("sub"))
                                        {
                                            ConvertFileEncoding(fileName, fileName);
                                            RenameSubtitle(fileName, sub_name);
                                        }
                                    }

                                    File.Delete(sub_name + ".zip");
                                    Directory.Delete(sub_name + "-zip");
                                    txtInfo.Text += "Hotovo";
                                    return;
                                }
                            }
                        }

                    }
                }
            }
            else
            {
                txtInfo.Text += "Titulky nenajdene";
            }

        }

        private void ProcessEntries(string[] fileEntries)
        {
            List<string> subtitles = new List<string>();
            List<string> shows = new List<string>();

            foreach (string fileName in fileEntries)
                if (fileName.EndsWith("srt") || fileName.EndsWith("sub"))
                {
                    if (chcUTF.Checked)
                    {
                        if (GetEncoding(fileName) != Encoding.UTF8)
                        {
                            ConvertFileEncoding(fileName, fileName);
                        }

                    }
                    subtitles.Add(fileName);
                }
                else
                {
                    shows.Add(fileName);
                }

            shows = shows.OrderBy(x => x).ToList();
            subtitles = subtitles.OrderBy(x => x).ToList();

            if (chcDownloadSub.Checked)
            {
                for (int i = 0; i <= shows.Count - 1; i++)
                {
                    if (!CheckSubAlreadyExist(shows[i]))
                    {
                        DownloadSubtitle(shows[i], ToHexadecimal(ComputeMovieHash(shows[i])));
                        DeleteJunk(Path.GetDirectoryName(shows[i]));
                    }
                    
                }
            }
            else
            {

                if (subtitles.Count != shows.Count)
                {
                    txtInfo.Text += "Nesedi pocet titulkov a casti";
                    return;
                }
                else
                {
                    for (int i = 0; i <= subtitles.Count - 1; i++)
                    {
                        if (chcNames.Checked)
                        {
                            RenameSubtitle(subtitles[i], shows[i]);
                        }

                    }
                }
            }



            txtInfo.Text += "Hotovo";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtInfo.Text += "";

            //(Directory.GetDirectories(txtPath.Text))[0]

            if (chcSubFold.Checked == false)
            {
                DeleteJunk(txtPath.Text);
                string[] fileEntries = Directory.GetFiles(txtPath.Text);
                ProcessEntries(fileEntries);
            }
            else
            {
                foreach (string dir in Directory.GetDirectories(txtPath.Text).OrderBy(x => x))
                {
                    DeleteJunk(dir);
                    string[] entries = Directory.GetFiles(dir);
                    ProcessEntries(entries);
                }
            }



        }
        private bool CheckSubAlreadyExist(string show)
        {
            return File.Exists(Path.ChangeExtension(show, "cz.srt"));
        }

        private void DeleteJunk(string dir)
        {
            string[] entries = Directory.GetFiles(dir);

            foreach (string file in entries)
            {
                if (file.EndsWith("txt") || file.EndsWith("jpg") || file.EndsWith("jpeg") || file.EndsWith("nfo") || file.EndsWith("sfv"))
                {
                    try
                    {
                        File.Delete(file);
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }


            }
        }

        public static Encoding GetEncoding(string filename)
        {
            // Read the BOM
            var bom = new byte[4];
            using (var file = new FileStream(filename, FileMode.Open, FileAccess.Read))
            {
                file.Read(bom, 0, 4);
            }

            // Analyze the BOM
            if (bom[0] == 0x2b && bom[1] == 0x2f && bom[2] == 0x76) return Encoding.UTF7;
            if (bom[0] == 0xef && bom[1] == 0xbb && bom[2] == 0xbf) return Encoding.UTF8;
            if (bom[0] == 0xff && bom[1] == 0xfe) return Encoding.Unicode; //UTF-16LE
            if (bom[0] == 0xfe && bom[1] == 0xff) return Encoding.BigEndianUnicode; //UTF-16BE
            if (bom[0] == 0 && bom[1] == 0 && bom[2] == 0xfe && bom[3] == 0xff) return Encoding.UTF32;
            return Encoding.ASCII;
        }

        public void ConvertFileEncoding(String sourcePath, String destPath)

        {

            // If the destination’s parent doesn’t exist, create it.

            String parent = Path.GetDirectoryName(Path.GetFullPath(destPath));

            if (!Directory.Exists(parent))

            {

                Directory.CreateDirectory(parent);

            }

            // Convert the file.

            String tempName = null;

            try

            {

                tempName = Path.GetTempFileName();

                using (StreamReader sr = new StreamReader(sourcePath, Encoding.Default, false))

                {

                    using (StreamWriter sw = new StreamWriter(tempName, false, Encoding.UTF8))

                    {

                        int charsRead;

                        char[] buffer = new char[128 * 1024];

                        while ((charsRead = sr.ReadBlock(buffer, 0, buffer.Length)) > 0)

                        {

                            sw.Write(buffer, 0, charsRead);

                        }

                    }

                }

                File.Delete(destPath);

                File.Move(tempName, destPath);

            }

            finally

            {

                File.Delete(tempName);

            }

        }

        public void RenameSubtitle(String sourcePath, String destPath)
        {
            File.Move(sourcePath, Path.ChangeExtension(destPath, "cz" + Path.GetExtension(sourcePath)));
        }
        
        public static byte[] ComputeMovieHash(string filename)
        {
            byte[] result;
            using (Stream input = File.OpenRead(filename))
            {
                result = ComputeMovieHash(input);
            }
            return result;
        }

        private static byte[] ComputeMovieHash(Stream input)
        {
            long lhash, streamsize;
            streamsize = input.Length;
            lhash = streamsize;

            long i = 0;
            byte[] buffer = new byte[sizeof(long)];
            while (i < 65536 / sizeof(long) && (input.Read(buffer, 0, sizeof(long)) > 0))
            {
                i++;
                lhash += BitConverter.ToInt64(buffer, 0);
            }

            input.Position = Math.Max(0, streamsize - 65536);
            i = 0;
            while (i < 65536 / sizeof(long) && (input.Read(buffer, 0, sizeof(long)) > 0))
            {
                i++;
                lhash += BitConverter.ToInt64(buffer, 0);
            }
            input.Close();
            byte[] result = BitConverter.GetBytes(lhash);
            Array.Reverse(result);
            return result;
        }

        private static string ToHexadecimal(byte[] bytes)
        {
            StringBuilder hexBuilder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                hexBuilder.Append(bytes[i].ToString("x2"));
            }
            return hexBuilder.ToString();
        }
    }
}
