using System;
using UnityEngine;

namespace Structs
{
    [Serializable]
    public class Question
    {
        public string Text;
        public string CorrectAnswer;
        public long Id;

        public GameObject AnswerModels;


        public long GenerateId()
        {
            Id = DateTime.Now.Ticks;
            return Id;
        }
    }
}