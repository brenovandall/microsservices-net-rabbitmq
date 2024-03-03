namespace ItemService.EventProcessor;

public interface IExecuteEvent
{
    void Execute(string message);
}
