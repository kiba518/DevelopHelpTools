using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Linq;
using Utility;

namespace PomReadFramework
{
    internal class Program
    {
        static Dictionary<string, Tuple<string, string, string, string>> listDependecy1;
        static Dictionary<string, Tuple<string, string, string, string>> listDependecy2;

        static Dictionary<string, Dictionary<string, Tuple<string, string, string, string>>> childListDependecy1 = new Dictionary<string, Dictionary<string, Tuple<string, string, string, string>>>();
        static Dictionary<string, Dictionary<string, Tuple<string, string, string, string>>> childListDependecy2 = new Dictionary<string, Dictionary<string, Tuple<string, string, string, string>>>();

        static void Main(string[] args)
        {

            //Dictionary<string, Tuple<string, string>> source = new Dictionary<string, Tuple<string, string>>();
            //source.Add("blade-bom", Tuple.Create("C:\\MavenRepostoty\\org\\springblade\\platform\\blade-bom\\4.0.1.RELEASE\\blade-bom-4.0.1.RELEASE.pom", "C:\\MavenRepostoty\\org\\springblade\\platform\\blade-bom\\2.0.21.12010-SNAPSHOT\\blade-bom-2.0.21.12010-SNAPSHOT.pom"));
            //source.Add("blade-starter-loadbalancer", Tuple.Create("C:\\MavenRepostoty\\org\\springblade\\blade-starter-loadbalancer\\4.0.1.RELEASE\\blade-starter-loadbalancer-4.0.1.RELEASE.pom", "C:\\Project\\huizhuyun-framework\\huizhuyun-framework\\blade-common\\pom.xml"));



           // "C:\\MavenRepostoty\\org\\springblade\\blade-core-boot\\4.0.1.RELEASE\\blade-core-boot-4.0.1.RELEASE.pom";


            listDependecy1 = GetDependcyManagement("blade-bom-4.0.1.RELEASE.pom");
            listDependecy2 =  GetDependcyManagement("blade-bom-2.0.21.12010-SNAPSHOT.pom");
             
            string name = "blade-bom-dependencyManagement";
            WriteToCsv(name,listDependecy1, listDependecy2);

            string rootPath = "C:\\MavenRepostoty\\";
            
            listDependecy1.Where(x => x.Key.Contains("org.springblade:blade")).ToList().ForEach(item => {
                //C:\MavenRepostoty\org\springblade\blade-core-auto\4.0.1.RELEASE\blade-core-auto-4.0.1.RELEASE.pom
                string key =item.Key;//org.springblade:blade-core-auto
                string version = item.Value.Item1; //4.0.1.RELEASE
                string groupId = item.Value.Item3; 
                string artifactId = item.Value.Item4;

                string groupIdDir = groupId.Replace(".", "\\");
                string path = rootPath + groupIdDir + "\\" + artifactId + "\\" + version + "\\" + artifactId + "-" + version + ".pom";

                Dictionary<string, Tuple<string, string, string, string>> listDependecy = GetDependcy(path);

                childListDependecy1.Add(groupId+"-"+ artifactId, listDependecy);

            });

            listDependecy2.Where(x => x.Key.Contains("org.springblade:blade")).ToList().ForEach(item => {
                //C:\MavenRepostoty\org\springblade\blade-core-auto\4.0.1.RELEASE\blade-core-auto-4.0.1.RELEASE.pom
                string key = item.Key;//org.springblade:blade-core-auto
                string version = item.Value.Item1; //4.0.1.RELEASE
                string groupId = item.Value.Item3;
                string artifactId = item.Value.Item4;

                string groupIdDir = groupId.Replace(".", "\\");
                string path = rootPath + groupIdDir + "\\" + artifactId + "\\" + version + "\\" + artifactId + "-" + version + ".pom";

                Dictionary<string, Tuple<string, string, string, string>> listDependecy = GetDependcy(path);
                childListDependecy2.Add(groupId + "-" + artifactId, listDependecy);

            });

            WriteToCsvChild(childListDependecy1, childListDependecy2);

            Console.Read();
        }

