// Title: Calling async code from normal code
using static Apparatus.Examples.Output;

namespace Apparatus.Examples;

public static class TaskExamples
{
    private static async Task<int> SlowAnswerAsync()
    {
        await Task.Delay(10);
        return 42;
    }

    private static async Task FailAsync()
    {
        await Task.Delay(1);
        throw new InvalidOperationException("something broke");
    }

    public static void Run()
    {
        Title("Tasks");
        Show("SlowAnswerAsync().Await()", SlowAnswerAsync().Await());

        Task.Delay(10).Await();
        Show("Task.Delay(10).Await()", "finished");

        try
        {
            FailAsync().Await();
        }
        catch (InvalidOperationException ex)
        {
            // You get the real exception, not an AggregateException.
            Show("FailAsync().Await() threw", ex.Message);
        }

        // The class name clashes with System.Threading.Tasks.TaskExtensions, so write it in full for static calls.
        Show("TaskExtensions.RunSync(async func)", Apparatus.TaskExtensions.RunSync(SlowAnswerAsync));
        Apparatus.TaskExtensions.RunSync(() => Task.Delay(5));
        Show("TaskExtensions.RunSync(async action)", "finished");
    }
}
