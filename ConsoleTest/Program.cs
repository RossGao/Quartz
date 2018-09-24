using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //var test = new ThreadLocal<Test>(() =>
            //    { return new Test(); }
            //);
            var test = new ThreadLocal<ITest>(trackAllValues:true);
            Action actionA = () =>
            {
                test.Value = new TestA();
                //Console.WriteLine($"The test id is {test.Value.Id}");
            };

            Action actionB = () =>
            {
                test.Value = new TestB();
            };

            //Action action = () =>
            //      {
            //          //bool isCreated = threadName.IsValueCreated;
            //          //Console.WriteLine(threadName.Value + (isCreated ? ("Repeated"):""));
            //          //number.Value = Guid.NewGuid();
            //          Console.WriteLine($"{test.Value.Id} threadId is: {Thread.CurrentThread.ManagedThreadId}.");
            //      };

            Parallel.Invoke(actionA, actionB, actionB, actionA, actionB, actionA, actionA, actionB, actionB);
            test.Values.ToList().ForEach(v => Console.WriteLine(v.GetType()));
            test.Dispose();
            Console.Read();
        }

        //static void Main(string[] args)
        //{
        //    var number = new ThreadLocal<int>(() => 0, trackAllValues: false);

        //    Parallel.For(0, 10, i =>
        //    {
        //        number.Value += 1;
        //        Console.WriteLine(number.Value + " " + Thread.CurrentThread.ManagedThreadId);
        //    });

        //    Console.Read();
        //}
    }

    public interface ITest
    {
    }

    public class TestA : ITest
    {
        public Guid Id { get; set; }

        public TestA()
        {
            Id = Guid.NewGuid();
        }
    }

    public class TestB : ITest
    {
        public Guid Id { get; set; }

        public TestB()
        {
            Id = Guid.NewGuid();
        }
    }
}