        static void WriteToCsv(string name ,Dictionary<string, Tuple<string, string, string, string>> listDependecy1, Dictionary<string, Tuple<string, string, string, string>> listDependecy2)
        {
            string filePath = name+".csv";
            FileHelper.DeleteFile(filePath);
            using (StreamWriter writer = new StreamWriter(filePath, false, Encoding.UTF8))
            {
                foreach (var pair1 in listDependecy1)
                {

                    var pair2 = listDependecy2.FirstOrDefault(p => p.Key == pair1.Key);

                    string pair1Key = pair1.Key;
                    string pair2Key = "";
                    string pair1Value = pair1.Value.Item1;
                    string pair1OriginalValue = pair1.Value.Item2;
                    string pair2Value = "";
                    string pair2OriginalValue = "";
                    if (pair2.Key != null)
                    {
                        pair2Key = pair2.Key;
                        pair2Value = pair2.Value.Item1;
                        pair2OriginalValue = pair2.Value.Item2;
                    }
                    writer.WriteLine($"{pair1Key},{pair1Value},{pair2Value},{pair1OriginalValue}/{pair2OriginalValue}");
                }
            }
        }
        static void WriteToCsvChild(Dictionary<string, Dictionary<string, Tuple<string, string, string, string>>> dicDependecy1, Dictionary<string, Dictionary<string, Tuple<string, string, string, string>>> dicDependecy2)
        {

            foreach (var dic1 in dicDependecy1)
            {
                var name = dic1.Key;
                var listD1 = dic1.Value;

                var dic2 = dicDependecy2.FirstOrDefault(p=>p.Key==name);
                Dictionary<string, Tuple<string, string, string, string>> listD2 = null;
                if (!dic2.Equals(null) && dic2.Key != null)
                {
                    listD2 = dic2.Value;
                }

                string filePath = name + ".csv";
                FileHelper.DeleteFile(filePath);
                using (StreamWriter writer = new StreamWriter(filePath, false, Encoding.UTF8))
                {
                    foreach (var pair1 in listD1)
                    {

                       

                        string pair1Key = pair1.Key;
                        string pair2Key = "";
                        string pair1Value = pair1.Value.Item1;
                        string pair1OriginalValue = pair1.Value.Item2;
                        string pair2Value = "";
                        string pair2OriginalValue = "";

                        var pair2 = listD2?.FirstOrDefault(p => p.Key == pair1.Key);
                        if (!pair2.Equals(null) &&pair2.Value.Key != null)
                        {
                            pair2Key = pair2.Value.Key;
                            pair2Value = pair2.Value.Value.Item1;
                            pair2OriginalValue = pair2.Value.Value.Item2;
                            if(string.IsNullOrEmpty(pair2Value))
                            {
                                var one = listDependecy2.FirstOrDefault(p => p.Key == pair1.Key);
                                if(one.Key!=null)
                                {
                                    pair2Value = one.Value.Item1;
                                }
                                
                            } 
                            listD2.Remove(pair2.Value.Key);
                        }
                        else
                        {
                            pair2Value = "无";
                        }
                        
                        writer.WriteLine($"{pair1Key},{pair1Value},{pair2Value},{pair1OriginalValue}/{pair2OriginalValue}");
                    }

                    if(listD2!=null && listD2.Count>0)
                    {
                        foreach (var pair2 in listD2)
                        {
                            string pair1Key = pair2.Key;
                            string pair2Key = "";
                            string pair1Value = "无";
                            string pair1OriginalValue = "";
                            string pair2Value = pair2.Value.Item1;
                            string pair2OriginalValue = pair2.Value.Item2;
                            if (string.IsNullOrEmpty(pair2Value))
                            {
                                var one = listDependecy2.FirstOrDefault(p => p.Key == pair2.Key);
                                if (one.Key != null)
                                {
                                    pair2Value = one.Value.Item1;
                                }

                            }

                            writer.WriteLine($"{pair1Key},{pair1Value},{pair2Value},{pair1OriginalValue}/{pair2OriginalValue}");
                        }
                    }

                }
            }
        }


