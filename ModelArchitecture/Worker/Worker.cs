namespace JobWorker;

using Services.WeatherReportJobService;

public class Worker : BackgroundService
{

    private int executionCount = 0;

    private readonly ILogger<Worker> _logger;

    public Worker(ILogger<Worker> logger)
    {
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {

            var count = Interlocked.Increment(ref executionCount);

            if(executionCount == 1)
            {
                _logger.LogInformation("Worker started at: {time}", DateTimeOffset.Now);            
            } 
            else 
            {
                _logger.LogInformation("Worker running job: {time} - job run #: {count}", 
                                       DateTimeOffset.Now, count);
            }            

            // Do the job
            await WeatherReportJobScheduler.RunScheduledJobs();            
            
            // Wait a minute
            await Task.Delay(1000 * 60, stoppingToken);
        }
    }
}
