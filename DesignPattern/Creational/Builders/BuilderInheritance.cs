using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DesignPattern.Creational.Builders
{
    [TestClass]
    public class BuilderInheritance : IExecute
    {
        public class Person
        {
            public string Name;

            public string Position;

            public DateTime DateOfBirth;

            public class Builder : PersonBirthDateBuilder<Builder>
            {
                public Builder()
                {
                }
            }

            public static Builder New => new Builder();

            public override string ToString()
            {
                return $"{nameof(Name)}: {Name}, {nameof(Position)}: {Position}";
            }
        }

        public abstract class PersonBuilder
        {
            protected Person Person = new Person();

            public Person Build()
            {
                return Person;
            }
        }

        public class PersonInfoBuilder<TSelf> : PersonBuilder
            where TSelf : PersonInfoBuilder<TSelf>
        {
            public TSelf Called(string name)
            {
                Person.Name = name;
                return (TSelf)this;
            }
        }

        public class PersonJobBuilder<TSelf>
            : PersonInfoBuilder<PersonJobBuilder<TSelf>>
            where TSelf : PersonJobBuilder<TSelf>
        {
            public TSelf WorksAsA(string position)
            {
                Person.Position = position;
                return (TSelf)this;
            }
        }

        // here's another inheritance level
        // note there's no PersonInfoBuilder<PersonJobBuilder<PersonBirthDateBuilder<SELF>>>!

        public class PersonBirthDateBuilder<TSelf>
            : PersonJobBuilder<PersonBirthDateBuilder<TSelf>>
            where TSelf : PersonBirthDateBuilder<TSelf>
        {
            public TSelf Born(DateTime dateOfBirth)
            {
                Person.DateOfBirth = dateOfBirth;
                return (TSelf)this;
            }
        }

        class SomeBuilder : PersonBirthDateBuilder<SomeBuilder>
        {
        }

        [TestMethod]
        public void Execute()
        {
            var me = Person.New
                .Called("Dmitri")
                .WorksAsA("Quant")
                .Born(DateTime.UtcNow)
                .Build();
            Console.WriteLine(me);
        }
    }
}