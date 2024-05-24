using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MokshenDesktop.Model
{
    public class Exercise
    {
        public string GroupIndex { get; set; }
        public Guid Id { get; set; }
        public List<Answer> Answers { get; set; }
        public string TrueAnswer { get; set; }
        public string Category { get; set; }
    }

    public class Answer
    {
        public string Content { get; set; }
        public bool IsSelected { get; set; }
        public int Index {  get; set; }
    }
}
