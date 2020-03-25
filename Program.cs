using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CSV.Models;
using static System.Net.Mime.MediaTypeNames;
using CSV.Models.Utilities;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;

namespace CSV
{
    class Program
    {
        static void Main(string[] args)
        {
            Student myrecord = new Student { StudentId = "200425198", FirstName = "MaryPravalika", LastName = "Jaddu" };
        List<Student> students = new List<Student>();
        List<string> directories = FTP.GetDirectory(Constants.FTP.BaseUrl);



            foreach (var directory in directories)
            {
                Student student = new Student() { AbsoluteUrl = Constants.FTP.BaseUrl };
        student.FromDirectory(directory);
                Console.WriteLine(directory);
                Console.WriteLine(student);
                string infoFilePath = student.FullPathUrl + "/" + Constants.Locations.InfoFile;
        string imageFilePath = student.FullPathUrl + "/" + Constants.Locations.ImageFile;


        bool fileExists = FTP.FileExists(infoFilePath);
                //if (FTP.FileExists(student.InfoCSVPath))
                {
                    var csvBytes = FTP.DownloadFileBytes(student.InfoCSVPath);

        string csvFileData = Encoding.ASCII.GetString(csvBytes, 0, csvBytes.Length);

        string[] data = csvFileData.Split("\r\n", StringSplitOptions.RemoveEmptyEntries);
                    if (data.Length != 2)
                    {
                        Console.WriteLine("Error in your Age");
                    }
                    else
                    {
                        student.FromCSV(data[1]);
                        Console.WriteLine("Age:" + student.Age);
                    }


                }
                if (fileExists == true)
                {
                    string csvPath = $@"/Users/marypravalikajaddu/Desktop/StudentFiles/{directory}.csv";

                    //FTP.DownloadFile(infoFilePath, csvPath);
                    Console.WriteLine("Found info file:");
                }
                else
                {
                    Console.WriteLine("Could not find info file:");
                }

                Console.WriteLine("\t" + infoFilePath);

            

                bool imageFileExists = FTP.FileExists(imageFilePath);

                if (imageFileExists == true)
                {

                    Console.WriteLine("Found image file:");
                }
                else
                {
                    Console.WriteLine("Could not find image file:");
                }

                Console.WriteLine("\t" + imageFilePath);

                students.Add(student);
            }

            //save to csv
       
            string studentsCSVPath = $"{Constants.Locations.DataFolder}//students.csv";
            //Establish a file stream to collect data from the response
            using (StreamWriter fs = new StreamWriter(studentsCSVPath))
            {
                foreach (var student in students)
                {
                    fs.WriteLine(student.ToCSV());
                }
            }


            int count = students.Count();
           Console.WriteLine("Number of students:"+count);

            string inputStarts = "  M";
        string Contains = "ar";
        int countStarts = 0;
        int countContains = 0;
                foreach (var listname in students)
                {
                string s = listname.LastName;
                if (s.StartsWith(inputStarts))
                {
                    Console.WriteLine("Last starts with " + inputStarts + " " + listname);
                    countStarts++;
                }
            }
            Console.WriteLine("People who have LastName that starts with 'M':" + countStarts);
            foreach (var record in students)
            {
                string s = record.LastName;
                if (s.Contains(Contains))
                {
                    Console.WriteLine("starts with " + Contains + " " + record);
                    countContains++;
                }
            }
            Console.WriteLine("People who have LastName that contains 'ra':" + countContains);

            Student me = students.SingleOrDefault(x => x.StudentId == myrecord.StudentId);
            Student meUsingFind = students.Find(x => x.StudentId == myrecord.StudentId);


                var avgage = students.Average(x => x.Age);
                var minage = students.Min(x => x.Age);
                var maxage = students.Max(x => x.Age);

            Console.WriteLine("Average Age:" + avgage);
            Console.WriteLine("Highest Age:" + maxage);
            Console.WriteLine("Lowest Age:" + minage);

            string studentsjsonPath = $"{Constants.Locations.DataFolder}//students.json";

            //Establish a file stream to collect data from the response
            using (StreamWriter fs = new StreamWriter(studentsjsonPath))
            {
                foreach (var student in students)
                {
                    string Student = Newtonsoft.Json.JsonConvert.SerializeObject(student);
                    fs.WriteLine(Student.ToString());
                }
            }

            //string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

            //string exePath = Environment.CurrentDirectory;
            //string imageFolder = $"{exePath}//..//..//..//Content/Image";

            //string filePath = @"/Users/marypravalikajaddu/Desktop/info.csv";
            //string fileContents;

            //using (StreamReader stream = new StreamReader(filePath))
            //{
            //    fileContents = stream.ReadToEnd();
            //}

            //List<string> entries = new List<string>();

            //entries = fileContents.Split("\r\n", StringSplitOptions.RemoveEmptyEntries).ToList();

            //Student student = new Student();
            //student.FromCSV(entries[1]);

            //string[] data = entries[1].Split(",", StringSplitOptions.None);

            //Student student = new Student();
            //student.StudentId = data[0];
            //student.FirstName = data[1];
            //student.LastName = data[2];
            //student.DateOfBirth = data[3];
            //student.ImageData = data[4];

            //Console.WriteLine(student.ToCSV());
            //Console.WriteLine(student.ToString());


            //string imagefilePath = $"{Constants.Locations.ImageFile}\\{Constants.Locations.ImageFile};
            //Image image = Image.FromFile(imagefilePath);
            //string base64Image = ImageToBase64(image, ImageFormat.Jpeg);
            //student.ImageData = base64Image;

            //string newfilePath = $"/Users/marypravalikajaddu/Desktop/{student.ToString()}.jpg";
            //FileInfo newfileinfo = new FileInfo(newfilePath);
            //Image studentImage = Base64ToImage(student.ImageData);
            //studentImage.Save(newfileinfo.FullName, ImageFormat.Jpeg);

            /// <summary>
            /// Converts an Image object to Base64
            /// </summary>
            /// <param name="image">An Image object</param>
            /// <param name="format">The format of the image (JPEG, BMP, etc.)</param>
            /// <returns>Base64 encoded string representation of an Image</returns>
            //public static string ImageToBase64(Image image, ImageFormat format)
           //{
            //    using (MemoryStream ms = new MemoryStream())
            //    {
            //        // Convert Image to byte[]
            //        image.Save(ms, format);
            //        byte[] imageBytes = ms.ToArray();

            //        // Convert byte[] to Base64 String
            //        string base64String = Convert.ToBase64String(imageBytes);
            //        return base64String;
            //    }
            //}

            ///// <summary>
            ///// Converts a Base64 encoded string to an Image
            ///// </summary>
            ///// <param name="base64String">Base64 encoded Image string</param>
            ///// <returns>Decoded Image</returns>
            //public static Image Base64ToImage(string base64String)
            //{
            //    // Convert Base64 String to byte[]
            //    byte[] imageBytes = Convert.FromBase64String(base64String.Trim());
            //    var ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
            //    // Convert byte[] to Image
            //    ms.Write(imageBytes, 0, imageBytes.Length);
            //    Image image = Image.FromStream(ms, true);
            //    return image;
            //}
        }

       
    }
}
