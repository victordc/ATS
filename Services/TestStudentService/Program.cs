using System;
using System.Linq;
using System.Activities;
using System.Activities.Statements;
using System.Threading;
using System.Collections.Generic;
using WFServices;

namespace TestStudentService
{

    class Program
    {
        static void Main(string[] args)
        {
            AutoResetEvent syncEvent = new AutoResetEvent(false);
            AutoResetEvent idleEvent = new AutoResetEvent(false);

            // Successfull Case
            //var inputs = new Dictionary<string, object>() { { "ID", "A6041119R" }, { "Degree", "Degree"}, {"YearExperience",2}, {"MediumInstruction", true}, {"EnglishType", "IELTS"}, {"EnglishScore", 7} };

            // Fail Case
            var inputs = new Dictionary<string, object>() { { "ID", "A6041119R" }, { "Degree", "Degree" }, { "YearExperience", 2 }, { "MediumInstruction", true }, { "EnglishType", "IELTS" }, { "EnglishScore", 5 } };

            WorkflowApplication wfApp =
                new WorkflowApplication(new AdmissionService(), inputs);

            wfApp.Completed = delegate(WorkflowApplicationCompletedEventArgs e)
            {
                string status = (e.Outputs["Status"].ToString());
                Console.WriteLine("Your application is {0}.", status);
                Console.ReadLine();

                syncEvent.Set();
            };

            wfApp.Aborted = delegate(WorkflowApplicationAbortedEventArgs e)
            {
                Console.WriteLine(e.Reason);
                syncEvent.Set();
            };

            wfApp.OnUnhandledException = delegate(WorkflowApplicationUnhandledExceptionEventArgs e)
            {
                Console.WriteLine(e.UnhandledException.ToString());
                return UnhandledExceptionAction.Terminate;
            };

            wfApp.Idle = delegate(WorkflowApplicationIdleEventArgs e)
            {
                idleEvent.Set();
            };

            wfApp.Run();

            // Loop until the workflow completes.
            WaitHandle[] handles = new WaitHandle[] { syncEvent, idleEvent };
            while (WaitHandle.WaitAny(handles) != 0)
            {
                string status = string.Empty;
                wfApp.ResumeBookmark("Status", status);
                //// Gather the user input and resume the bookmark.
                //bool validEntry = false;
                //while (!validEntry)
                //{
                //    int Guess;
                //    if (!Int32.TryParse(Console.ReadLine(), out Guess))
                //    {
                //        Console.WriteLine("Please enter an integer.");
                //    }
                //    else
                //    {
                //        validEntry = true;
                //        wfApp.ResumeBookmark("EnterGuess", Guess);
                //    }
                //}
            }
        }
    }
}
