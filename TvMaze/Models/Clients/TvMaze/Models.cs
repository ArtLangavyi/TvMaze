public class ScheduleOverview
{
    public int id { get; set; }
    public Links _links { get; set; }
}

public class Links
{
    public ShowLink show { get; set; }
}

public class ShowLink
{
    public string href { get; set; }
}
