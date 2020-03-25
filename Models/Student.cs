using System;
using System.Collections.Generic;

namespace CSV.Models
{
    public class Student
    {
        public static string HeaderRow = $"{nameof(Student.StudentId)},{nameof(Student.FirstName)} , {nameof(Student.LastName)},{nameof(Student.DateOfBirth)}, {nameof(Student.ImageData)}";

        public string StudentId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        private string _DateOfBirth;

        public string DateOfBirth
        {
            get { return _DateOfBirth; }
            set { _DateOfBirth = value;

                DateTime dtOut;
                DateTime.TryParse(_DateOfBirth, out dtOut);
                DateOfBirthDT = dtOut;
            }
        }

        public DateTime DateOfBirthDT { get; private set; }

        public string ImageData { get; set; }

        public string AbsoluteUrl { get; set; }

        public string Directory{ get; set; }

        public string FullPathUrl
        {
            get
            {
                return AbsoluteUrl + "/" + Directory;
            }
        }
        public List<string> Exceptions { get; set; } = new List<string>();

        public void FromCSV(string csvdata)
        {
            string[] data = csvdata.Split(",", StringSplitOptions.None);

            try
            {
                StudentId = data[0];
                FirstName = data[1];
                LastName = data[2];
                DateOfBirth = data[3];
                ImageData = data[4];
            }

            catch (Exception e)
            {
                Exceptions.Add(e.Message);
            }
        }

        public  void FromDirectory(string directory)
        {
            Directory = directory;

            if(String.IsNullOrEmpty(directory.Trim()))
            {
                return;
            }
            string[] data = directory.Trim().Split(" ", StringSplitOptions.None);


            StudentId = data[0];
            FirstName = data[1];
            LastName = data[2];
        }

        public string ToCSV()
        {
            string result = $"{StudentId},{FirstName},{LastName}, {DateOfBirthDT.ToShortDateString()},{ImageData}";
            return result;

        }

        public override string ToString()
        {
            string result = $"{StudentId} {FirstName} {LastName}";
            return result;
        }

        
    }


}
