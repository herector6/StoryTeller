namespace StoryTeller.RequestModel;

public class UpdateStoryTellerRequestModel
{

    public string Title { get; set; }

    public string Story { get; set; }

    public DateTime? Date { get; set; }

    public IFormFile? Image { get; set; }

}