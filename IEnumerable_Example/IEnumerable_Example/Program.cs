using System;
using System.Collections;

namespace IEnumerable_Example
{
    public class Person
    {
        public Person(string fname, string lname)
        {
            this._firstName = fname;
            this._lastName = lname;
        }

        public string _firstName;
        public string _lastName;
    }

    public class People : IEnumerable
    {
        public Person [] _people;

        public People (Person [] pArray)
        {
            _people = new Person[pArray.Length];

            for (int i = 0; i < pArray.Length; i++)
            {
                _people[i] = pArray[i];
            }
        }

        // Implementation of GetEnumerator method
        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator) GetEnumerator();
        }

        public PeopleEnum GetEnumerator()
        {
            return new PeopleEnum(_people);
        }


        // After the implementation of IEnumerable, IEnumerator has to be implemented as well
        public class PeopleEnum : IEnumerator
        {
            public Person[] _people;
            // Enumerators are positioned before the first element of the collection, until MoveNext() is called for the first time
            int position = -1;

            public PeopleEnum(Person [] peopleList)
            {
                this._people = peopleList;
            }

            public bool MoveNext()
            {
                position++;
                return (position < _people.Length);
            }

            public void Reset()
            {
                position = -1;
            }

            object IEnumerator.Current
            {
                get
                {
                    return Current;
                }
            }

            public Person Current
            {
                get
                {
                    try
                    {
                        return _people[position];
                    }
                    catch (IndexOutOfRangeException)
                    {
                        throw new IndexOutOfRangeException();
                    }
                }
            }

        }
    }



    class Program
    {
        static void Main(string[] args)
        {
            Person[] peopleArray = new Person[4]
            {
                new Person("John", "Wilkinson"),
                new Person("Marie", "Weber"),
                new Person("Thomas", "Wagner"),
                new Person("Jack", "Smith")
            };

            People peopleList = new People(peopleArray);
            foreach (Person person in peopleList)
            {
                Console.WriteLine("First name: " + person._firstName + ", Last name: " + person._lastName);
            }
        }
    }
}
