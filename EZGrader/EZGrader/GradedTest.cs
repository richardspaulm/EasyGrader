using System;
using System.Collections.Generic;
using System.Text;

namespace EZGrader
{
    public class GradedTest
    {
        public string testNamed { set; get; }
        public string testClass { set; get; }
        public List<String> scoredNameList { set; get; }
        public List<Double> scoredScoreList { set; get; }
        public Double totalQuestions { set; get;  }
        public List<Double> rawScores { set; get; }
    }
}