        static Dictionary<string, Tuple<string,string, string, string>> GetDependcyManagement(string path)
        {
            Dictionary<string, Tuple<string, string, string, string>> ret = new Dictionary<string, Tuple<string, string, string, string>>();
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(path);
            var element = xmlDoc.DocumentElement;
            XmlNode dependencyManagement = null;
            XmlNode properties = null;
            foreach (XmlNode item in element.ChildNodes)
            {
                if (item.Name == "dependencyManagement")
                {
                    dependencyManagement = item;

                }
                if (item.Name == "properties")
                {
                    properties = item;

                }

            }
            Dictionary<string, string> dicProperty = new Dictionary<string, string>();
            if (properties != null)
            {
                foreach (XmlNode item in properties)
                {
                    dicProperty.Add(item.Name, item.InnerText);
                }
            }
            foreach (XmlNode dependenciesNode in dependencyManagement.ChildNodes)
            {
                if (dependenciesNode.Name == "dependencies")
                {
                    foreach (XmlNode dependencyNode in dependenciesNode.ChildNodes)
                    {
                        if (dependencyNode.Name == "dependency")
                        {
                            string groupId = "", artifactId = "", version = "",originalVersion = "";
                            foreach (XmlNode item in dependencyNode.ChildNodes)
                            {

                                if (item.Name == "groupId")
                                {
                                    groupId = item.InnerText;
                                }
                                if (item.Name == "artifactId")
                                {
                                    artifactId = item.InnerText;
                                }
                                if (item.Name == "version")
                                {
                                    originalVersion = item.InnerText;
                                    version = item.InnerText;
                                    var val = dicProperty.Where(p => version.Contains(p.Key)).ToList();
                                    if (val != null && val.Count > 0)
                                    {
                                        version = val.First().Value;
                                    }
                                }

                            }
                            string key = $"{groupId}:{artifactId}";
                            if (ret.Keys.Contains(key))
                            {
                                ret.Remove(key);
                            }
                            ret.Add(key, Tuple.Create(version, originalVersion, groupId, artifactId));
                            Console.WriteLine($"{groupId}:{artifactId}:{version}");

                        }
                    }
                }
            }

            return ret;
        }


        static Dictionary<string, Tuple<string, string, string, string>> GetDependcy(string path)
        {
            Dictionary<string, Tuple<string, string, string, string>> ret = new Dictionary<string, Tuple<string, string, string, string>>();
            XmlDocument xmlDoc = new XmlDocument();
            if (FileHelper.IsExistFile(path))
            {
                xmlDoc.Load(path);
                var element = xmlDoc.DocumentElement;
                XmlNode dependencies = null;
                XmlNode properties = null;
                foreach (XmlNode item in element.ChildNodes)
                {
                    if (item.Name == "dependencies")
                    {
                        dependencies = item;

                    }
                    if (item.Name == "properties")
                    {
                        properties = item;

                    }

                }
                Dictionary<string, string> dicProperty = new Dictionary<string, string>();
                if (properties != null)
                {
                    foreach (XmlNode item in properties)
                    {
                        dicProperty.Add(item.Name, item.InnerText);
                    }
                }
                if (dependencies != null)
                {
                    foreach (XmlNode dependencyNode in dependencies.ChildNodes)
                    {
                        if (dependencyNode.Name == "dependency")
                        {
                            string groupId = "", artifactId = "", version = "", originalVersion = "";
                            foreach (XmlNode item in dependencyNode.ChildNodes)
                            {

                                if (item.Name == "groupId")
                                {
                                    groupId = item.InnerText;
                                }
                                if (item.Name == "artifactId")
                                {
                                    artifactId = item.InnerText;
                                }
                                if (item.Name == "version")
                                {
                                    originalVersion = item.InnerText;
                                    version = item.InnerText;
                                    var val = dicProperty.Where(p => version.Contains(p.Key)).ToList();
                                    if (val != null && val.Count > 0)
                                    {
                                        version = val.First().Value;
                                    }
                                }

                            }
                            string key = $"{groupId}:{artifactId}";
                            if (ret.Keys.Contains(key))
                            {
                                ret.Remove(key);
                            }
                            ret.Add(key, Tuple.Create(version, originalVersion, groupId, artifactId));
                            Console.WriteLine($"{groupId}:{artifactId}:{version}");

                        }
                    }

                }
            }

            return ret;
        }
    }
}
