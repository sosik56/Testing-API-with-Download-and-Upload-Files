using System.Collections.Generic;
using System;
using System.Linq;


namespace examTaskVariant2.Models
{
    public class TestModel
    {
        public string Duration { get; set; }
        public string Method { get; set; }
        public string Name { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string Status { get; set; }
        

        public static bool IsSortedByDecendingByFildName(List<TestModel> listofTests, string filedName)
        {
            if (listofTests.SequenceEqual(listofTests.OrderByDescending(d => d.GetType().GetProperty(filedName))))
            {
                return true;
            }
            return false;
        }

        public static int MathFromTwoLists(List<TestModel> bigestList, List<TestModel> smallerList)
        {
            int count = 0;
            foreach (var item in smallerList)
            {
                foreach (var item2 in bigestList)
                {
                    if (item.Equals(item2))
                    {
                        count++;
                        break;
                    }
                }
            }
            return count;
        }

        public bool Equals(TestModel other)
        {            
            return this.Duration.ToLower() == other.Duration.ToLower() &&
                   (other.EndTime == null || this.EndTime == other.EndTime) &&
                   this.Method.ToLower() == other.Method.ToLower() &&
                   this.Name.ToLower()  == other.Name.ToLower() &&
                   this.StartTime.ToLower() == other.StartTime.ToLower() &&
                   this.Status.ToLower() == other.Status.ToLower();
        }
    }
}
