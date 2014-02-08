using System;
using System.Collections.Generic;
using System.Drawing;

namespace Resume
{
    public class JuniorDotNet
    {
        public string Name;
        public string Adress;
        public string Target;
        public Bitmap Photo;
        public DateTime BirthDate;
        public String[] Education;
        public Dictionary<string, List<string>> Skills;
        public string Hobbie;
        private Dictionary<string, string> _contacts;

        public JuniorDotNet()
        {
            Name = "Natalya Lysakova";

            Adress = "Biryzova st. 14/12, Donetsk, Ukraine 83084";

            Target = "Interested to work as a Junior C# / ASP.Net programmer in your organization where I can get my first experience in real projects and improve my skills in favorite technology.";

            Photo = new Bitmap("my_photo.jpg");

            DateTime.TryParse("22.08.1992", out BirthDate);

            Education = new[]
            {
                "2010 – 2014, Donetsk National Technical University,Faculty of Computer Science and Technology, Bachelor",
                "1999 - 2010, School №98, Donetsk, Ukraine"
            };

            Skills = new Dictionary<string, List<string>> 
            {
                {"Programming", new List<string> {
                    "C#",
                    "WinForms",
                    ".NET Framework",
                    "ASP.Net",
                    "basic knowledge of C",
                    "C++",
                    "Java",
                    "HTML",
                    "CSS",
                    "VisualBasic"
                }},
                
                { "IDE", new List<string> {
                    "Visual Studio 2010/2012/2013",
                    "Eclipse",
                    "IntelliJ IDEA"
                }},

                {"VCS", new List<string> {
                    "Git",
                    "Mercurial"
                }},

                {"Database", new List<string> {
                    "SQL Server 2012",
                    "MS Access 2010/2013"
                }},

                {"Office Software",new List<string> {
                    "MS Word",
                    "MS Excel",
                    "MS PowerPoint",
                    "MS Visio",
                    "ConceptDraw MindMap",
                    "AllFusion Process Modeler"
                }}
            };

            Hobbie =
                "I like to play computer games. My favorite genre is simulator. Most of all I like the game series SimCity and Sid Meier’s Civilization. \n In addition, I like superheroes movies, Star Wars universe and Lord of The Rings Trilogy. Driver license type B.";

            _contacts = new Dictionary<string, string>
            {
                {"Phone", "+38(050)810-42-50"},
                {"e-mail", "natashalysakova[at]gmail.com"},
                {"skype", "natashalysakova"}
            };
        }
    }
}