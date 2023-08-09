using System;

namespace CrimeSceneDesktop.Contracts
{
    internal class Person
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public int Count { get; set; }
    }
}
